using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VaxineApp.Models
{
    public class ChildModel
    {
        public Guid Id { get; set; }
        [Required]
        public int HouseNo { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public bool OPV0 { get; set; }
        public int RINo { get; set; }
        public UserMetaData UserMetaData { get; set; }
        public ChildModel()
        {
            Id = Guid.NewGuid();
            UserMetaData = new UserMetaData();
        }
    }
}
