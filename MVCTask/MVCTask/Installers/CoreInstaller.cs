using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MVCTask.Core.Interface;
using MVCTask.Core.Class;

namespace MVCTask.Installers
{
    public class CoreInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITestServise>().ImplementedBy<TestService>().LifestylePerWebRequest());
        }
    }
}