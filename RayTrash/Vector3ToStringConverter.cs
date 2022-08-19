using RayTrace;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RayTrash
{
    public class Vector3ToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (Vector3)value;
            return v.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string)value;
            var split = str.Split(';');
            if (split.Length != 3)
            {
                return DependencyProperty.UnsetValue;
            }

            double convert(string v)
            {
                return double.Parse(v.Trim());
            }

            return new Vector3(convert(split[0]), convert(split[1]), convert(split[2]));
        }
    }
}
