using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class LocalDebugOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }
}