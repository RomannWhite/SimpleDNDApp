using System;
using System.Globalization;
using Xamarin.Forms;

namespace DNDApp.Converters
{
    class BoolNotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool BoolValue)
                return !BoolValue;
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool BoolValue)
                return !BoolValue;
            return value;
        }
    }
}