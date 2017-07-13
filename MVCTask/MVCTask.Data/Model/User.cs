﻿using System;
using System.Collections.Generic;

namespace MVCTask.Data.Model
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? CompanyId { get; set; }
        public DateTime BirthDate { get; set; }
        public string FileUrl { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Title> Titles { get; set; }
    }
}