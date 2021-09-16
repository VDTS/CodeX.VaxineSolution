using System.Text.RegularExpressions;

namespace Utility.Validations
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
