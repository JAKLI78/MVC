using System;
using System.Web.Mvc;
using MVCTask.Interface;
using StructureMap.Attributes;

namespace MVCTask.Filters
{
    public class LogActionFilter : ActionFilterAttribute, IActionFilter
    {
        [SetterProperty]
        public ILog _logService { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _logService.Log(filterContext, string.Format("{0} Action {1} starts",
                                               DateTime.Now, filterContext.ActionDescriptor.ActionName) +
                                           "\n");
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logService.Log(filterContext, string.Format("{0} Action {1} ends",
                                               DateTime.Now, filterContext.ActionDescriptor.ActionName) +
                                           "\n");
        }
    }
}