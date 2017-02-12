using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Testura.Android.PageObjectCreator.Util.Converters
{
    public class BooleanToVisibilityInvertedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolean = (bool)value;
            return boolean ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
