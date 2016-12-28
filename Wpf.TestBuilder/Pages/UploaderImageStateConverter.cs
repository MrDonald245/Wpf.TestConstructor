using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Wpf.TestBuilder.Pages
{
    class UploaderImageStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Visible" : "Hidden";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Visibility)value)
            {
                case Visibility.Visible:
                    return true;
                case Visibility.Hidden:
                    return false;
                default:
                    return false;
            }
        }
    }
}
