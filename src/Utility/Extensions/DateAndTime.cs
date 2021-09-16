using System;

namespace Utility.Extensions
{
    public static class DateAndTime
    {
        public static int TimeDifferenceBySecs(this DateTime dateTime)
        {
            var CurrentTime = DateTime.UtcNow;

            int result = 0;

            result += (CurrentTime.Day - dateTime.Day) * 24 * 60 * 60;
            result += (CurrentTime.Hour - dateTime.Hour) * 60 * 60;
            result += (CurrentTime.Minute - dateTime.Minute) * 60;
            result += (CurrentTime.Second - dateTime.Second);

            return result;
        }
    }
}
