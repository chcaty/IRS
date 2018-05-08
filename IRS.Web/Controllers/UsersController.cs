using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRS.BLL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IRS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IUserRoleService _userroleService;
        private IRoleService _roleService;
        public UsersController(IUserService userService, IUserRoleService userroleService, IRoleService roleService)
        {
            _userService = userService;
            _userroleService = userroleService;
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            var userList = _userService.LoadEntities(u => true);
            var userroleList = _userroleService.LoadEntities(ur => true);
            var roleList = _roleService.LoadEntities(r => true);
            var result = from u in userList
                         join ur in userroleList
                         on u.UserId equals ur.UserId
                         join r in roleList
                         on ur.RoleId equals r.RoleId
                         select new { u, r.RoleName };
            return Json(new { result });
        }
        

    }
}