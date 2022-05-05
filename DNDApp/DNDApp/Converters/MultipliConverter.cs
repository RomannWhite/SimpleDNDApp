using System;
using System.Globalization;
using Xamarin.Forms;

namespace DNDApp.Converters
{
    class MultipliConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value.ToString(), out double A) && double.TryParse(parameter.ToString(), out double B))
                return A * B;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}