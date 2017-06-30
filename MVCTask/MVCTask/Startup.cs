using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Owin;
using MVCTask;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace MVCTask
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    /*static class  MyClass
    {
        public static IHttpRoute MapHttpRoute(this HttpRouteCollection routes, string name, string routeTemplate, object defaults, object constraints, HttpMessageHandler handler)
        {
            if (routes == null)
                throw new ArgumentNullException(nameof(routes));
            HttpRouteValueDictionary routeValueDictionary1 = new HttpRouteValueDictionary(defaults);
            HttpRouteValueDictionary routeValueDictionary2 = new HttpRouteValueDictionary(constraints);
            IHttpRoute route = routes.CreateRoute(routeTemplate, (IDictionary<string, object>)routeValueDictionary1, (IDictionary<string, object>)routeValueDictionary2, (IDictionary<string, object>)null, handler);
            routes.Add(name, route);
            return route;
        }

        public static Route MapRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            if (url == null)
                throw new ArgumentNullException("url");
            Route route = new Route(url, (IRouteHandler)new MvcRouteHandler())
            {
                Defaults = RouteCollectionExtensions.CreateRouteValueDictionaryUncached(defaults),
                Constraints = RouteCollectionExtensions.CreateRouteValueDictionaryUncached(constraints),
                DataTokens = new RouteValueDictionary()
            };
            ConstraintValidation.Validate(route);
            if (namespaces != null && namespaces.Length > 0)
                route.DataTokens["Namespaces"] = (object)namespaces;
            routes.Add(name, (RouteBase)route);
            
            return route;
            
        }
    }*/
}