using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.Models
{
    public class GetFamilyModel
    {
        public Guid Id { get; set; }
        public int HouseNo { get; set; }
        public string ParentName { get; set; }
        public string PhoneNumber { get; set; }
        public GetFamilyModel()
        {
            Id = new Guid();
        }
    }
}
