using System.Data.Entity;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MVCTask.Data.Class;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;

namespace MVCTask.Core.Installers
{
     public class DataInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<DbContext>().ImplementedBy<DataContext>().LifestylePerWebRequest());
            container.Register(Component.For<ICompanyRepository>().ImplementedBy<CompanyRepository>()
                .LifestylePerWebRequest());
            container.Register(
                Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifestylePerWebRequest());
            container.Register(Component.For<ITitleRepository>().ImplementedBy<TitleRepository>()
                .LifestylePerWebRequest());

        }
    }
}
