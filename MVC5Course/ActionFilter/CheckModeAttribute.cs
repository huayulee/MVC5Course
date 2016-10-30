using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class CheckWhereAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.whereAmi = string.Empty;
            
            if (filterContext.HttpContext.Request.IsLocal)
            {
                filterContext.Controller.ViewBag.whereAmi = filterContext.HttpContext.Request.Url;
            }

        }
    }
}