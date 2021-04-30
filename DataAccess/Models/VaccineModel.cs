using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class VaccineModel
    {
        public DateTime VaccinePeriod { get; set; }
        public VaccineStatus VaccineStatus { get; set; }
        public UserMetaData UserMetaData { get; set; }
        public VaccineModel()
        {
            UserMetaData = new UserMetaData();
        }
    }
}
