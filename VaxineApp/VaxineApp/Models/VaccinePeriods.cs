using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.Models
{
    public class VaccinePeriods
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PeriodName { get; set; }
        public VaccinePeriods()
        {
            Id = new Guid();
        }
    }
}
