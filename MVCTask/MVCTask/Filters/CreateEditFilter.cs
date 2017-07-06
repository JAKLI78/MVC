using System;
using System.Web.Mvc;

namespace MVCTask.Filters
{
    public class CreateEditFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sd = filterContext.HttpContext.Session["log"].ToString();
            filterContext.HttpContext.Session["log"] =
                string.Format("{0} creat/edit", DateTime.Now) + " / " + sd;
            base.OnActionExecuting(filterContext);
        }
    }
}