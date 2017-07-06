using System.Web.Mvc;
using MVCTask.Filters;

namespace MVCTask
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new SearchFilter());
            filters.Add(new DeleteFilter());
            filters.Add(new IndexFilter());
            filters.Add(new NewEditUserFilter());
            filters.Add(new CreateEditFilter());
        }
    }
}