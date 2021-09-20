using FluentValidation;
using Utility.Validations;

namespace VaxineApp.Models.AccountModels
{
    public class NewPasswordModel
    {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
    public class NewPasswordValidator : AbstractValidator<NewPasswordModel>
    {
        public NewPasswordValidator()
        {
            RuleFor(e => e.CurrentPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty");

            RuleFor(e => e.NewPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(ProperPassword)
                .WithMessage("Password must contains at least one digit, one lowercase, one uppercase, one special character, and length must between 8-20"); ;

            RuleFor(e => e.ConfirmPassword)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} is Empty")
                .Must(ProperPassword)
                .WithMessage("Password must contains at least one digit, one lowercase, one uppercase, one special character, and length must between 8-20");

            RuleFor(e => new { e.NewPassword, e.ConfirmPassword })
                .Must(x => EqualEmails(x.NewPassword, x.ConfirmPassword))
                .WithMessage("newEmail and confirmEmail aren't the same");
        }

        protected bool EqualEmails(string newPassword, string confirmPassword)
        {
            if (newPassword == confirmPassword)
                return true;
            return false;
        }
        protected bool ProperPassword(string password)
        {
            return PasswordValidator.IsPasswordValid(password);
        }
    }
}
