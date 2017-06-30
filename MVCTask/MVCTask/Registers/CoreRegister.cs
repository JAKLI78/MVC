using MVCTask.Core.Interface;
using MVCTask.Core.Services;
using StructureMap.Configuration.DSL;

namespace MVCTask.Installers
{
    public class CoreRegister : Registry
    {
        public CoreRegister()
        {
            For<ITestServise>().Use<TestService>();
        }
    }
}