using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTask.Controllers
{
    public class NewUserController : Controller
    {
        // GET: NewUser
        public ActionResult NewUser()
        {
            return View();
        }
    }
}