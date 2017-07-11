using System.Data.Entity;
using MVCTask.Data.Interface;
using MVCTask.Data.Model;
using MVCTask.Data.Repositores;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace MVCTask.Core.Registers
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            For<IUserRepository>().Use<UserRepository>().LifecycleIs<UniquePerRequestLifecycle>();
            For<ICompanyRepository>().Use<CompanyRepository>().LifecycleIs<UniquePerRequestLifecycle>();
            For<ITitleRepository>().Use<TitleRepository>().LifecycleIs<UniquePerRequestLifecycle>();
            For<DbContext>().Use<DataContext>().LifecycleIs<UniquePerRequestLifecycle>();
        }
    }
}