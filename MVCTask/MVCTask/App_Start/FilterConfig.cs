using System.Web.Mvc;
using MVCTask.DependencyResolution;
using MVCTask.Filters;

namespace MVCTask
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}