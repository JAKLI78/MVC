using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Castle.Core.Internal;
using MVCTask.Core.Interface;
using MVCTask.Data.Model;
using MVCTask.Filters;
using MVCTask.Models;
using PagedList;

namespace MVCTask.Controllers
{
    [LogActionFilter]
    public class UsersController : BaseController
    {
        private readonly ICompanyService _companyService;

        private readonly int _maxCount = 5;
        private readonly ISearchService _searchService;

        private readonly ITitelsServise _titelsServise;
        private readonly IUserService _userService;


        public UsersController(ITitelsServise titelsServise, IUserService userService,
            ICompanyService companyService, ISearchService searchService)
        {
            if (titelsServise == null)
                throw new ArgumentNullException(nameof(titelsServise), $"{nameof(titelsServise)} cannot be null.");
            if (userService == null)
                throw new ArgumentNullException(nameof(userService), $"{nameof(userService)} cannot be null.");
            if (companyService == null)
                throw new ArgumentNullException(nameof(companyService), $"{nameof(companyService)} cannot be null.");
            if (searchService == null)
                throw new ArgumentNullException(nameof(searchService), $"{nameof(searchService)} cannot be null.");

            _companyService = companyService;
            _searchService = searchService;
            _userService = userService;
            _titelsServise = titelsServise;
        }


        // GET: Users
        public ActionResult Index(int? page)
        {
            var users = GetAllUsers();

            return View(new UsersViewModel
            {
                UserModels = (PagedList<UserModel>) users.ToPagedList(page.GetValueOrDefault(1), _maxCount)
            });
        }


        [HttpGet]
        public ActionResult Find(string req, int? page)
        {
            var returnModel = new UsersViewModel();
            if (req.IsNullOrEmpty())
            {
                var users =
                    returnModel.UserModels =
                        (PagedList<UserModel>) GetAllUsers().ToPagedList(page.GetValueOrDefault(1), _maxCount);
            }
            else
            {
                var usersFromSearch = _searchService.FindUsers(req);
                var users = usersFromSearch.Select(FillUser).ToList();
                returnModel.UserModels = (PagedList<UserModel>) users.ToPagedList(page.GetValueOrDefault(1), _maxCount);
            }

            return View("Index", returnModel);
        }

        [HttpPost]
        public ActionResult DeleteUser()
        {
            var userId = 0;
            foreach (var valuesKey in RouteData.Values.Keys)
                if (valuesKey.Equals("id")) userId = int.Parse(RouteData.Values[valuesKey].ToString());
            _userService.DeleteUser(userId);

            return RedirectToAction("Index");
        }

        private UserModel FillUser(User user)
        {
            var companyName = _companyService.GetCompanyNameById(user.CompanyId.GetValueOrDefault());
            var titels = _titelsServise.GetTitelsByUserId(user.Id);
            var strTitels = "";
            if (titels.Any())
                strTitels = $"( {titels.Aggregate((current, str) => current + ", " + str)} )";
            return new UserModel
            {
                Id = user.Id,
                BirthDate = user.BirthDate,
                CompanyName = companyName,
                Name = user.Name,
                Surname = user.Surname,
                TitlesForView = strTitels
            };
        }

        private List<UserModel> GetAllUsers()
        {
            var someUser = _userService.GetUsers();
            var users = someUser.Select(user => FillUser(user)).ToList();
            return users;
        }
    }
}