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
            For<IUserRepository>().Use<UserRepository>().LifecycleIs<TransientLifecycle>();
            For<ICompanyRepository>().Use<CompanyRepository>().LifecycleIs<TransientLifecycle>();
            For<ITitleRepository>().Use<TitleRepository>().LifecycleIs<TransientLifecycle>();
            For<DbContext>().Use<DataContext>().LifecycleIs<TransientLifecycle>();
        }
    }
}