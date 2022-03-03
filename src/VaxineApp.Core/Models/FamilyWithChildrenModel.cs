namespace VaxineApp.Core.Models
{
    public class FamilyWithChildrenModel
    {
        public FamilyModel Family { get; set; }
        public ChildModel[] Children { get; set; }
    }
}
