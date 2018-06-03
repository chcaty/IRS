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
    [Route("api/role")]
    public class RolesController : Controller
    {
        private IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public ActionResult GetRoles()
        {
            var addresslist = _roleService.LoadEntities(r => true);
            return Json(new
            {
                data = addresslist
            });
        }

        [HttpGet("{id}")]
        public ActionResult GetRole(int id)
        {
            var address = _roleService.LoadEntities(r => r.RoleId == id).FirstOrDefault();
            return Json(new
            {
                data = address
            });
        }

        [HttpPost]
        public ActionResult AddRole([FromBody] Role role)
        {
            var result = _roleService.AddEntity(role);
            return Json(new
            {
                status_code = 200
            });
        }

        [HttpPut("{id}")]
        public ActionResult EditRole(int id, [FromBody] Role role)
        {
            var result = _roleService.LoadEntities(r => r.RoleId== id).FirstOrDefault();
            if (result != null && result != role)
            {
                result.RoleName = role.RoleName;
                result.RoleDecs = role.RoleDecs;
                if (_roleService.EditEntity(result))
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
        public ActionResult DeleteRole(int id)
        {
            var result = _roleService.LoadEntities(r => r.RoleId == id).FirstOrDefault();
            if (result != null)
            {
                if (_roleService.DeleteEntity(result))
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