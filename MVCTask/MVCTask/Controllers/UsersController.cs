using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter;
using Castle.Core.Internal;
using MVCTask.Core.Interface;
using MVCTask.Data.Model;
using MVCTask.Models;
using PagedList;

namespace MVCTask.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ICompanyService _companyService;

        private readonly int _maxCount;
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
            _maxCount = int.Parse(ConfigurationManager.AppSettings["MaxCountOfUsersOnPage"]);
        }


        // GET: Users
        [HttpGet]
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

        public ActionResult NewUser(int? id)
        {
            var model = new NewEditUserModel
            {
                Surname = "",
                Name = "",
                Email = "",
                Title = new List<string> {""}
            };
            ViewBag.Title = "New user";
            if (id > 0)
            {
                var userToEdit = _userService.FindUserById(id.Value);
                var userTitels = _titelsServise.GetTitelsByUserId(id.Value);
                if (!userTitels.Any())
                    userTitels = new List<string> {""};
                ViewBag.Title = "Edit user";
                model = new NewEditUserModel
                {
                    Id = userToEdit.Id,
                    BirthDate = userToEdit.BirthDate,
                    Email = userToEdit.Email,
                    Name = userToEdit.Name,
                    Surname = userToEdit.Surname,
                    CompanyId = userToEdit.CompanyId.GetValueOrDefault(),
                    Title = (ICollection<string>) userTitels,
                    StrImage = userToEdit.FileUrl
                };
            }
            else
            {
                model.Surname = "";
                model.Name = "";
                model.Email = "";
                model.Title = new List<string> {""};
            }
            model.CompanyModels = BuildCompanyModelsForView();

            return View("NewEditUser", model);
        }

        [HttpPost]
        public ActionResult CreateUser(HttpPostedFileBase file, NewEditUserModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var path = "";
                if (!string.IsNullOrEmpty(model.StrImage))
                {
                    path = model.StrImage;
                }
                else if (file != null)
                {
                    var fil = Path.GetFileName(file.FileName);
                    path = Server.MapPath(ConfigurationManager.AppSettings["PhotoPath"] + fil);
                    file.SaveAs(path);
                }
                if (model.Id > 0)
                {
                    _userService.UpdateUser(model.Id, model.Name, model.Surname, model.Email,
                        model.BirthDate.GetValueOrDefault(),
                        model.CompanyId, path);
                    var oldCountOfTitels = _titelsServise.GetTitelsByUserId(model.Id);
                    foreach (var oldCountOfTitel in oldCountOfTitels)
                        _titelsServise.RemoveTitle(model.Id, oldCountOfTitel);
                    foreach (var s in model.Title)
                        if (s.Length > 0)
                            _titelsServise.CreateTitle(s, model.Id);
                }
                else
                {
                    _userService.CreateUser(model.Name, model.Surname, model.Email, model.BirthDate.GetValueOrDefault(),
                        model.CompanyId, path);
                    var uId = _userService.GetUsers().First(u => u.Email == model.Email).Id;
                    foreach (var s in model.Title)
                        _titelsServise.CreateTitle(s, uId);
                }

                return RedirectToAction("Index", "Users");
            }

            model.Title = new List<string> {""};
            model.CompanyModels = BuildCompanyModelsForView();
            ViewBag.Title = "New user";
            if (model.Id > 0)
                ViewBag.Title = "Edit user";

            return View("NewEditUser", model);
        }

        [HttpDelete]
        public ActionResult DeleteUser(int id)
        {
            _userService.DeleteUser(id);

            return Json(new {result = "Redirect", url = Url.Action("index", "Users")});
        }

        private UserModel FillUser(User user)
        {
            var companyName = _companyService.GetCompanyNameById(user.CompanyId.GetValueOrDefault());
            var titles = _titelsServise.GetTitelsByUserId(user.Id);
            var strTitels = "";
            if (titles.Any())
                strTitels = "(" + string.Join(", ", titles) + ")";
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

        private List<CompanyModel> BuildCompanyModelsForView()
        {
            List<CompanyModel> companyModels = new EditableList<CompanyModel>
            {
                new CompanyModel()
            };
            var companies = _companyService.GetCompanies();
            companyModels.AddRange(companies.Select(company => new CompanyModel
            {
                CompanyName = company.Name,
                Id = company.Id,
                MaxCountOfUsers = company.MaxCounOfUsers
            }));

            return companyModels;
        }
    }
}