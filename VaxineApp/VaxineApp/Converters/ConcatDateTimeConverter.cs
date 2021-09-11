using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using VaxineApp.Models.Enums;
using Xamarin.Forms;

namespace VaxineApp.Converters
{
    public class ConcatDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var age = (DateTime)value;
            return Minutes(age);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }

        public string Minutes(DateTime age)
        {
            var nowTime = DateTime.UtcNow;
            if (age.Year == nowTime.Year &&
                age.Month == nowTime.Month &&
                age.Day == nowTime.Day &&
                age.Hour == nowTime.Hour &&
                age.Minute == nowTime.Minute &&
                age.Minute != nowTime.Second)
            {
                return string.Concat(Math.Abs(age.Second - nowTime.Second), " secs");
            }
            else if (age.Year == nowTime.Year &&
                    age.Month == nowTime.Month &&
                    age.Day == nowTime.Day &&
                    age.Hour == nowTime.Hour &&
                    age.Minute != nowTime.Minute)
            {
                return string.Concat(Math.Abs(age.Minute - nowTime.Minute), " mins");
            }
            else if (age.Year == nowTime.Year &&
                    age.Month == nowTime.Month &&
                    age.Day == nowTime.Day &&
                    age.Hour != nowTime.Hour)
            {
                return string.Concat(age.Hour, ":", age.Minute);
            }
            else if (age.Year == nowTime.Year &&
                    age.Month == nowTime.Month &&
                    age.Day != nowTime.Day)
            {
                var a = (Months)0;
                return string.Concat((Months)(age.Month-1), " ", age.Day);
            }
            else
            {
                return string.Concat((Months)(age.Month - 1), " ", age.Day, " '", (age.Year % 100));
            }
        }
    }
}
