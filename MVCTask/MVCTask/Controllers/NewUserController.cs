using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter;
using MVCTask.Core.Interface;
using MVCTask.Models;

namespace MVCTask.Controllers
{
    public class NewUserController : BaseController
    {
        private readonly ICompanyService _companyService;
        private readonly ITitelsServise _titelsServise;
        private readonly IUserService _userService;

        public NewUserController(IUserService userService, ITitelsServise titelsServise, ICompanyService companyService)
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
            var model = new UserModel
            {
                Surname = "",
                Name = "",
                Email = "",
                Title = new List<string> {""}
            };
            ViewBag.Title = "New user";
            if (id > 0)
            {
                var userToEdit = _userService.GetUsers().First(u => u.Id == id.Value);
                var userTitels = _titelsServise.GetTitelsForUserById(id.Value);
                if (!userTitels.Any())

                    userTitels = new List<string> {""};

                ViewBag.Title = "Edit user";
                model = new UserModel
                {
                    Id = userToEdit.Id,
                    BirthDate = userToEdit.BirthDate,
                    Email = userToEdit.Email,
                    Name = userToEdit.Name,
                    Surname = userToEdit.Surname,
                    CompanyId = userToEdit.CompanyId.GetValueOrDefault(),
                    Title = (ICollection<string>) userTitels
                };
            }
            model.CompanyModels = BuildCompanyModelsForView();

            return View("NewUser", model);
        }


        [HttpPost]
        public ActionResult CreateUser(HttpPostedFileBase file, UserModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var titles = "";
                var companyId = formCollection["Company"];
                var someDate = formCollection["BirthDate"];

                if (someDate.Length > 0)
                    model.BirthDate = DateTime.Parse(someDate);

                var path = "";
                if (file != null)
                {
                    var fil = Path.GetFileName(file.FileName);
                    path = Server.MapPath("/Content/Uploads/Files/" + fil);
                    file.SaveAs(path);
                }
                if (model.Id > 0) //TODO:ОБНОВЛЕНИЕ ДОЛЖНОСТЕЙ
                {
                    _userService.UpdateUser(model.Id, model.Name, model.Surname, model.Email,
                        model.BirthDate.Value,
                        int.Parse(companyId), path);
                }
                else
                {
                    _userService.CreateUser(model.Name, model.Surname, model.Email, titles, model.BirthDate.Value,
                        int.Parse(companyId), path);
                    var uId = _userService.GetUsers().First(u => u.Email == model.Email).Id;
                    foreach (var s in model.Title)
                        _titelsServise.CreateTitle(s, uId);
                }


                return RedirectToAction("Index", "Users");
            }

            model.Titles = new List<string> {""};
            model.CompanyModels = BuildCompanyModelsForView();
            ViewBag.Title = "New user";
            if (model.Id > 0)
                ViewBag.Title = "Edit user";

            return View("NewUser", model);
        }

        private List<CompanyModel> BuildCompanyModelsForView()
        {
            List<CompanyModel> compModels = new EditableList<CompanyModel>
            {
                new CompanyModel {CompanyName = "", Id = 0}
            };
            compModels.AddRange(_companyService.GetCompanies()
                .Select(company => new CompanyModel {CompanyName = company.Name, Id = company.Id}));

            return compModels;
        }
    }
}