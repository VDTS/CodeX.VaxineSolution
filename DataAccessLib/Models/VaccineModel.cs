using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib.Models
{
    public class VaccineModel
    {
        public DateTime VaccinePeriod { get; set; }
        public string VaccineStatus { get; set; }
        public Guid RegisteredBy { get; set; }
    }
}
