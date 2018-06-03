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
    [Route("api/record")]
    public class RecordsController : Controller
    {
        private IRecordService _recordService;
        private ICategoryInfoService _categoryInfoService;
        private IProcessingRecordService _processingRecordService;
        public RecordsController(IRecordService recordService, ICategoryInfoService categoryInfoService,
            IProcessingRecordService processingRecordService)
        {
            _recordService = recordService;
            _categoryInfoService = categoryInfoService;
            _processingRecordService = processingRecordService;
        }

        [HttpGet]
        public ActionResult GetRecords(int page, int pageSize, string RecordUser, string UserDorm, string RecordTime)
        {
            int total = 0;
            var record = _recordService.LoadPageEntities(page, pageSize, out total, r => true, r => r.RecordTime, false);
            var category = _categoryInfoService.LoadEntities(c => true);
            if (record != null)
            {
                if (!string.IsNullOrEmpty(RecordUser))
                {
                    record = record.Where(r => r.RecordUser == RecordUser);
                }
                if (!string.IsNullOrEmpty(UserDorm))
                {
                    record = record.Where(r => r.UserDorm == UserDorm);
                }
                if (!string.IsNullOrEmpty(RecordTime))
                {
                    record = record.Where(r => r.RecordTime.Contains(RecordTime));
                }
                var result = from r in record
                             join ac in category on r.DormCategory equals ac.CategoryInfoId
                             join fc in category on r.FaultCategory equals fc.CategoryInfoId
                             join rc in category on r.FaultResult equals rc.CategoryInfoId
                             select new
                             {
                                 r.RecordId,
                                 r.RecordUser,
                                 r.DormCategory,
                                 r.UserPhone,
                                 DormCategoryName = ac.CategoryInfoName,
                                 r.UserDorm,
                                 r.RecordTime,
                                 r.FaultCategory,
                                 FaultCategoryName = fc.CategoryInfoName,
                                 r.FaultDesc,
                                 r.FaultResult,
                                 FaultResultName = rc.CategoryInfoName,
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

        [HttpGet("All")]
        public ActionResult GetRecordAll(string RecordUser, string UserDorm, string RecordTime)
        {
            var record = _recordService.LoadEntities(r => true);
            var category = _categoryInfoService.LoadEntities(c => true);
            if (record != null)
            {
                if (!string.IsNullOrEmpty(RecordUser))
                {
                    record = record.Where(r => r.RecordUser == RecordUser);
                }
                if (!string.IsNullOrEmpty(UserDorm))
                {
                    record = record.Where(r => r.UserDorm == UserDorm);
                }
                if (!string.IsNullOrEmpty(RecordTime))
                {
                    record = record.Where(r => r.RecordTime.Contains(RecordTime));
                }
                var result = from r in record
                             join ac in category on r.DormCategory equals ac.CategoryInfoId
                             join fc in category on r.FaultCategory equals fc.CategoryInfoId
                             join rc in category on r.FaultResult equals rc.CategoryInfoId
                             select new
                             {
                                 r.RecordId,
                                 r.RecordUser,
                                 r.DormCategory,
                                 r.UserPhone,
                                 DormCategoryName = ac.CategoryInfoName,
                                 r.UserDorm,
                                 r.RecordTime,
                                 r.FaultCategory,
                                 FaultCategoryName = fc.CategoryInfoName,
                                 r.FaultDesc,
                                 r.FaultResult,
                                 FaultResultName = rc.CategoryInfoName,
                             };
                return Json(new
                {
                    data = result.OrderByDescending(c => c.RecordTime)
                });
            }
            return Json(new
            {
                data = new Array[0],
                total = 0
            });

        }

        [HttpPost]
        public ActionResult AddRecord([FromBody]Record record)
        {
            record.RecordTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            record.FaultResult = _categoryInfoService.LoadEntities(c => c.CategoryInfoType == 3 && c.StartFlag == 1).FirstOrDefault().CategoryInfoId;
            var result = _recordService.AddEntity(record);
            return Json(new
            {
                status_code = 200
            });
        }

        [HttpGet("{id}")]
        public ActionResult GetRecord(int id)
        {
            var result = _recordService.LoadEntities(r => r.RecordId == id);
            var category = _categoryInfoService.LoadEntities(c => true);
            var data = from r in result
                       join ac in category on r.DormCategory equals ac.CategoryInfoId
                       join fc in category on r.FaultCategory equals fc.CategoryInfoId
                       join rc in category on r.FaultResult equals rc.CategoryInfoId
                       select new
                       {
                           r.RecordId,
                           r.RecordUser,
                           r.DormCategory,
                           r.UserPhone,
                           DormCategoryName = ac.CategoryInfoName,
                           r.UserDorm,
                           r.RecordTime,
                           r.FaultCategory,
                           FaultCategoryName = fc.CategoryInfoName,
                           r.FaultDesc,
                           r.FaultResult,
                           FaultResultName = rc.CategoryInfoName,
                       };
            if (data != null)
            {
                return Json(new
                {
                    data = data.FirstOrDefault(),
                });
            }
            return Content("no");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteRecord(int id)
        {
            var result = _recordService.LoadEntities(r => r.RecordId == id).FirstOrDefault();
            if (result != null)
            {
                if (_recordService.DeleteEntity(result))
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
        public ActionResult EditRecord(int id, [FromBody] Record record)
        {
            var result = _recordService.LoadEntities(r => r.RecordId == id).FirstOrDefault();
            if (result != null)
            {
                result.RecordUser = record.RecordUser;
                result.UserPhone = record.UserPhone;
                result.RecordTime = record.RecordTime;
                result.FaultCategory = record.FaultCategory;
                result.FaultDesc = record.FaultDesc;
                result.DormCategory = record.DormCategory;
                result.UserDorm = record.UserDorm;
                result.FaultResult = record.FaultResult;
                if (_recordService.EditEntity(result))
                {
                    return Json(new
                    {
                        status_code = 200
                    });
                }
            }
            return Content("no");
        }

        [HttpGet("process/{id}")]
        public ActionResult GetProcess(int id)
        {
            var process = _processingRecordService.LoadEntities(r => r.RecordId == id);
            var category = _categoryInfoService.LoadEntities(c => true);
            if (process != null)
            {
                var result = from p in process
                             join c in category on p.ProcessingResult equals c.CategoryInfoId
                             select new
                             {
                                 p.ProcessingDesc,
                                 p.ProcessingPeople,
                                 p.ProcessingRecordId,
                                 p.ProcessingResult,
                                 p.ProcessingTime,
                                 ProcessingResultName = c.CategoryInfoName
                             };
                return Json(new
                {
                    data = result.OrderByDescending(p => p.ProcessingTime)
                });
            }
            return Content("no");
        }

        [HttpPost("process")]
        public ActionResult AddProcess([FromBody] ProcessingRecord record)
        {
            var result = _processingRecordService.AddEntity(record);
            var recordInfo = _recordService.LoadEntities(r => r.RecordId == record.RecordId).FirstOrDefault();
            recordInfo.FaultResult = record.ProcessingResult;
            _recordService.EditEntity(recordInfo);
            return Json(new
            {
                status_code = 200
            });
        }


    }
}