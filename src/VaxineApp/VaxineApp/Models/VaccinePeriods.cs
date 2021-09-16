using System;

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

        public bool IsEmpty()
        {
            if (Id == Guid.Empty && StartDate == default && EndDate == default && PeriodName is null)
            {
                return true;
            }
            return false;
        }
    }
}
