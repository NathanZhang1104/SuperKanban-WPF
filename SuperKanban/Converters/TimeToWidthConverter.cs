using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using SuperKanban.Model.Entities;
namespace SuperKanban.Converters
{
    public class TimeToWidthConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tr = (TimeRange)value;
            double ret ;
            switch ((string)parameter)
            {
                case "-1":
                    ret = tr.start*30;
                    break;
                case "1":
                    ret = tr.end * 30;
                    break;
                case "0":
                    ret = (tr.RangeLength) * 30;
                    break;
                /* 您可以有任意数量的 case 语句 */
                case "2":
                    ;
                    return TimeSpan.FromHours(tr.start).ToString("h\\:mm") + "-" 
                        + TimeSpan.FromHours(tr.end).ToString("h\\:mm");
                default: /* 可选的 */
                   ret =0;
                    break;
            }
            return ret;
            throw new NotImplementedException();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
