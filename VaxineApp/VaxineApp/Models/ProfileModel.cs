using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaxineApp.Models
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
        [Required]
        public string Role { get; set; }
        public string TeamId { get; set; }
        public string ClusterId { get; set; }
        public string LocalId { get; set; }
        public ProfileModel()
        {
            Id = new Guid();
        }
    }
}
