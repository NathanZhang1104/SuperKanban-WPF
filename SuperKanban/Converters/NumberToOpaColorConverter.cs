using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace SuperKanban.Converters
{
    public class NumberToOpaColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString((string)"#0094D75C"));
            }
            else
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString((string)"#4F94D75C"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
