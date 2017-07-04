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
        private readonly ICompanyService _companyService;
        private readonly IImageService _imageService;
        private readonly ITitelsServise _titelsServise;
        private readonly IUserService _userService;

        public UsersController(ITitelsServise titelsServise, IImageService imageService, IUserService userService,
            ICompanyService companyService)
        {
            if (titelsServise == null)
                throw new ArgumentNullException(nameof(titelsServise), $"{nameof(titelsServise)} cannot be null.");

            if (userService == null)
                throw new ArgumentNullException(nameof(userService), $"{nameof(userService)} cannot be null.");
            if (companyService == null)
                throw new ArgumentNullException(nameof(companyService), $"{nameof(companyService)} cannot be null.");

            _companyService = companyService;
            _userService = userService;
            _titelsServise = titelsServise;
            _imageService = imageService;
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = new List<UserModel>();

            foreach (var user in _userService.GetUsers())
            {
                var companyName = _companyService.GetCompanyNameById(user.CompanyId.GetValueOrDefault());
                var titels = _titelsServise.GetTitelsForUserById(user.Id);
                var strTitels = "";
                if (titels.Any())
                    strTitels = $"( {titels.Aggregate((current, str) => current + ", " + str)} )";
                if (user.FileUrl.Length > 0)

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
            foreach (var user in _userService.GetUsers())
            {
                var companyName = _companyService.GetCompanyNameById(user.CompanyId.GetValueOrDefault());
                var titels = _titelsServise.GetTitelsForUserById(user.Id);
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
            _userService.DeleteUser(userId);
            return RedirectToAction("Index");
        }
    }
}