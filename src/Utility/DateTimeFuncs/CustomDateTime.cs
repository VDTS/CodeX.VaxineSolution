using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.DateTimeFuncs
{
    public static class CustomDateTime
    {
        public static int DatesDifference(DateTime startDate, DateTime endDate)
        {
            var one = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            int years = endDate.Year - startDate.Year;
            int days = endDate.Day - startDate.Day;
            int startMonth = 0;
            int endMonth = 0;
            
            if(startDate.Year % 4 == 0)
            {
                one[1] = 29;
            }
            else
            {
                one[1] = 28;
            }


            for (int i = 0; i < startDate.Month; i++)
            {
                startMonth += one[i];
            }

            if (endDate.Year % 4 == 0)
            {
                one[1] = 29;
            }
            else
            {
                one[1] = 28;
            }

            for (int i = 0; i < endDate.Month; i++)
            {
                endMonth += one[i];
            }

            var result = years * 365 + days;
            result += endMonth - startMonth;

            return result;
        }
    }
}
