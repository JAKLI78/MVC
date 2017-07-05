using System;
using System.Linq;
using System.Web.Mvc;
using Castle.Core.Internal;
using MVCTask.Core.Interface;
using MVCTask.Data.Model;
using MVCTask.Models;

namespace MVCTask.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ICompanyService _companyService;
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
        public ActionResult Index()
        {
            return View(GetModelWithAllUsers());
        }

        [HttpPost]
        public ActionResult Find(UsersViewModel model)
        {
            var returnModel = new UsersViewModel {Search = model.Search};
            if (model.Search.IsNullOrEmpty())
            {
                returnModel.UserModels = GetModelWithAllUsers().UserModels;
            }
            else
            {
                var usersFromSearch = _searchService.FindUsers(model.Search);
                var users = usersFromSearch.Select(user => FillUser(user)).ToList();
                returnModel.UserModels = users;
            }

            return View("Index", returnModel);
        }

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
            var titels = _titelsServise.GetTitelsForUserById(user.Id);
            var strTitels = "";
            if (titels.Any())
                strTitels = $"( {titels.Aggregate((current, str) => current + ", " + str)} )";
            return new UserModel
            {
                Id = user.Id,
                BirthDate = user.BirthDate,
                CompanyName = companyName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                TitlesForView = strTitels
            };
        }

        private UsersViewModel GetModelWithAllUsers()
        {
            var someUser = _userService.GetUsers();
            var users = someUser.Select(user => FillUser(user)).ToList();
            return new UsersViewModel {UserModels = users};
        }
    }
}