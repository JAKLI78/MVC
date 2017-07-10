using System.Web.Mvc;
using MVCTask.Filters;

namespace MVCTask
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogActionFilter());
            filters.Add(new LogResultFilter());
            filters.Add(new LogAuthorizationFilter());
            filters.Add(new LogExceptionFilter());
        }
    }
}