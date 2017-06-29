using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter;
using Microsoft.Ajax.Utilities;
using MVCTask.Core.Interface;
using MVCTask.Models;

namespace MVCTask.Controllers
{
    public class NewUserController : BaseController
    {
        private readonly ITestServise _servise;
        
        public NewUserController(IActionInvoker actionInvoker,ITestServise servise)
        {
            if (actionInvoker==null||servise==null)
            {
                throw new ArgumentNullException("Some OF");
            }
            ActionInvoker = actionInvoker;
            _servise = servise;
        }
        // GET: NewUser
        public ActionResult NewUser()
        {
            
            
            
            var model = new UserModel()
            {
                Surname = "",Name = "",Email = "",Titles = new List<string>() { ""}
            };
            var userId = 0;
            foreach (var valuesKey in RouteData.Values.Keys)
            {
                if (valuesKey.Equals("id")) userId = int.Parse(RouteData.Values[valuesKey].ToString());
            }
            if(userId>0)            
            {
                var userToEdit = _servise.GetUsers().First(u=>u.Id == userId);
                var userTitels = _servise.GetTitelsForUserById(userId);
                ViewBag.Title = "Edit user";
                model = new UserModel()
                {
                    Id = userToEdit.Id,
                    BirthDate = userToEdit.BirthDate,
                    Email = userToEdit.Email,
                    Name = userToEdit.Name,
                    Surname = userToEdit.Surname,
                    CompanyId = userToEdit.CompanyId.GetValueOrDefault(),
                    Titles = userTitels,
                    CountOfTitles = userTitels.Count()
                };
            }
            List<CompanyModel> compModels = new EditableList<CompanyModel>
            {
                new CompanyModel() { CompanyName = "", Id = 0 }
            };

            compModels.AddRange(_servise.GetCompanies().Select(company => new CompanyModel() {CompanyName = company.Name, Id = company.Id}));

            model.CompanyModels = compModels;

            return View("NewUser",model);
        }
        [HttpPost]
        public ActionResult CreateUser(HttpPostedFileBase file,UserModel model,FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                var titles = "";
                var companyId = formCollection["Company"];
                var someDate = formCollection["BirthDate"];
                foreach (string formCollectionKey in formCollection.Keys)
                {
                    if (formCollectionKey.Contains("Title"))
                    {
                        if (formCollection[formCollectionKey].Length>0)
                        {
                            titles+=formCollection[formCollectionKey]+"/";
                        }                        
                    }
                }
                if (someDate.Length > 0)
                {
                    model.BirthDate = DateTime.Parse(someDate);
                }

                var path = "";
                if (file != null)
                {
                    var fil = System.IO.Path.GetFileName(file.FileName);
                    path = Server.MapPath("/Content/Uploads/Files/" + fil);
                    file.SaveAs(path);
                }
                if (model.Id>0)
                {
                    _servise.UpdateUser(model.Id, model.Name, model.Surname, model.Email, titles, model.BirthDate, int.Parse(companyId), path);
                }
                else
                _servise.CreateUser(model.Name, model.Surname, model.Email, titles, model.BirthDate, int.Parse(companyId), path);

                return RedirectToAction("Index", "Users");
            }

            List<CompanyModel> compModels = new EditableList<CompanyModel>
            {
                new CompanyModel() { CompanyName = "", Id = 0 }
            };
            compModels.AddRange(_servise.GetCompanies().Select(company => new CompanyModel() { CompanyName = company.Name, Id = company.Id }));
            model.Titles = new List<string>(){""};
            model.CompanyModels = compModels;
            ViewBag.Titel = "New user";
            if (model.Id>0)
            {
                ViewBag.Titel = "Edit user";
            }
           
            return View("NewUser",model);
        }
    }
}