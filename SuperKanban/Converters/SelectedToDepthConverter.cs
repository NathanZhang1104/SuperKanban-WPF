using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
namespace SuperKanban.Converters
{
    public class SelectedToDepthConverter : IValueConverter
    {
     
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            { 

                return value is bool boolValue && boolValue? ShadowDepth.Depth1 : ShadowDepth.Depth0;
                throw new NotImplementedException();
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
    }
}
