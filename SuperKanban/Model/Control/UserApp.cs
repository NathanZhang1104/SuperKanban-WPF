using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace SuperKanban.Model.Control
{

    public enum ProcessSame
    {
        Same = 0,
        SamePathDifferentProcess = 1,
        SameProcessDifferentTitle = 2,
        Different,
    }

    public enum AppType
    {
        Common=0,
        Browser=1,
        Other=2
    }

    public class UserApp
    {

        private List<Process> processes = new List<Process>();

        public UserApp(Process processes)
        {
            this.CurrentProcess = processes;
        }

        //private Process currentprocess;

        public Process CurrentProcess
        {
            get
            {
                if (processes.Count > 0)
                {
                    return processes[0];
                }
                else
                {
                    return null;
                }
            }
            set

            {
                bool isnew = true;
                for (int i = 0; i < processes.Count; i++)
                {
                    ProcessSame same = processes[i].CompareTo(value);
                    if (same == ProcessSame.Same|| same == ProcessSame.SameProcessDifferentTitle)
                    {
                        isnew = false;
                        processes.Insert(0, value);
                        processes.RemoveAt(i +1);
                        break;
                    }
                }
                if (isnew)
                {
                    processes.Insert(0, value);
                    if (processes.Count > 5)
                    {
                        processes.RemoveAt(processes.Count - 1);
                    }
                }
               
            }
        }

        public string GetCurUrl()
        {
            return "21";
        }
        public string Title
        {
            set {; }
            get
            {
                return CurrentProcess?.MainWindowTitle;
            }
        }
        public string Path
        {
            set {; }
            get
            {
                return CurrentProcess?.MainModule.FileName;
            }
        }
    }
}
