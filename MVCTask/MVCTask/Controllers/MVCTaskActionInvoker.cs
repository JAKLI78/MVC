using System;
using System.Data.Entity;
using System.Web.Mvc;

namespace MVCTask.Controllers
{
    public class MVCTaskActionInvoker:ControllerActionInvoker
    {
        private DbContext _context;

        public MVCTaskActionInvoker(DbContext context)
        {
            if (context==null)
            {
                throw new ArgumentNullException("context");
            }
            _context = context;
        }

        public override bool InvokeAction(ControllerContext controllerContext, string actionName)
        {
            if (actionName.Equals("Index"))
            {
                base.InvokeAction(controllerContext, actionName);
                return true;
            }
            if (actionName.Equals("NewUser"))
            {
                base.InvokeAction(controllerContext, actionName);
                return true;
            }
            

            return false;
        }
    }
}