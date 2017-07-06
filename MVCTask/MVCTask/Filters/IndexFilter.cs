using System;
using System.Web.Mvc;

namespace MVCTask.Filters
{
    public class IndexFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} index", DateTime.Now) + " / ");
                base.OnActionExecuting(filterContext);
                return;
            }

            filterContext.HttpContext.Session["log"] =
                string.Format("{0} index", DateTime.Now) + " / " + sd;
        }
    }
}