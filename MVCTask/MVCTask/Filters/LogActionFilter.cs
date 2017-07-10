using System;
using System.Web.Mvc;

namespace MVCTask.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Action {1} starts",
                                                                 DateTime.Now,
                                                                 filterContext.ActionDescriptor.ActionName) + " \n ");
                return;
            }

            filterContext.HttpContext.Session["log"] = string.Format("{0} Action {1} starts",
                                                           DateTime.Now, filterContext.ActionDescriptor.ActionName) +
                                                       " \n " + sd;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Action {1} ends",
                                                                 DateTime.Now,
                                                                 filterContext.ActionDescriptor.ActionName) + " \n ");
                return;
            }

            filterContext.HttpContext.Session["log"] = string.Format("{0} Action {1} ends",
                                                           DateTime.Now, filterContext.ActionDescriptor.ActionName) +
                                                       " \n " + sd;
        }
    }
}