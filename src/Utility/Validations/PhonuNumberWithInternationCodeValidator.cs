using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Utility.Validations
{
    public class PhonuNumberWithInternationCodeValidator
    {
        public static bool IsPhoneNumberValid(string? phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                string phoneNumberPattern = @"((\+93))[7]\d{8}";

                bool isPhoneNumber = Regex.IsMatch(phoneNumber, phoneNumberPattern);

                if (isPhoneNumber)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (string.IsNullOrEmpty(phoneNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
