﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FluentValidation;
using FluentValidation.Attributes;

namespace MVCTask.Models
{
    [Validator(typeof(AbstractValidator<UserModel>))]
    public class UserModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string TitlesForView { get; set; }

        public string CompanyName { get; set; }

        public DateTime? BirthDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        public IEnumerable<CompanyModel> CompanyModels { get; set; }
    }
}