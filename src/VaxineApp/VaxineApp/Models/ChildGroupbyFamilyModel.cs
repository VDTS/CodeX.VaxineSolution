using System.Collections.Generic;

namespace VaxineApp.Models
{
    public class ChildGroupbyFamilyModel : List<ChildModel>
    {
        public int HouseNo { get; private set; }
        public ChildGroupbyFamilyModel(int HouseNo, List<ChildModel> Childs) : base(Childs)
        {
            this.HouseNo = HouseNo;
        }
    }
}
