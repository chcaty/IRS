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
    [Route("api/menu")]
    public class MenuController : Controller
    {
        private ICategoryInfoService _categoryInfoService;
        private IRoleService _roleService;
        public MenuController(ICategoryInfoService categoryInfoService,IRoleService roleService)
        {
            _categoryInfoService = categoryInfoService;
            _roleService = roleService;
        }
        
        [HttpGet("{type}")]
        public ActionResult GetMenu(int type)
        {
            var addresslist = _categoryInfoService.LoadEntities(c => c.CategoryInfoType == type && c.CategoryInfoEnable == 1);
            return Json(new
            {
                data = addresslist
            });
        }
        
        [HttpGet("endflag/{type}")]
        public ActionResult GetEndFlag(int type)
        {
            var address = _categoryInfoService.LoadEntities(c => c.CategoryInfoType == type && c.CategoryInfoEnable == 1 && c.EndFlag == 1).FirstOrDefault();
            return Json(new
            {
                data =address.CategoryInfoId
            });
        }

        [HttpGet("roles")]
        public ActionResult GetRoles()
        {
            var roles = _roleService.LoadEntities(r => true);
            return Json(new
            {
                data = roles
            });
        }
    }
}