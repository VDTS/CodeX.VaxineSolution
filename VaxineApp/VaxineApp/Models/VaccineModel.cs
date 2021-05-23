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
        public string VaccineStatus { get; set; }
    }
}
