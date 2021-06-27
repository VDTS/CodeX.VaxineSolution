using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib.Models
{
    public class ProfileModel
    {
        // Image property
        public string FId { get; set; }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string FatherOrHusbandName { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string TeamId { get; set; }
        public string ClusterId { get; set; }
        public ProfileModel()
        {
            Id = new Guid();
        }
    }
}
