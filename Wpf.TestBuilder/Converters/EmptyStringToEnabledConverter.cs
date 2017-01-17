using System;
using System.Globalization;
using System.Windows.Data;

namespace Wpf.TestBuilder.Converters
{
    class EmptyStringToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string) value == String.Empty || (string) value == null)
                return false;

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
