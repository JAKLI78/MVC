using System;
using System.Web.Mvc;

namespace MVCTask.Filters
{
    public class LogExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Exception  {1} {2}",
                                                                 DateTime.Now,
                                                                 filterContext.RequestContext.RouteData
                                                                     .Values["Action"],
                                                                 filterContext.Exception.Message) + " \n ");
                return;
            }
            filterContext.HttpContext.Session["log"] = string.Format("{0} Exception {1} {2}",
                                                           DateTime.Now,
                                                           filterContext.RequestContext.RouteData.Values["Action"],
                                                           filterContext.Exception.Message) +
                                                       " \n " + sd;
        }
    }
}