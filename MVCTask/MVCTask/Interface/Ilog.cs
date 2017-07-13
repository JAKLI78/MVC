using System.Web.Mvc;

namespace MVCTask.Interface
{
    public interface ILog
    {        
        void Log(ControllerContext context, string message);
    }
}