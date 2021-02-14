
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
    public class DateToHeightConverter : IValueConverter
    {
        private List<string> weekstringlist = new List<string> { "周一", "周二", "周三", "周四", "周五", "周六", "周日" };

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tr = (DateRange)value;
            double ret;
            switch ((string)parameter)
            {
                case "-1":
                    ret = tr.from * 30;
                    break;
                case "1":
                    ret = tr.to * 30;
                    break;
                case "0":
                    ret = (tr.RangeLength) * 30;
                    break;
                /* 您可以有任意数量的 case 语句 */
                case "2":
                    return weekstringlist[tr.from] + "到" + weekstringlist[tr.to];
                default: /* 可选的 */

                    ret = 0;
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
