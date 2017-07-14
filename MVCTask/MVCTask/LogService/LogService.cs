using System.Collections.Generic;
using System.Web.Mvc;
using MVCTask.Interface;

namespace MVCTask.LogService
{
    public  class LogService :ILog
    {
        public  void Log(ControllerContext controllerContext, string message)
        {

            if (controllerContext.HttpContext.Session["log"] == null)
            {
                controllerContext.HttpContext.Session.Add("log", new List<string>());
            }                
            ((List<string>)controllerContext.HttpContext.Session["log"]).Add(message);
        }
    }
}