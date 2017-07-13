using System.Web.Mvc;
using MVCTask.Interface;

namespace MVCTask.LogService
{
    public  class LogService :ILog
    {
        public  void Log(ControllerContext controllerContext, string message)
        {
            var result = "";
            if (controllerContext.HttpContext.Session["log"] != null)
                result = controllerContext.HttpContext.Session["log"].ToString();
            else
                controllerContext.HttpContext.Session.Add("log", "");
             
            controllerContext.HttpContext.Session["log"] = message + result;
        }
    }
}