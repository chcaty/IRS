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
    [Route("api/permissions")]
    public class PermissionsController : Controller
    {
        private IPermissionService _permissionService { get; set; }
        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        //[HttpGet]
        //public ActionResult GetRecords(int page, int pageSize, string RecordUser, string UserDorm, string RecordTime)
        //{
        //    int total = 0;
        //    var record = _permissionService.LoadPageEntities(page, pageSize, out total, r => true, r => r.PermissionId, false);
        //    if (record != null)
        //    {
        //        if (!string.IsNullOrEmpty(RecordUser))
        //        {
        //            record = record.Where(r => r.RecordUser == RecordUser);
        //        }
        //        if (!string.IsNullOrEmpty(UserDorm))
        //        {
        //            record = record.Where(r => r.UserDorm == UserDorm);
        //        }
        //        if (!string.IsNullOrEmpty(RecordTime))
        //        {
        //            record = record.Where(r => r.RecordTime.Contains(RecordTime));
        //        }
        //        var result = from r in record
        //                     join ac in category on r.DormCategory equals ac.CategoryInfoId
        //                     join fc in category on r.FaultCategory equals fc.CategoryInfoId
        //                     join rc in category on r.FaultResult equals rc.CategoryInfoId
        //                     select new
        //                     {
        //                         r.RecordId,
        //                         r.RecordUser,
        //                         r.DormCategory,
        //                         r.UserPhone,
        //                         DormCategoryName = ac.CategoryInfoName,
        //                         r.UserDorm,
        //                         r.RecordTime,
        //                         r.FaultCategory,
        //                         FaultCategoryName = fc.CategoryInfoName,
        //                         r.FaultDesc,
        //                         r.FaultResult,
        //                         FaultResultName = rc.CategoryInfoName,
        //                     };
        //        return Json(new
        //        {
        //            data = result,
        //            total = total
        //        });
        //    }
        //    return Json(new
        //    {
        //        data = new Array[0],
        //        total = 0
        //    });
        //}

        //[HttpPost]
        //public ActionResult AddRecord([FromBody]Record record)
        //{
        //    record.RecordTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //    record.FaultResult = _categoryInfoService.LoadEntities(c => c.CategoryInfoType == 3 && c.StartFlag == 1).FirstOrDefault().CategoryInfoId;
        //    var result = _recordService.AddEntity(record);
        //    return Json(new
        //    {
        //        status_code = 200
        //    });
        //}

        //[HttpGet("{id}")]
        //public ActionResult GetRecord(int id)
        //{
        //    var result = _recordService.LoadEntities(r => r.RecordId == id).FirstOrDefault();
        //    if (result != null)
        //    {
        //        return Json(new
        //        {
        //            data = result
        //        });
        //    }
        //    return Content("no");
        //}

        //[HttpDelete("{id}")]
        //public ActionResult DeleteRecord(int id)
        //{
        //    var result = _recordService.LoadEntities(r => r.RecordId == id).FirstOrDefault();
        //    if (result != null)
        //    {
        //        if (_recordService.DeleteEntity(result))
        //        {
        //            return Json(new
        //            {
        //                status_code = 200
        //            });
        //        }
        //    }
        //    return Content("no");
        //}

        //[HttpPut("{id}")]
        //public ActionResult EditRecord(int id, [FromBody] Record record)
        //{
        //    var result = _recordService.LoadEntities(r => r.RecordId == id).FirstOrDefault();
        //    if (result != null)
        //    {
        //        result.RecordUser = record.RecordUser;
        //        result.UserPhone = record.UserPhone;
        //        result.RecordTime = record.RecordTime;
        //        result.FaultCategory = record.FaultCategory;
        //        result.FaultDesc = record.FaultDesc;
        //        result.DormCategory = record.DormCategory;
        //        result.UserDorm = record.UserDorm;
        //        result.FaultResult = record.FaultResult;
        //        if (_recordService.EditEntity(result))
        //        {
        //            return Json(new
        //            {
        //                status_code = 200
        //            });
        //        }
        //    }
        //    return Content("no");
        //}
    }
}