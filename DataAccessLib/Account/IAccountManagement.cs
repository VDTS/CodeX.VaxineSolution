using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.Account
{
    public interface IAccountManagement
    {
        public Task<string> SignIn(string email, string password);
        public Task<string> ChangeAccountPassword(string email, string password);
        public Task<string> ChangeEmail(string email, string token);
        public Task<string> VerifyEmail(string Token);
        public Task<string> SendPasswordResetcode(string email);
        public Task<string> VerifyPasswordResetCode(string resetCode);
        public Task<string> ConfirmPasswordReset(string resetCode, string NewPassword);
    }
}
