using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using SuperKanban.Model.Entities;
using System.Windows.Data;

namespace SuperKanban.Converters
{
    public class LimitTypeToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            switch ((LimitType)value)
            {
                case LimitType.Allow:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#888B8B8B"));
                    break;
                case LimitType.Block:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#88E04646"));
                    break;
                case LimitType.Limit:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#77FFCC00"));
                    break;
                default:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("green"));
                    break;

            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
