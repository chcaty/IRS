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
    [Route("api/Roles")]
    public class RolesController : BaseController
    {
        private IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult GetRoles()
        {
            var roleList = _roleService.LoadEntities(r => true);
            return Json(roleList);
        }

        [HttpPost]
        public ActionResult AddRole([FromBody]Role role)
        {
            var result = _roleService.AddEntity(role);
            return Content("ok");
        }

        public ActionResult EditRole([FromBody]Role role)
        {
            var result = _roleService.EditEntity(role);
            if (result)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult DelRole(int id)
        {
            var role = _roleService.LoadEntities(r => r.RoleId == id).First();
            var result = _roleService.DeleteEntity(role);
            if (result)
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }

        public ActionResult GetRole(int id)
        {
            var role = _roleService.LoadEntities(r => r.RoleId == id).First();
            return Json(role);
        }
    }
}