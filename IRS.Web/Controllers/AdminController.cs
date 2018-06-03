using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IRS.BLL.Interface;
using IRS.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IRS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IPermissionService _permissionService;
        private IRolePermissionService _rolepermissionService;
        public AdminController(IUserService userService, IRoleService roleService, IPermissionService permissionService, IRolePermissionService rolepermissionService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult GetAdmin(int page, int pageSize, string UserName, string UserCode)
        {
            int total = 0;
            var users = _userService.LoadPageEntities(page, pageSize, out total, u => true, u => u.Validity, false);
            var role = _roleService.LoadEntities(r => true);
            if (users != null)
            {
                if (!string.IsNullOrEmpty(UserCode))
                {
                    users = users.Where(u => u.UserCode == UserCode);
                }
                if (!string.IsNullOrEmpty(UserName))
                {
                    users = users.Where(u => u.UserName == UserName);
                }
                var result = from u in users
                             join r in role on u.RoleId equals r.RoleId
                             select new
                             {
                                 u.UserId,
                                 u.UserName,
                                 u.Validity,
                                 u.RoleId,
                                 u.IsEnable,
                                 r.RoleName,
                                 u.UserCode
                             };
                return Json(new
                {
                    data = result,
                    total = total
                });
            }
            return Json(new
            {
                data = new Array[0],
                total = 0
            });
        }

        [HttpGet("{id}")]
        public ActionResult GetAdmin(int id)
        {
            var result = _userService.LoadEntities(r => r.UserId == id).FirstOrDefault();
            if (result != null)
            {
                return Json(new
                {
                    data = result
                });
            }
            return Content("no");
        }

        [HttpPost]
        public ActionResult AddAdmin([FromBody]User user)
        {
            var result = _userService.AddEntity(user);
            return Json(new
            {
                status_code = 200
            });
        }

        [HttpPut("{id}")]
        public ActionResult EditAdmin(int id, [FromBody]User user)
        {
            var result = _userService.LoadEntities(u => u.UserId == id).FirstOrDefault();
            if (result != null)
            {
                result.UserName = user.UserName;
                result.UserCode = user.UserCode;
                result.Validity = user.Validity;
                result.IsEnable = user.IsEnable;
                result.RoleId = user.RoleId;
                result.UserPwd = user.UserPwd;
                if (_userService.EditEntity(result))
                {
                    return Json(new
                    {
                        status_code = 200
                    });
                }
            }
            return Content("no");
        }

        [HttpDelete("{id}")]
        public ActionResult DelAdmin(int id)
        {
            var result = _userService.LoadEntities(u => u.UserId == id).FirstOrDefault();
            if (result != null)
            {
                if (_userService.DeleteEntity(result))
                {
                    return Json(new
                    {
                        status_code = 200
                    });
                }
            }
            return Content("no");
        }

        [HttpPost("reset/{id}")]
        public ActionResult ResetPsd(int id, string password)
        {
            var result = _userService.LoadEntities(u => u.UserId == id).FirstOrDefault();
            if (result != null)
            {
                result.UserPwd = password;
                if (_userService.EditEntity(result))
                {
                    return Json(new
                    {
                        status_code = 200
                    });
                }
            }
            return Content("no");
        }
    }
}