using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IRS.Web.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            String name = HttpContext.Session.GetString("UserCode");
            if (name == null)
            {
                //重定向到登录页面  
                HttpContext.Response.Redirect("Login/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}