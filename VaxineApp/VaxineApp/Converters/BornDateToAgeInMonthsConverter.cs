﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace VaxineApp.Converters
{
    public class BornDateToAgeInMonthsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var age = (DateTime)value;
            var ageInMonths = 12 * (DateTime.UtcNow.Year - age.Year) + Math.Abs((age.Month) - (DateTime.UtcNow.Month));
            return ageInMonths;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}
