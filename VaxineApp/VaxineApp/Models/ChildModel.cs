using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VaxineApp.Models
{
    public class ChildModel
    {
        public Guid Id { get; set; }
        public string HouseNo { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string OPV0 { get; set; }
        public string RINo { get; set; }
        public UserMetaData UserMetaData { get; set; }
        public ChildModel()
        {
            Id = Guid.NewGuid();
            UserMetaData = new UserMetaData();
        }
    }
}
