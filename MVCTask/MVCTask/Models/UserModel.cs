using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVCTask.Models
{
    public class UserModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        public string[] Title { get; set; }

        public string TitlesForView { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CompanyId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int CountOfTitles { get; set; }

        [HiddenInput(DisplayValue = false)]
        public IEnumerable<string> Titles { get; set; }

        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Enter date")]
        public DateTime? BirthDate { get; set; }

        public IEnumerable<CompanyModel> CompanyModels { get; set; }
    }
}