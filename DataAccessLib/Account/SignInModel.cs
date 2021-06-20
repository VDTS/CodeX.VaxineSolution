using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLib.Account
{
    public class SignInModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }
}
