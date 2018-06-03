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
    [Route("api/solver")]
    public class SolverController : Controller
    {
        private ICategoryInfoService _categoryInfoService;
        public SolverController(ICategoryInfoService categoryInfoService)
        {
            _categoryInfoService = categoryInfoService;
        }

        [HttpGet]
        public ActionResult GetAddressInfos()
        {
            var addresslist = _categoryInfoService.LoadEntities(c => c.CategoryInfoType == 3);
            return Json(new
            {
                data = addresslist
            });
        }

        [HttpGet("{id}")]
        public ActionResult GetAddressInfo(int id)
        {
            var address = _categoryInfoService.LoadEntities(c => c.CategoryInfoId == id && c.CategoryInfoType == 3).FirstOrDefault();
            return Json(new
            {
                data = address
            });
        }

        [HttpPost]
        public ActionResult AddAddressInfo([FromBody] CategoryInfo categoryInfo)
        {
            var result = _categoryInfoService.AddEntity(categoryInfo);
            return Json(new
            {
                status_code = 200
            });
        }

        [HttpPut("{id}")]
        public ActionResult EditAddressInfo(int id, [FromBody] CategoryInfo categoryInfo)
        {
            var result = _categoryInfoService.LoadEntities(c => c.CategoryInfoId == id && c.CategoryInfoType == 3).FirstOrDefault();
            if (result != null && result != categoryInfo)
            {
                result.CategoryInfoEnable = categoryInfo.CategoryInfoEnable;
                result.CategoryInfoName = categoryInfo.CategoryInfoName;
                result.CategoryInfoOrder = categoryInfo.CategoryInfoOrder;
                result.StartFlag = categoryInfo.StartFlag;
                result.EndFlag = categoryInfo.EndFlag;
                if (_categoryInfoService.EditEntity(result))
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
        public ActionResult DeleteAddressInfo(int id)
        {
            var result = _categoryInfoService.LoadEntities(c => c.CategoryInfoId == id && c.CategoryInfoType == 3).FirstOrDefault();
            if (result != null)
            {
                if (_categoryInfoService.DeleteEntity(result))
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