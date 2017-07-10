using System.Web.Mvc;
using System.Web.Routing;

namespace MVCTask
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new {controller = "Users", action = "Index", id = UrlParameter.Optional}
            );
            routes.MapRoute(
                "Search",
                "{controller}/{action}/{req}/{page}",
                new {controller = "Users", action = "Find", req = "", id = UrlParameter.Optional}
            );
        }
    }
}