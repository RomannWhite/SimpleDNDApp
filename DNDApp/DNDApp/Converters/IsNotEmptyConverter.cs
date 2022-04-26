using System.Globalization;
using Xamarin.Forms;
using System;

namespace DNDApp.Converters
{
    class IsNotEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string Value)
                return !string.IsNullOrEmpty(Value);
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
