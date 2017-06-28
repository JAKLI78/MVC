using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCTask.Core.Interface;
using MVCTask.Models;
using MVCTask.Data;

namespace MVCTask.Controllers
{
    public class UsersController : BaseController
    {
        private ITestServise _servise;

        public UsersController(ITestServise servise)
        {
            if (servise == null)
            {
                throw new ArgumentNullException("servise");
            }
            _servise = servise;
        }
        // GET: Users
        public ActionResult Index()
        {
            var users = new List<UserModel>();
            foreach (var user in _servise.GetUsers())
            {
                var companyName = _servise.GetCompanyNameById(user.CompanyId.GetValueOrDefault());
                var titels = _servise.GetTitelsForUserById(user.Id);
                var strTitels = "";
                if (titels.Count()>0)
                {
                    strTitels = string.Format("( {0} )", titels.Aggregate((current, str) => current + ", " + str));
                }
                
                users.Add(new UserModel(){Id = user.Id,BirthDate = user.BirthDate,CompanyName = companyName,Email = user.Email,Name = user.Name,Surname = user.Surname,Title = strTitels});
            }
            return View(users);
        }

        public ActionResult Find(FormCollection formCollection)
        {
            var users = new List<UserModel>();
            foreach (var user in _servise.GetUsers())
            {
                var companyName = _servise.GetCompanyNameById(user.CompanyId.GetValueOrDefault());
                var titels = _servise.GetTitelsForUserById(user.Id);
                var strTitels = "";
                if (titels.Count() > 0)
                {
                    strTitels = string.Format("( {0} )", titels.Aggregate((current, str) => current + ", " + str));
                }
                users.Add(new UserModel() { Id = user.Id, BirthDate = user.BirthDate, CompanyName = companyName, Email = user.Email, Name = user.Name, Surname = user.Surname, Title = strTitels });
            }
            return View("Index", users);
        }

        public ActionResult NewUser()
        {
            return View();
        }

        public ActionResult EditUser()
        {
            return View();
        }

        public ActionResult DeleteUser()
        {
            //TODO: deleteing user
            return View("Index");
        }
    }
}