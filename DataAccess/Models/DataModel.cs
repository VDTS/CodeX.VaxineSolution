using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class DataModel
    {
        public Guid Id { get; set; }
        public int HouseNo { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public bool OPV0 { get; set; }
        public int RINo { get; set; }
        public UserMetaData UserMetaData { get; set; }
        public DataModel()
        {
            Id = Guid.NewGuid();
            UserMetaData = new UserMetaData();
        }
        public List<VaccineModel> Vaccine { get; set; }
    }
}
