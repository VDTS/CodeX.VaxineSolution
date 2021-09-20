using FluentValidation;
using System;
using VaxineApp.Models.Enums;

namespace VaxineApp.Models
{
    public class AnnouncementsModel
    {
        public string? FId { get; set; }
        public Guid? Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? MessageDateTime { get; set; }
        public IsActive? IsActive { get; set; }
        public AnnouncementsModel()
        {
            Id = new Guid();
        }
        public bool IsEmpty()
        {
            if (Id == Guid.Empty && Title is null && Content is null && MessageDateTime != default)
            {
                return true;
            }
            return false;
        }
    }
    public class AnnouncementsValidator : AbstractValidator<AnnouncementsModel>
    {
        public AnnouncementsValidator()
        {
            RuleFor(a => a.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty");
        }
    }
}
