using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaxineApp.Models
{
    public class VaccineModel
    {
        [Required]
        public DateTime VaccinePeriod { get; set; }
        [Required]
        public VaccineStatus VaccineStatus { get; set; }
        public UserMetaData UserMetaData { get; set; }
        public VaccineModel()
        {
            UserMetaData = new UserMetaData();
        }
    }
}
