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

namespace IRS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/login")]
    public class LoginController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private readonly IConfiguration _configuration;
        public LoginController(IUserService userService, IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = userService;
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
                        //data = new
                        //{
                        //    id = result.UserId,
                        //    name = result.UserName,
                        //    permission = new string[0], //{ "admin" },
                        //    role = new string[1] { "admin" }
                        //},
                        //status = "sucess",
                        //status_code = "200"
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
        public ActionResult Index()
        {
            return Json(new
            {
                data = new
                {
                    id = 1,
                    name = "caty",
                    permission = new Array[0],
                    role = new string[1] { "admin" }
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