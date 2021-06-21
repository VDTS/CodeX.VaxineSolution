using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaxineApp.Models
{
    public class VaccineModel
    {
        public string FId { get; set; }
        public Guid Id { get; set; }
        public DateTime VaccinePeriod { get; set; }
        public string VaccineStatus { get; set; }
        public Guid RegisteredBy { get; set; }
        public VaccineModel()
        {
            Id = new Guid();
        }
    }
}
