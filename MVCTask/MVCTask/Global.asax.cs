using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using MVCTask.Plumbing;

namespace MVCTask
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            BootstrapContanier();
        }
        private static void BootstrapContanier()
        {
            _container = new WindsorContainer();
            _container.Kernel.Resolver.AddSubResolver(new CollectionResolver(_container.Kernel));            
            var controllerFactory = new WindsorControllerFactory(_container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            _container.Install(FromAssembly.This());
            _container.Install(FromAssembly.Named("MVCTask.Core"));

        }

        protected void Application_End()
        {
            _container.Dispose();
        }

    }
}
