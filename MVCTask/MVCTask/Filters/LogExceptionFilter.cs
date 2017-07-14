using System;
using System.Web.Mvc;
using MVCTask.Interface;
using StructureMap.Attributes;

namespace MVCTask.Filters
{
    public class LogExceptionFilter :FilterAttribute, IExceptionFilter
    {
        [SetterProperty]
        public ILog _logService { get; set; }

        public void OnException(ExceptionContext filterContext)
        {
            _logService.Log(filterContext, string.Format("{0} Exception {1} {2}",
                                               DateTime.Now,
                                               filterContext.RequestContext.RouteData.Values["Action"],
                                               filterContext.Exception.Message) +
                                           " \n ");
        }
    }
}