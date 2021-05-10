using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Models
{
    public class GetChildGroupedbyFamilyModel : List<GetChildModel>
    {
        public int HouseNo { get; private set; }
        public GetChildGroupedbyFamilyModel(int HouseNo, List<GetChildModel> Childs) : base(Childs)
        {
            this.HouseNo = HouseNo;
        }
    }
    public class GetChildModel
    {
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public bool OPV0 { get; set; }
        public int RINo { get; set; }
    }
}
