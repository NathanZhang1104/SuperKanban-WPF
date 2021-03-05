using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
namespace SuperKanban.Converters
{
    public class BooleanToPomoColorConverter: BooleanConverter<Brush>
    {
        public BooleanToPomoColorConverter() :
            base(new SolidColorBrush((Color)ColorConverter.ConvertFromString((string)"#FFE75F55")), new SolidColorBrush((Color)ColorConverter.ConvertFromString((string)"#FF95EEA9")))
        { }
    }
}
