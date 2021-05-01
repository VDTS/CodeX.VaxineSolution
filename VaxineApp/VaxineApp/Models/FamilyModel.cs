using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.Models
{
    public class FamilyModel
    {
        public Guid Id { get; set; }
        public int HouseNo { get; set; }
        public string ParentName { get; set; }
        public string PhoneNumber { get; set; }

        List<ChildModel> Children { get; set; }
    }
}
