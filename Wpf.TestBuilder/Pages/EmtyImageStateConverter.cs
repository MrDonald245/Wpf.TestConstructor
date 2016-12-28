using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Wpf.TestBuilder.Pages
{
    class EmtyImageStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Hidden" : "Visible";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Visibility)value)
            {
                case Visibility.Visible:
                    return false;
                case Visibility.Hidden:
                    return true;
                default:
                    return false;
            }
        }
    }
}
