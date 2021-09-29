using FluentValidation;
using Newtonsoft.Json;

namespace DataAccess.Models
{
    public class AccountIdToken
    {
        [JsonProperty("idToken")]
        public string IdToken { get; set; }
    }
    public class AccountModels
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("returnSecureToken")]
        public bool ReturnSecureToken { get; set; }
    }
    public class AccountValidator : AbstractValidator<AccountModels>
    {
        public AccountValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(a => a.Password)
                .NotEmpty()
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
                .WithMessage("Password must not be less than 8 characters and must include letters, digits and special characters.");
        }
    }

    public class ChangeAccountPasswordModel
    {
        [JsonProperty("idToken")]
        public string IdToken { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("returnSecureToken")]
        public bool ReturnSecureToken { get; set; }
    }
    public class ChangeAccountPasswordValidator : AbstractValidator<ChangeAccountPasswordModel>
    {
        public ChangeAccountPasswordValidator()
        {
            RuleFor(a => a.Password)
                .NotEmpty()
                .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
                .WithMessage("Password must not be less than 8 characters and must include letters, digits and special characters.");
        }
    }
    public class ChangeEmailModel
    {
        [JsonProperty("idToken")]
        public string IdToken { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("returnSecureToken")]
        public bool ReturnSecureToken { get; set; }
    }
    public class ChangeEmailValidator : AbstractValidator<ChangeEmailModel>
    {
        public ChangeEmailValidator()
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }

    public class VerifyEmailModel
    {
        [JsonProperty("requestType")]
        public string RequestType { get; set; }
        [JsonProperty("idToken")]
        public string IdToken { get; set; }
    }
    public class SendPasswordResetCodeModel
    {
        [JsonProperty("requestType")]
        public string RequestType { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
    }
    public class VerifyPasswordResetCodeModel
    {
        [JsonProperty("oobCode")]
        public string OobCode { get; set; }
    }
    public class ConfirmPasswordResetModel
    {
        [JsonProperty("oobCode")]
        public string oobCode { get; set; }
        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }
    public class ConfirmPasswordReset : AbstractValidator<ConfirmPasswordResetModel>
    {
        public ConfirmPasswordReset()
        {
            RuleFor(a => a.NewPassword)
               .NotEmpty()
               .Matches(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$")
               .WithMessage("Password must not be less than 8 characters and must include letters, digits and special characters.");
        }
    }
    public class RefreshTokenModel
    {
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
