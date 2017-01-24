using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Wpf.TestBuilder.Converters
{
    class StringToVisiblilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string) value == String.Empty || (string) value == null ? "Hidden" : "Visible";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Visibility) value)
            {
                case Visibility.Visible:
                    return "";
                case Visibility.Hidden:
                    return "";
                default:
                    return "";
            }
        }
    }
}