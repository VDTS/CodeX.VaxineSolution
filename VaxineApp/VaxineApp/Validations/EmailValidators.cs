using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VaxineApp.Validations
{
    public class EmailValidators
    {
        public static bool IsEmailValid(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                string emailRegex = @"^([\w\. \-]+)@([\w\-]+)((\.(\w){2,3})+)$";
                bool isMatched = Regex.IsMatch(email, emailRegex);
                if (isMatched)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
