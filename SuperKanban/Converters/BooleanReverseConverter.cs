using System;
using System.Collections.Generic;
using System.Text;

namespace SuperKanban.Converters
{
    class BooleanReverseConverter : BooleanConverter<bool>
    {
        public BooleanReverseConverter() :
        base(false,true)
        { }
    }
}
