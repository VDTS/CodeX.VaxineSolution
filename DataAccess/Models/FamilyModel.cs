using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class FamilyModel
    {
        public int HouseNo { get; set; }
        public string ParentName { get; set; }
        public string PhoneNumber { get; set; }

        public List<ChildModel> Children { get; set; }
    }
}
