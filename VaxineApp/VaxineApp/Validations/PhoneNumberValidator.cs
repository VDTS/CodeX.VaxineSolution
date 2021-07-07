using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VaxineApp.Validations
{
    public class PhoneNumberValidator
    {
        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                string phoneNumberPattern = @"((0093)|(\+93)|(0))[7]\d{8}";

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
            else
            {
                return false;
            }
        }
    }
}
