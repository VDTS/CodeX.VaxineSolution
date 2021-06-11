using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib.Models
{
    public class GetFamilyModel
    {
        public Guid Id { get; set; }
        public int HouseNo { get; set; }
        public string ParentName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RegisteredBy { get; set; }
        public GetFamilyModel()
        {
            Id = new Guid();
        }
    }
}
