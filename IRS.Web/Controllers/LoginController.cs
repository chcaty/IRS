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
    [Route("api/Login")]
    public class LoginController : BaseController
    {
        private IUserService _userService;
        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Login([FromBody]User user)
        {
            var result = _userService.LoadEntities(u => u.UserCode == user.UserCode && u.UserPwd == user.UserPwd).First();
            if(result != null)
            {
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetInt32("RoleId", user.RoleId);
                HttpContext.Session.SetString("UserCode", user.UserCode);
                HttpContext.Session.SetString("UserName", user.UserName);
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Content("out");
        }
    }
}