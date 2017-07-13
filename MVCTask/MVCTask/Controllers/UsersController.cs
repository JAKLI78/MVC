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
using MVCTask.Interface;
using MVCTask.Models;
using PagedList;

namespace MVCTask.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ICompanyService _companyService;

        private readonly int _maxCount;
        private readonly ISearchService _searchService;

        private readonly ITitlesServise _titelsServise;
        private readonly IUserService _userService;

        public IConfig ConfigManager { get; set; }


        public UsersController(ITitlesServise titelsServise, IUserService userService,
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
                UserModels = 
                (PagedList<UserModel>) users.ToPagedList(page.GetValueOrDefault(1), _maxCount)
            });
        }


        [HttpGet]
        public ActionResult Find(string search, int? page)
        {
            ViewBag.Search = search;
            var returnModel = new UsersViewModel();
            if (search.IsNullOrEmpty())
            {
                returnModel.UserModels =
                    (PagedList<UserModel>) GetAllUsers().ToPagedList(page.GetValueOrDefault(1), _maxCount);
            }
            else
            {
                var usersFromSearch = _searchService.FindUsers(search);
                var users = usersFromSearch.Select(FillUser).ToList();
                returnModel.UserModels = 
                    (PagedList<UserModel>) users.ToPagedList(page.GetValueOrDefault(1), _maxCount);
            }

            return View("Index", returnModel);
        }

        public ActionResult NewUser(int? id)
        {
            var model = new NewEditUserModel();
     
            if (id > 0)
            {
                var userToEdit = _userService.FindUserByIdWithTitles(id.Value);
                
                ViewBag.Title = "Edit user";
                model = new NewEditUserModel
                {
                    Id = userToEdit.Id,
                    BirthDate = userToEdit.BirthDate,
                    Email = userToEdit.Email,
                    Name = userToEdit.Name,
                    Surname = userToEdit.Surname,
                    CompanyId = userToEdit.CompanyId.GetValueOrDefault(),
                    Title = GetUserTitlesNames(userToEdit.Titles),
                    StrImage = userToEdit.FileUrl
                };
            }
            else
            {
                model.Surname = "";
                model.Name = "";
                model.Email = "";
                model.Title = new List<string> {""};
                ViewBag.Title = "New user";
            }
            model.CompanyModels = BuildCompanyModelsForView();

            return View("NewEditUser", model);
        }

        [HttpPost]
        public ActionResult CreateUser(HttpPostedFileBase file, NewEditUserModel model)
        {
            if (ModelState.IsValid)
            {
                string path = null;
                if (!string.IsNullOrEmpty(model.StrImage))
                {
                    path = model.StrImage;
                }
                else if (file != null)
                {
                    var filePath = Path.GetFileName(file.FileName);
                    
                    path = Path.Combine(ConfigManager.GetSittingsValueByKey("PhotoPath"), filePath);
                    file.SaveAs(Server.MapPath(path));
                }
                if (model.Id > 0)
                {
                    var userToUpdate = new User()
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Surname = model.Surname,
                        Email = model.Email,
                        BirthDate = model.BirthDate.GetValueOrDefault(),
                        CompanyId = model.CompanyId,
                        FileUrl = path
                    };
                    _userService.UpdateUser(userToUpdate);                    
                    _titelsServise.UpdateUserTitles(model.Id, model.Title);                    
                }
                else
                {
                    var userToCreate = new User()
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        Email = model.Email,
                        BirthDate = model.BirthDate.GetValueOrDefault(),
                        CompanyId = model.CompanyId,
                        FileUrl = path
                    };
                    _userService.CreateUser(userToCreate);
                    var uId = _userService.GetUsers().First(u => u.Email == model.Email).Id;
                    foreach (var s in model.Title)
                        _titelsServise.CreateTitle(s, uId);
                }

                return RedirectToAction("Index", "Users");
            }
            
            model.CompanyModels = BuildCompanyModelsForView();
            ViewBag.Title = "New user";
            if (model.Id > 0)
                ViewBag.Title = "Edit user";

            return View("NewEditUser", model);
        }

        [HttpDelete]
        public ActionResult DeleteUser(int id)
        {
            if (_userService.isUserExist(id))
            {
                _userService.DeleteUser(id);
            }
            return Json(new {result = "Redirect", url = Url.Action("index", "Users")});
        }

        private UserModel FillUser(User user)
        {
            return new UserModel
            {
                Id = user.Id,
                BirthDate = user.BirthDate,
                CompanyName = user.Company.Name,
                Name = user.Name,
                Surname = user.Surname,
                TitlesForView = GetUserTitlesNamesByOneString(user.Titles)
            };
        }

        private ICollection<string> GetUserTitlesNames(ICollection<Title> titles)
        {
            if (titles.Any())
            {
                return new List<string>(titles.Select(t => t.Name));
            }
            return new List<string>(){""};
        }

        private List<UserModel> GetAllUsers()
        {
            var tmpUsers = _userService.GetUsersWithAllInfo();
            var users = tmpUsers.Select(FillUser).ToList();
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
            }));

            return companyModels;
        }

        private string GetUserTitlesNamesByOneString(ICollection<Title> titles)
        {
            var strTitels = "";
            if (titles.Any())
            {
                strTitels = "( ";
                foreach (var title in titles)
                {
                    strTitels += $"{title.Name}, ";
                }
                strTitels = strTitels.Remove(strTitels.LastIndexOf(","), 1) + ")";
            }
            return strTitels;
        }
    }
}