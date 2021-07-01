using System;
using System.Collections.Generic;
using System.Text;

namespace VaxineApp.Validations
{
    public static class ChildValidator
    {
        public static bool IsChildUnder5(DateTime DOB)
        {
            var ageInMonths = 12 * (DateTime.UtcNow.Year - DOB.Year) + DOB.Month;
            if (ageInMonths <= 60)
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
