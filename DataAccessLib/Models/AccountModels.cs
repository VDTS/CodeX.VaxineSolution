using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib.Models
{
    public class AccountModels
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }
    public class AccountValidator : AbstractValidator<AccountModels>
    {
        public AccountValidator()
        {
            RuleFor(a => a.email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(a => a.password)
                .NotEmpty()
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
                .WithMessage("Password must not be less than 8 characters and must include letters, digits and special characters.");
        }
    }

    public class ChangeAccountPasswordModel
    {
        public string idToken { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }
    public class ChangeAccountPasswordValidator : AbstractValidator<ChangeAccountPasswordModel>
    {
        public ChangeAccountPasswordValidator()
        {
            RuleFor(a => a.password)
                .NotEmpty()
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
                .WithMessage("Password must not be less than 8 characters and must include letters, digits and special characters.");
        }
    }
    public class ChangeEmailModel
    {
        public string idToken { get; set; }
        public string email { get; set; }
        public bool returnSecureToken { get; set; }
    }
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailModel>
    {
        public ChangeEmailValidator()
        {
            RuleFor(a => a.email)
                .NotEmpty()
                .EmailAddress();
        }
    }

    public class VerifyEmailModel
    {
        public string requestType { get; set; }
        public string idToken { get; set; }
    }
    public class SendPasswordResetCodeModel
    {
        public string requestType { get; set; }
        public string email { get; set; }
    }
    public class VerifyPasswordResetCodeModel
    {
        public string oobCode { get; set; }
    }
    public class ConfirmPasswordResetModel
    {
        public string oobCode { get; set; }
        public string newPassword { get; set; }
    }
    public class ConfirmPasswordReset : AbstractValidator<ConfirmPasswordResetModel>
    {
        public ConfirmPasswordReset()
        {
            RuleFor(a => a.newPassword)
               .NotEmpty()
               .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
               .WithMessage("Password must not be less than 8 characters and must include letters, digits and special characters.");
        }
    }
    public class RefreshTokenModel
    {
        public string grant_type { get; set; }
        public string refresh_token { get; set; }
    }
}
