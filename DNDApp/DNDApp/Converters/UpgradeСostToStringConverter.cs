using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;
using System;

namespace DNDApp.Converters
{
    class UpgradeСostToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string Result = "";
            if(value is float[] Value)
            {
                foreach (var item in Value)
                {
                    if (item % 1 == 0)
                        Result += item.ToString("0");
                    else
                        Result += "½";
                }
            }
            return Result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<float> Result = new List<float>();
            if (value is string Value)
            {
                foreach (var item in Value.ToCharArray())
                {
                    if (int.TryParse(item.ToString(), out int IntItem))
                        Result.Add(IntItem);
                    else
                        Result.Add(0.5F);
                }
            }
            return Result.ToArray();
        }
    }
}
