using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTask.Models
{
    public class NewEditUserModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public ICollection<string> Title { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string StrImage { get; set; }

        public int CompanyId { get; set; }

        public DateTime? BirthDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        public IEnumerable<CompanyModel> CompanyModels { get; set; }
    }
}