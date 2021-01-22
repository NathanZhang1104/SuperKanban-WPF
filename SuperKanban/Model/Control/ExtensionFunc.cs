using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
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
    }
}
