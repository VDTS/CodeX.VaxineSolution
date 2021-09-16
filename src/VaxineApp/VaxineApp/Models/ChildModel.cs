using FluentValidation;
using System;
using System.Linq;

namespace VaxineApp.Models
{
    public class ChildModel
    {
        public string FId { get; set; }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public bool OPV0 { get; set; }
        public int RINo { get; set; }
        public Guid RegisteredBy { get; set; }
        public ChildModel()
        {
            Id = new Guid();
        }
    }

    public class ChildValidator : AbstractValidator<ChildModel>
    {
        public ChildValidator()
        {
            RuleFor(c => c.FullName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
            RuleFor(c => c.Gender).NotEmpty();
            RuleFor(c => c.RegisteredBy).NotEmpty();
            RuleFor(c => c.RINo).NotEmpty();
            RuleFor(c => c.DOB)
                .Must(BeAValidDateOfBorn)
                .WithMessage("Child must be under 5");
        }

        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");
            return name.All(Char.IsLetter);
        }

        protected bool BeAValidDateOfBorn(DateTime DOB)
        {
            var ageInMonths = 12 * (DateTime.UtcNow.Year - DOB.Year) + DOB.Month;
            if (ageInMonths <= 60)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
