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
            if (actionName.Equals("CreateUser"))
            {
                var transact = _context.Database.BeginTransaction();
                using (transact)
                {
                    try
                    {
                        base.InvokeAction(controllerContext, actionName);
                        transact.Commit();
                    }
                    catch (Exception e)
                    {
                        transact.Rollback();
                        throw;
                    }                    
                }
                return true;
            }
            if (actionName.Equals("DeleteUser"))
            {
                var transact = _context.Database.BeginTransaction();
                using (transact)
                {
                    try
                    {
                        base.InvokeAction(controllerContext, actionName);
                        transact.Commit();
                    }
                    catch (Exception e)
                    {
                        transact.Rollback();
                        throw;
                    }
                }
                return true;
            }

            return false;
        }
    }
}