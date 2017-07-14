using System;
using System.Web.Mvc;
using MVCTask.Interface;
using StructureMap.Attributes;

namespace MVCTask.Filters
{
    public class LogAuthorizationFilter :AuthorizeAttribute, IAuthorizationFilter
    {
        [SetterProperty]
        public ILog _logService { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            _logService.Log(filterContext, string.Format("{0} Authorization {1} ",
                                               DateTime.Now,
                                               filterContext.RequestContext.RouteData.Values["Action"]) +
                                           " \n");
        }
    }
}