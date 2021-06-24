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
    }
}
