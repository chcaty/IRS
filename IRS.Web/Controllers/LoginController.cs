using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRS.BLL.Interface;
using IRS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Collections;

namespace IRS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    public class LoginController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private readonly IConfiguration _configuration;
        private IRolePermissionService _rolePermissionService;
        private IPermissionService _permissionService;
        public LoginController(IUserService userService, IConfiguration configuration, IRoleService roleService,IRolePermissionService rolePermissionService, IPermissionService permissionService)
        {
            _configuration = configuration;
            _userService = userService;
            _roleService = roleService;
            _rolePermissionService = rolePermissionService;
            _permissionService = permissionService;
        }

        [HttpPost]
        public ActionResult Login([FromBody] User user)
        {
            var result = _userService.LoadEntities(u => u.UserCode == user.UserCode && u.UserPwd == user.UserPwd).First();
            if (result != null)
            {
                if (Convert.ToDateTime(result.Validity) > Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) || string.IsNullOrEmpty(result.Validity))
                {
                    string token = "";
                    int expires_in = 6000;
                    token = RequestToken(new TokenRequest() { Username = user.UserCode, Password = user.UserPwd }, token, expires_in);

                    return Json(new
                    {
                        token,
                        expires_in,
                    });
                }
            }
            return Content("no");
        }

        public ActionResult Logout()
        {
            //HttpContext.Session.Clear();
            return Content("out");
        }

        [HttpGet]
        public ActionResult Index(string token)
        {
            JwtSecurityTokenHandler jwt = new JwtSecurityTokenHandler();
            var info = jwt.ReadJwtToken(token).Claims;
            var usercode = info.Where(r => r.Type == ClaimTypes.Name).FirstOrDefault().Value;
            var user = _userService.LoadEntities(u => u.UserCode == usercode);
            var role = _roleService.LoadEntities(r => true);
            var rolepermission = _rolePermissionService.LoadEntities(rp => true);
            var permission = _permissionService.LoadEntities(p => true);
            var result = from u in user
                         join r in role on u.RoleId equals r.RoleId
                         select new
                         {
                             u.UserId,
                             u.UserCode,
                             u.UserName,
                             r.RoleName,
                             r.RoleId
                         };
            var permissionlist = from r in result
                        join rp in rolepermission on r.RoleId equals rp.RolesId into temp
                        from tt in temp.DefaultIfEmpty()
                        select new
                        {
                            tt.PermissionId
                        };
            var apilist = from r in permissionlist
                       join p in permission on r.PermissionId equals p.PermissionId
                       select new
                       {
                           route_match = p.PermissionApi
                       };
            var routelist = from r in permissionlist
                          join p in permission on r.PermissionId equals p.PermissionId
                          select new
                          {
                              p.PermissionRoute
                          };
            ArrayList arrayList = new ArrayList();
            foreach(var r in routelist)
            {
                arrayList.Add(r.PermissionRoute);
            }
            return Json(new
            {
                data = new
                {
                    id = result.FirstOrDefault().UserId,
                    name = result.FirstOrDefault().UserName,
                    route = arrayList,
                    //route = new ArrayList{
                    //   "admin","admin/index","record","record/index","role",
                    //   "role/index","permission/index","charts","charts/index","category","category/address","category/error","category/solver"
                    //},
                    permission = apilist,
                    //permission = new ArrayList
                    //{
                    //    new
                    //    {
                    //       route_match = "/api/dashboard"
                    //    },
                    //    new
                    //    {
                    //         route_match = "/api/admin"
                    //    }
                    //},
                    role=new string[1] {result.FirstOrDefault().RoleName }
                 },
                status = "sucess",
                status_code = "200"
            });

        }

        public string RequestToken([FromBody] TokenRequest request, string token, int expires_in)
        {
            // push the user’s name into a claim, so we can identify the user later on.
            var claims = new[]
            {
                   new Claim(ClaimTypes.Name, request.Username)
             };
            //sign the token using a secret key.This secret will be shared between your API and anything that needs to check that the token is legit.
            //Configuration = builder.Build();
            //var value = Configuration["Section:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //.NET Core’s JwtSecurityToken class takes on the heavy lifting and actually creates the token.
            /**
             * Claims (Payload)
                Claims 部分包含了一些跟这个 token 有关的重要信息。 JWT 标准规定了一些字段，下面节选一些字段:

                iss: The issuer of the token，token 是给谁的
                sub: The subject of the token，token 主题
                exp: Expiration Time。 token 过期时间，Unix 时间戳格式
                iat: Issued At。 token 创建时间， Unix 时间戳格式
                jti: JWT ID。针对当前 token 的唯一标识
                除了规定的字段外，可以包含其他任何 JSON 兼容的字段。
             * */
            var tokens = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            return token = new JwtSecurityTokenHandler().WriteToken(tokens);
        }


        public class TokenRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }

}