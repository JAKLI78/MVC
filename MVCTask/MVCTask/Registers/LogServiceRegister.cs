using MVCTask.Interface;
using StructureMap.Configuration.DSL;

namespace MVCTask.Registers
{
    public class LogServiceRegister : Registry
    {
        public LogServiceRegister()
        {
            For<ILog>().Use<LogService.LogService>();
        }
    }
}