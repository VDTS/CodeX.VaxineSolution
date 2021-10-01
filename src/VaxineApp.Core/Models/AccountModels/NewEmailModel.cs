using FluentValidation;

namespace VaxineApp.Core.Models.AccountModels
{
    public class NewEmailModel
    {
        public string? CurrentEmail { get; set; }
        public string? NewEmail { get; set; }
        public string? ConfirmEmail { get; set; }
    }

    public class NewEmailValidator : AbstractValidator<NewEmailModel>
    {
        public NewEmailValidator()
        {
            RuleFor(e => e.CurrentEmail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .EmailAddress().WithMessage("Not proper email address");

            RuleFor(e => e.NewEmail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .EmailAddress().WithMessage("Not proper email address");

            RuleFor(e => e.ConfirmEmail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .EmailAddress().WithMessage("Not proper email address");

            RuleFor(e => new { e.NewEmail, e.ConfirmEmail }).Must(x => EqualEmails(x.NewEmail, x.ConfirmEmail))
                .WithMessage("newEmail and confirmEmail aren't the same");
        }

        protected bool EqualEmails(string newEmail, string confirmEmail)
        {
            if (newEmail == confirmEmail)
                return true;
            return false;
        }
    }
}
