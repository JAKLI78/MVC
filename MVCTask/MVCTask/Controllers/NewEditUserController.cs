using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter;
using MVCTask.Core.Interface;
using MVCTask.Filters;
using MVCTask.Models;

namespace MVCTask.Controllers
{
    [LogActionFilter]
    public class NewEditUserController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly ITitelsServise _titelsServise;
        private readonly IUserService _userService;

        public NewEditUserController(IUserService userService, ITitelsServise titelsServise,
            ICompanyService companyService)
        {
            if (userService == null)
                throw new ArgumentNullException(nameof(userService), $"{nameof(userService)} cannot be null.");
            if (titelsServise == null)
                throw new ArgumentNullException(nameof(titelsServise), $"{nameof(titelsServise)} cannot be null");
            if (companyService == null)
                throw new ArgumentNullException(nameof(companyService), $"{nameof(companyService)} cannot be null.");

            _userService = userService;
            _titelsServise = titelsServise;
            _companyService = companyService;
        }

        
        // GET: NewUser
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
                    path = Server.MapPath("/Content/Uploads/Files/" + fil);
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

        private List<CompanyModel> BuildCompanyModelsForView()
        {
            List<CompanyModel> companyModels = new EditableList<CompanyModel>
            {
                new CompanyModel {CompanyName = "", Id = 0}
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