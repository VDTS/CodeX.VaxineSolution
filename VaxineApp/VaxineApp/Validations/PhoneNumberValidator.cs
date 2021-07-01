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
                string phoneNumberWithInternationalCode = @"^[\+][9][3][0-9]{9}$";
                string phoneNumberWithLocalCode = @"^[0][7][0-9]{8}$";
                string phoneNumberWithCode = @"^[0]{2}[9][3][7][0-9]{8}$";

                bool isphoneNumberWithInternationalCodeMatched = Regex.IsMatch(phoneNumber, phoneNumberWithInternationalCode);
                bool isphoneNumberWithLocalCode = Regex.IsMatch(phoneNumber, phoneNumberWithLocalCode);
                bool isphoneNumberWithCode = Regex.IsMatch(phoneNumber, phoneNumberWithCode);

                if (isphoneNumberWithInternationalCodeMatched || isphoneNumberWithLocalCode || isphoneNumberWithCode)
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
