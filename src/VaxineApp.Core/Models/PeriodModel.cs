using FluentValidation;
using System;
using Utility.DateTimeFuncs;

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
            RuleFor(p => new { p.StartDate, p.EndDate }).Must(x => LessThan30Days(x.StartDate, x.EndDate))
                .WithMessage("Period must be end in less than 30 days");
        }

        private bool LessThan30Days(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (CustomDateTime.DatesDifference(startDate, endDate) <= 30 && CustomDateTime.DatesDifference(startDate, endDate) >= 1)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
