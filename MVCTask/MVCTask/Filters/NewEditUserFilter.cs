using System;
using System.Web.Mvc;

namespace MVCTask.Filters
{
    public class NewEditUserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sd = filterContext.HttpContext.Session["log"].ToString();
            filterContext.HttpContext.Session["log"] =
                string.Format("{0} new/edit", DateTime.Now) + " / " + sd;
            base.OnActionExecuting(filterContext);
        }
    }
}