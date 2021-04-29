using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaxineApp.Models
{
    public class ProfileModel
    {
        // Image property
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string FatherOrHusbandName { get; set; }
        public int Age { get; set; }
        [Required]
        public string Role { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.EmailAddress)]
        [Compare("Email")]
        public string ConfirmEmail { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
