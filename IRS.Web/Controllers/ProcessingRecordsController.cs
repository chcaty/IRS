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
    [Route("api/ProcessingRecords")]
    public class ProcessingRecordsController : BaseController
    {
        private IProcessingRecordService _processingrecordService;
        public ProcessingRecordsController(IProcessingRecordService processingrecordService)
        {
            _processingrecordService = processingrecordService;
        }

        public ActionResult GetProcessingRecords()
        {
            var list = _processingrecordService.LoadEntities(pr => true);
            return Json(list);
        }

        public ActionResult AddProcessingRecord(ProcessingRecord record)
        {
            var result = _processingrecordService.AddEntity(record);
            return Content("ok");
        }

        public ActionResult EditProcessingRecord(ProcessingRecord record)
        {
            var result = _processingrecordService.EditEntity(record);
            if (result)
            {
                return Content("ok");
            }
            return Content("no");
        }

    }
}