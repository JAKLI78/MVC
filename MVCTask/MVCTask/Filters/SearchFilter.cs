using System;
using System.Web.Mvc;
using MVCTask.Interface;

namespace MVCTask.Filters
{
    public class SearchFilter : ActionFilterAttribute, IActionFilter
    {
        public ILogService LogService { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sd = filterContext.HttpContext.Session["log"].ToString();
            filterContext.HttpContext.Session["log"] =
                string.Format("{0} search", DateTime.Now) + " / " + sd;
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}