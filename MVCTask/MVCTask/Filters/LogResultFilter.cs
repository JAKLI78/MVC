using System;
using System.Web.Mvc;

namespace MVCTask.Filters
{
    public class LogResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Action {1} result  starts",
                                                                 DateTime.Now,
                                                                 filterContext.RequestContext.RouteData
                                                                     .Values["Action"]) + " \n ");
                return;
            }
            filterContext.HttpContext.Session["log"] = string.Format("{0} Action {1} result starts",
                                                           DateTime.Now,
                                                           filterContext.RequestContext.RouteData.Values["Action"]) +
                                                       " \n " + sd;
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Action {1} result ends",
                                                                 DateTime.Now,
                                                                 filterContext.RequestContext.RouteData
                                                                     .Values["Action"]) + " \n ");
                return;
            }
            filterContext.HttpContext.Session["log"] = string.Format("{0} Action {1} result ends",
                                                           DateTime.Now,
                                                           filterContext.RequestContext.RouteData.Values["Action"]) +
                                                       " \n " + sd;
        }
    }
}