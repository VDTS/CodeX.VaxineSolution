using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class ProfileModel
    {
        // Image property
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string FatherOrHusbandName { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Team { get; set; }
        public string Cluster { get; set; }
        public string Area { get; set; }
    }
}
