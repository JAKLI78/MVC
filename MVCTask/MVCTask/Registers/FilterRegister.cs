using System.Web.Mvc;
using MVCTask.Filters;
using StructureMap.Configuration.DSL;

namespace MVCTask.Registers
{
    public class FilterRegister : Registry
    {
        public FilterRegister()
        {
            For<IActionFilter>().Use<LogActionFilter>();
            For<IAuthorizationFilter>().Use<LogAuthorizationFilter>();
            For<IExceptionFilter>().Use<LogExceptionFilter>();
            For<IResultFilter>().Use<LogResultFilter>();
        }
    }
}