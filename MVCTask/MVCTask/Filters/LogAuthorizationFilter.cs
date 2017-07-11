using System;
using System.Web.Mvc;

namespace MVCTask.Filters
{
    public class LogAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var sd = "";
            if (filterContext.HttpContext.Session["log"] != null)
            {
                sd = filterContext.HttpContext.Session["log"].ToString();
            }
            else
            {
                filterContext.HttpContext.Session.Add("log", string.Format("{0} Authorization  {1} ",
                                                                 DateTime.Now,
                                                                 filterContext.RequestContext.RouteData
                                                                     .Values["Action"]) + " \n ");
                return;
            }
            filterContext.HttpContext.Session["log"] = string.Format("{0} Authorization {1} ",
                                                           DateTime.Now,
                                                           filterContext.RequestContext.RouteData.Values["Action"]) +
                                                       " \n " + sd;
        }
    }
}