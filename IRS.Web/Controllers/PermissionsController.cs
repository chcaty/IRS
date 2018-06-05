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
    [Route("api/permission")]
    public class PermissionsController : Controller
    {
        private IPermissionService _permissionService { get; set; }
        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public ActionResult GetPermissions(int page, int pageSize, string PermissionName, string PermissionRoute, string PermissionApi)
        {
            int total = 0;
            var record = _permissionService.LoadPageEntities(page, pageSize, out total, r => true, r => r.PermissionId, false);
            if (record != null)
            {
                if (!string.IsNullOrEmpty(PermissionName))
                {
                    record = record.Where(r => r.PermissionName == PermissionName);
                }
                if (!string.IsNullOrEmpty(PermissionRoute))
                {
                    record = record.Where(r => r.PermissionRoute == PermissionRoute);
                }
                if (!string.IsNullOrEmpty(PermissionApi))
                {
                    record = record.Where(r => r.PermissionApi.Contains(PermissionApi));
                }
                var result = from r in record
                             join p in record on r.parentId equals p.PermissionId into temp
                             from tt in temp.DefaultIfEmpty()
                             select new
                             {
                                 r.PermissionId,
                                 r.PermissionApi,
                                 r.PermissionDecs,
                                 r.PermissionName,
                                 r.PermissionRoute,
                                 r.parentId,
                                 ParentIdName = tt==null ?"顶级功能": tt.PermissionName
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

        [HttpGet("all")]
        public ActionResult GetPermissions()
        {
            int total = 0;
            var record = _permissionService.LoadEntities(p=>true);
            if (record != null)
            {
                return Json(new
                {
                    data = record,
                    total = total
                });
            }
            return Json(new
            {
                data = new Array[0],
                total = 0
            });
        }

        [HttpPost]
        public ActionResult AddPermission([FromBody]Permission permission)
        {
            var result = _permissionService.AddEntity(permission);
            return Json(new
            {
                status_code = 200
            });
        }

        [HttpGet("{id}")]
        public ActionResult GetPermission(int id)
        {
            var result = _permissionService.LoadEntities(r => r.PermissionId == id).FirstOrDefault();
            if (result != null)
            {
                return Json(new
                {
                    data = result
                });
            }
            return Content("no");
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePermission(int id)
        {
            var result = _permissionService.LoadEntities(r => r.PermissionId == id).FirstOrDefault();
            if (result != null)
            {
                if (_permissionService.DeleteEntity(result))
                {
                    return Json(new
                    {
                        status_code = 200
                    });
                }
            }
            return Content("no");
        }

        [HttpPut("{id}")]
        public ActionResult EditPermission(int id, [FromBody] Permission permission)
        {
            var result = _permissionService.LoadEntities(r => r.PermissionId == id).FirstOrDefault();
            if (result != null)
            {
                result.PermissionId = permission.PermissionId;
                result.PermissionName = permission.PermissionName;
                result.PermissionRoute = permission.PermissionRoute;
                result.PermissionApi = permission.PermissionApi;
                result.PermissionDecs = permission.PermissionDecs;
                if (_permissionService.EditEntity(result))
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