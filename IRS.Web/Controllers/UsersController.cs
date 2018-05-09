using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRS.BLL.Interface;
using IRS.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IRS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : BaseController
    {
        private IUserService _userService;
        private IRoleService _roleService;
        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            var userList = _userService.LoadEntities(u => true);
            var roleList = _roleService.LoadEntities(r => true);
            var result = from u in userList
                         join r in roleList
                         on u.RoleId equals r.RoleId
                         select new { u, r.RoleName,r.RoleId };
            return Json(result);
        }
        
        [HttpPost]
        public ActionResult AddUser([FromBody]User user)
        {
            var result = _userService.AddEntity(user);
            return Content("ok");
        }

        public ActionResult EditUser([FromBody]User user)
        {
            var result = _userService.EditEntity(user);
            if(result)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult DelUser(int id)
        {
            var user = _userService.LoadEntities(u => u.UserId == id).First();
            var result = _userService.DeleteEntity(user);
            if (result)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult GetUser(int id)
        {
            var user = _userService.LoadEntities(u => u.UserId == id).First();
            return Json(user);
        }
    }
}