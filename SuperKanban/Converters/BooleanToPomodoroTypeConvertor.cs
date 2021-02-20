using System;
using System.Collections.Generic;
using System.Text;
using SuperKanban.View.UserConrols;
namespace SuperKanban.Converters
{

    public sealed class BooleanToPomodoroTypeConvertor : BooleanConverter<TimeType>
    {
        public BooleanToPomodoroTypeConvertor() :
            base(TimeType.Pomodoro, TimeType.ShortBreak)
        { }
    }
}
