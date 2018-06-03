using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRS.BLL.Interface;
using IRS.Common.Time;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IRS.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/chart")]
    public class ChartsController : Controller
    {
        private IRecordService _recordService;
        private ICategoryInfoService _categoryInfoService;
        public ChartsController(IRecordService recordService,ICategoryInfoService categoryInfoService)
        {
            _recordService = recordService;
            _categoryInfoService = categoryInfoService;
        }

        [HttpGet]
        public ActionResult GetChartData(string startdate, string enddate, string dormcheck, string faultcheck, string solvercheck, string usercheck)
        {
            var start = Convert.ToDateTime(startdate);
            var end = Convert.ToDateTime(enddate).AddDays(1);
            var categorylist = _categoryInfoService.LoadEntities(c=>true).OrderBy(c=>c.CategoryInfoOrder);
            List<Data> dormlist = new List<Data>();
            List<Data> faultlist = new List<Data>();
            List<Data> solverlist = new List<Data>();
            List<Data> userlist = new List<Data>();
            var recordlist = _recordService.LoadEntities(r => Convert.ToDateTime(r.RecordTime) >= start && Convert.ToDateTime(r.RecordTime) < end);
            var timelist = TimeMethod.GetDateList(startdate, enddate);
            var timecount = timelist.Count;
            var dormcategory = categorylist.Where(c => c.CategoryInfoType == 1);
            var dormcount = dormcategory.Count();
            var faultcategory = categorylist.Where(c => c.CategoryInfoType == 2);
            var faultcount = faultcategory.Count();
            var solvercategory = categorylist.Where(c => c.CategoryInfoType == 3);
            var solvercount = solvercategory.Count();
            List<string> checkList = new List<string>()
            {
                dormcheck,faultcheck,solvercheck,usercheck
            };
            if (checkList.Contains("宿舍楼报修分析"))
            {
                var list = from r in recordlist
                           join c in dormcategory on r.DormCategory equals c.CategoryInfoId
                           select new
                           {
                               dormCategory = c.CategoryInfoName,
                               Time = r.RecordTime.Substring(0, 10),
                               Id = r.RecordId
                           };
                for (int i = 0; i < dormcount; i++)
                {
                    List<int> countlist = new List<int>();
                    for (int j = 0; j < timecount; j++)
                    {
                        var a = from r in list where (r.Time == timelist[j] && r.dormCategory == dormcategory.ToList()[i].CategoryInfoName) select r;
                        if (a != null)
                        {
                            countlist.Add(a.Count());
                        }
                        else
                        {
                            countlist.Add(0);
                        }
                    }
                    dormlist.Add(new Data
                    {
                        DataName = dormcategory.ToList()[i].CategoryInfoName,
                        DataList = countlist
                    });
                }
            }
            if (checkList.Contains("处理情况分析"))
            {
                var list = from r in recordlist
                           join c in solvercategory on r.FaultResult equals c.CategoryInfoId
                           select new
                           {
                               dormCategory = c.CategoryInfoName,
                               Time = r.RecordTime.Substring(0, 10),
                               Id = r.RecordId
                           };
                for (int i = 0; i < solvercount; i++)
                {
                    List<int> countlist = new List<int>();
                    for (int j = 0; j < timecount; j++)
                    {
                        var a = from r in list where (r.Time == timelist[j] && r.dormCategory == solvercategory.ToList()[i].CategoryInfoName) select r;
                        if (a != null)
                        {
                            countlist.Add(a.Count());
                        }
                        else
                        {
                            countlist.Add(0);
                        }
                    }
                    solverlist.Add(new Data
                    {
                        DataName = solvercategory.ToList()[i].CategoryInfoName,
                        DataList = countlist
                    });
                }
            }
            if (checkList.Contains("故障情况分析"))
            {
                var list = from r in recordlist
                           join c in faultcategory on r.FaultCategory equals c.CategoryInfoId
                           select new
                           {
                               dormCategory = c.CategoryInfoName,
                               Time = r.RecordTime.Substring(0, 10),
                               Id = r.RecordId
                           };
                for (int i = 0; i < faultcount; i++)
                {
                    List<int> countlist = new List<int>();
                    for (int j = 0; j < timecount; j++)
                    {
                        var a = from r in list where (r.Time == timelist[j] && r.dormCategory == faultcategory.ToList()[i].CategoryInfoName) select r;
                        if (a != null)
                        {
                            countlist.Add(a.Count());
                        }
                        else
                        {
                            countlist.Add(0);
                        }
                    }
                    faultlist.Add(new Data
                    {
                        DataName = faultcategory.ToList()[i].CategoryInfoName,
                        DataList = countlist
                    });
                }
            }
            if (checkList.Contains("维修员维修分析"))
            {

            }

            return Json(new
            {
                dormlist,
                faultlist,
                solverlist,
                timelist,
                dormcategory = from c in dormcategory select c.CategoryInfoName,
                faultcategory = from c in faultcategory select c.CategoryInfoName,
                solvercategory = from c in solvercategory select c.CategoryInfoName,
            });
        }

        public class Data
        {
            public string DataName { get; set; }
            public List<int> DataList { get; set; }
        }
    }
}