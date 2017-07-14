using System;
using System.Web.Mvc;
using MVCTask.Interface;
using StructureMap.Attributes;

namespace MVCTask.Filters
{
    public class LogResultFilter :ActionFilterAttribute, IResultFilter
    {
        [SetterProperty]
        public ILog _logService { get; set; }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _logService.Log(filterContext, string.Format("{0} Action {1} result starts",
                                               DateTime.Now,
                                               filterContext.RequestContext.RouteData.Values["Action"]) +
                                           "\n");
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _logService.Log(filterContext, string.Format("{0} Action {1} result ends",
                                               DateTime.Now,
                                               filterContext.RequestContext.RouteData.Values["Action"]) +
                                           "\n");
        }
    }
}