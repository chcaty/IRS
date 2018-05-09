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
    [Route("api/Records")]
    public class RecordsController : BaseController
    {
        private IRecordService _recordService;
        public RecordsController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        public ActionResult GetRecords()
        {
            var recordList = _recordService.LoadEntities(r => true);
            return Json(recordList);
        }

        public ActionResult AddRecord(Record record)
        {
            var result = _recordService.AddEntity(record);
            return Content("ok");
        }

        public ActionResult GetRecord(int id)
        {
            var result = _recordService.LoadEntities(r => r.RecordId == id).First();
            if(result != null)
            {
                return Content("ok");
            }
            return Content("no");
        }
    }
}