using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.Models
{
    public class ChildModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public bool OPV0 { get; set; }
        public int RINo { get; set; }
        public ChildModel()
        {
            Id = new Guid();
        }
    }
}
