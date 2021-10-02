using FluentValidation;
using System;

namespace VaxineApp.Core.Models
{
    public class PeriodModel
    {
        public string FId { get; set; }
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? PeriodName { get; set; }
        public PeriodModel()
        {
            Id = new Guid();
        }

        public bool IsEmpty()
        {
            if (Id == Guid.Empty && StartDate == default && EndDate == default && PeriodName is null)
            {
                return true;
            }
            return false;
        }
    }
    public class PeriodValidator : AbstractValidator<PeriodModel>
    {
        public PeriodValidator()
        {
            RuleFor(p => p.PeriodName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Length(1, 20).WithMessage("Length of {PropertyName} should be between 1 - 20");
            RuleFor(p => p.StartDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty();
            RuleFor(p => p.EndDate)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty");
        }
    }
}
