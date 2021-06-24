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

    public class Root
    {
        public string idToken { get; set; }
        public string password { get; set; }
        public bool returnSecureToken { get; set; }
    }

    public class EmailRoot
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
}
