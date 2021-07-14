using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string Type { get; set; }
        public bool IsVaccined { get; set; }
        public Guid RegisteredBy { get; set; }
        public AnonymousChildModel()
        {
            Id = new Guid();
            IsVaccined = true;
        }
    }
    public class AnonymousChildValidator : AbstractValidator<AnonymousChildModel>
    {
        public AnonymousChildValidator()
        {
            RuleFor(c => c.FullName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 20).WithMessage("Length of {PropertyName} should be between 3 - 20");
            RuleFor(c => c.Gender).NotEmpty();
            RuleFor(c => c.Type).NotEmpty();
            RuleFor(c => c.RegisteredBy).NotEmpty();
            RuleFor(c => c.DOB)
                .Must(BeAValidDateOfBorn)
                .WithMessage("Child must be under 5");
            RuleFor(c => c.IsVaccined)
                .NotEmpty().WithMessage("Can't add without adding vaccine");
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
