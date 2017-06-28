using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCTask.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Entre name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Entre surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Entre Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Entre title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Entre company")]
        public string CompanyName { get; set; }
        [Required(ErrorMessage = "Entre date")]
        public DateTime BirthDate { get; set; }
    }
}