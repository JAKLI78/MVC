using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Attributes;

namespace MVCTask.Models
{
    [Validator(typeof(AbstractValidator<UserModel>))]
    public class UserModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public ICollection<string> Title { get; set; }

        public string TitlesForView { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string StrImage { get; set; }

        public int CompanyId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public IEnumerable<string> Titles { get; set; }

        public string CompanyName { get; set; }

        public DateTime? BirthDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        public IEnumerable<CompanyModel> CompanyModels { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Log { get; set; }
    }
}