using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace VaxineApp.Validations
{
    public static class VaccinePeriodValidator
    {
        public static bool IsPeriodAvailable(DateTime dateTime)
        {
            var period = dateTime;

            var startDate = Preferences.Get("PeriodStartDate", DateTime.UtcNow);

            var endDate = Preferences.Get("PeriodEndDate", DateTime.UtcNow);

            if (startDate.Year == endDate.Year &&
                startDate.Year == period.Year &&
                period.Month <= endDate.Month &&
                period.Month >= startDate.Month &&
                period.Day <= endDate.Day &&
                period.Day >= startDate.Day)
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
