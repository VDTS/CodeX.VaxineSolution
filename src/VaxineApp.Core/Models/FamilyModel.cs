using FluentValidation;
using System;
using System.Linq;
using Utility.Validations;

namespace VaxineApp.Core.Models
{
    public class FamilyModel
    {
        public string? FId { get; set; }
        public Guid Id { get; set; }
        public int HouseNo { get; set; }
        public string? ParentName { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid RegisteredBy { get; set; }
        public FamilyModel()
        {
            Id = new Guid();
        }
    }
    public class FamilyValidator : AbstractValidator<FamilyModel>
    {
        public FamilyValidator()
        {
            RuleFor(f => f.HouseNo).NotEmpty();
            RuleFor(f => f.ParentName).Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
            RuleFor(f => f.PhoneNumber)
                .Must(PhoneNumberValidator.IsPhoneNumberValid)
                .WithMessage("Invalid phone number, must start with +93, 0093 or 0 and has 9 digits after it.");
        }
        protected bool BeAValidName(string? name)
        {
            name = name?.Replace(" ", "");
            name = name?.Replace("-", "");
            return name.All(Char.IsLetter);
        }
    }
}
