﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class FamilyModel
    {
        public Guid Id { get; set; }
        public int HouseNo { get; set; }
        public string ParentName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
