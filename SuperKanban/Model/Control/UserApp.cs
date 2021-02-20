using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using SuperKanban.Interop;

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
            if (processes.MainModule.FileName.Contains("edge.exe"))
            {
                this.AppType = AppType.Browser;
                this.browserType = BrowserType.MSEDGE;
            }
            this.CurrentProcess = processes;
        }
        public AppType AppType{get;set;}
        private BrowserType browserType { get; set; }

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

        public string Url { get; set; }
        public string UpdateCurUrl()
        {
             Url= UiaMethods.GetURLFromProcess(CurrentProcess, browserType);

            return Url;
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

        public bool KillSelf()
        {
            if (this.AppType == AppType.Browser)
            {
                NativeMethods.ControlKey(87);
            }
            return true;
        }
    }
}
