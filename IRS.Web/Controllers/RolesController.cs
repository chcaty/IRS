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
        private IPermissionService _permissionService;
        private IRolePermissionService _rolePermissionService;
        public RolesController(IRoleService roleService, IPermissionService permissionService, IRolePermissionService rolePermissionService)
        {
            _roleService = roleService;
            _permissionService = permissionService;
            _rolePermissionService = rolePermissionService;
        }

        [HttpGet]
        public ActionResult GetRoles()
        {
            var rolelist = _roleService.LoadEntities(r => true);
            return Json(new
            {
                data = rolelist
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

        [HttpGet("permission/{id}")]
        public ActionResult GetPermission(int id)
        {
            var result = _rolePermissionService.LoadEntities(rp => rp.RolesId == id);
            var perm = _permissionService.LoadEntities(p => true);
            var permission = from r in result
                             join p in perm on r.PermissionId equals p.PermissionId
                             select new
                             {
                                 r.PermissionId
                             };
            return Json(new
            {
                data = permission,
            });
        }

        [HttpPut("permission/{id}")]
        public ActionResult EditPermission(int id, [FromBody]Model model)
        {
            _rolePermissionService.DeleteEntityById(id);
            foreach (var i in model.Permission)
            {
                _rolePermissionService.AddEntity(new RolePermission { RolesId = id, PermissionId = Convert.ToInt32(i) });
            }
            return Json(new
            {
                status_code = 200
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
            var result = _roleService.LoadEntities(r => r.RoleId == id).FirstOrDefault();
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

        public class Model
        {
            public int[] Permission { get; set; }
        }
    }
}