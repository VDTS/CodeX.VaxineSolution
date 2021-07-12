using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib.Account
{
    public class AccountModels
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }

    public class ChangeAccountPasswordModel
    {
        public string idToken { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }

    public class ChangeEmailModel
    {
        public string idToken { get; set; }
        public string email { get; set; }
        public bool returnSecureToken { get; set; }
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
}
