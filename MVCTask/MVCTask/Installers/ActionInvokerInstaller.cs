using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MVCTask.Installers
{
    public class ActionInvokerInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IActionInvoker>().ImplementedBy<Controllers.MVCTaskActionInvoker>()
                .LifestylePerWebRequest());
        }
    }
}