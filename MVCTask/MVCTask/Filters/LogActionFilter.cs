using System;
using System.Web.Mvc;

namespace MVCTask.Filters
{
    public class LogActionFilter : ActionFilterAttribute
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
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Action {1} starts",
                    DateTime.Now,filterContext.ActionDescriptor.ActionName) + " / ");                
                return;
            }

            filterContext.HttpContext.Session["log"] = string.Format("{0} Action {1} starts",
                DateTime.Now,filterContext.ActionDescriptor.ActionName) + " / " + sd;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Action {1} ends",
                                                                 DateTime.Now, filterContext.ActionDescriptor.ActionName) + " / ");
                return;
            }

            filterContext.HttpContext.Session["log"] = string.Format("{0} Action {1} ends",
                                                           DateTime.Now, filterContext.ActionDescriptor.ActionName) + " / " + sd;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Action {1} result  starts",
                                                                 DateTime.Now, filterContext.RequestContext.RouteData.Values["Action"]) + " / ");
                return;
            }

            filterContext.HttpContext.Session["log"] = string.Format("{0} Action {1} result starts",
                                                           DateTime.Now, filterContext.RequestContext.RouteData.Values["Action"]) + " / " + sd;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Action {1} result ends",
                                                                 DateTime.Now, filterContext.RequestContext.RouteData.Values["Action"]) + " / ");
                return;
            }

            filterContext.HttpContext.Session["log"] = string.Format("{0} Action {1} result ends",
                                                           DateTime.Now, filterContext.RequestContext.RouteData.Values["Action"]) + " / " + sd;
        }
    }
}