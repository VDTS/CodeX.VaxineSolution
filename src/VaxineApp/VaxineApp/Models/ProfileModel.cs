using FluentValidation;
using System;
using System.Linq;

namespace VaxineApp.Models
{
    public class ProfileModel
    {
        // Image property
        public string? FId { get; set; }
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public string? FatherOrHusbandName { get; set; }
        public int Age { get; set; }
        public string? Role { get; set; }
        public string? TeamId { get; set; }
        public string? ClusterId { get; set; }
        public string? LocalId { get; set; }
        public ProfileModel()
        {
            Id = new Guid();
        }
    }
    public class ProfileValidator : AbstractValidator<ProfileModel>
    {
        public ProfileValidator()
        {
            RuleFor(p => p.FullName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
            RuleFor(p => p.Gender).NotEmpty();
            RuleFor(p => p.FatherOrHusbandName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(BeAValidName).WithMessage("{PropertyName} must be valid characters")
                .Length(3, 50).WithMessage("Length of {PropertyName} should be between 3 - 50");
            RuleFor(p => p.Age)
                .LessThanOrEqualTo(70)
                .GreaterThanOrEqualTo(18);
        }
        protected bool BeAValidName(string? name)
        {
            name = name?.Replace(" ", "");
            name = name?.Replace("-", "");
            return name.All(Char.IsLetter);
        }
    }
}
