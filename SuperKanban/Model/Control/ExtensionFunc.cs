using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SuperKanban.Model.Entities;
namespace SuperKanban.Model.Control
{
    public static class ExtensionF
    {
        //静态方法
        public static ProcessSame CompareTo(this Process process1,Process process2)  //this关键字
        {
            if (process2 == null) { return ProcessSame.Different; }
            if (process1.Id == process2.Id &&process1.MainWindowTitle== process2.MainWindowTitle)
            {
                return ProcessSame.Same;
            }else if(process1.Id == process2.Id && process1.MainWindowTitle != process2.MainWindowTitle)
            {
                return ProcessSame.SameProcessDifferentTitle;
            }
            else if (process1.MainModule.FileName == process2.MainModule.FileName && process1.Id != process2.Id)
            {
                return ProcessSame.SamePathDifferentProcess;
            }
            else if(process1.MainModule.FileName != process2.MainModule.FileName)
            {
                return ProcessSame.Different;
            }
            else
            {
                return ProcessSame.Different;

            }
        }

        public static bool IsIn(this DateTime dateTime,TimeRuleOne ruleOne)
        {
            int week = ((int)dateTime.DayOfWeek-1)%7;
            if (week > ruleOne.DateRange.from && week < ruleOne.DateRange.to)
            {
                double timevalue = dateTime.Hour + ((double)(dateTime.Minute * 60 + dateTime.Second)) / 3600;
                if (timevalue > ruleOne.TimeRange.start && timevalue < ruleOne.TimeRange.end)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}
