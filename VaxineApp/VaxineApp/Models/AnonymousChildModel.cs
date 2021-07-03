using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.Models
{
    public class AnonymousChildModel
    {
        public string FId { get; set; }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string VaccineStatus { get; set; }
        public Guid RegisteredBy { get; set; }
        public AnonymousChildModel()
        {
            Id = new Guid();
        }
    }
}
