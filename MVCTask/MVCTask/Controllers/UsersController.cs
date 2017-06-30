using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVCTask.Core.Interface;
using MVCTask.Models;

namespace MVCTask.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ITestServise _servise;

        public UsersController(ITestServise servise)
        {
            if (servise == null)
                throw new ArgumentNullException(nameof(servise), $"{nameof(servise)} cannot be null.");
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
                if (titels.Any())
                    strTitels = $"( {titels.Aggregate((current, str) => current + ", " + str)} )";

                users.Add(new UserModel
                {
                    Id = user.Id,
                    BirthDate = user.BirthDate,
                    CompanyName = companyName,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    TitlesForView = strTitels
                });
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
                if (titels.Any())
                    strTitels = $"( {titels.Aggregate((current, str) => current + ", " + str)} )";
                users.Add(new UserModel
                {
                    Id = user.Id,
                    BirthDate = user.BirthDate,
                    CompanyName = companyName,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    TitlesForView = strTitels
                });
            }
            return View("Index", users);
        }

        public ActionResult DeleteUser()
        {
            var userId = 0;
            foreach (var valuesKey in RouteData.Values.Keys)
                if (valuesKey.Equals("id")) userId = int.Parse(RouteData.Values[valuesKey].ToString());
            _servise.DeleteUser(userId);
            return RedirectToAction("Index");
        }
    }
}