using System.Text.RegularExpressions;

namespace Utility.Validations
{
    public static class PasswordValidator
    {
        public static bool IsPasswordValid(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                string passwordRegex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
                return Regex.IsMatch(password, passwordRegex);
            }
            else
            {
                return false;
            }
        }
    }
}
