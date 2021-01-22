using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SuperKanban.Model.Control
{
    public class Monitor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public void Execute()
        {
            var timer = new System.Timers.Timer();
            timer.Elapsed += detect;

            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Start();
            while (true) {; }
        }
        private static Process last_process ;

        public List<UserApp> UserApps { get; set; } = new List<UserApp>();
         
        public UserApp CurrentApp
        {
            get{
                if (UserApps.Count > 0)
                {
                    return UserApps[0];

                }
                else
                {
                    return null;
                }

            }
            set
            {
             
            }
        }
        private void detect(object sender, System.Timers.ElapsedEventArgs e)
        {
            IntPtr handle = GetForegroundWindow();   //获取当前窗口句柄;
            uint currentThreadPid;
            GetWindowThreadProcessId(handle, out currentThreadPid);
            Process process = Process.GetProcessById((int)currentThreadPid);
            if (process == null || process?.Id <100) return;
            ProcessSame same = process.CompareTo(last_process);
            if (same == ProcessSame.Same)
            {
                return;
            }
            else if(same == ProcessSame.Different)
            {
                bool isnew = true;
                for (int i = 0; i < UserApps.Count(); i++)
                {
                    if (UserApps[i].Path == process.MainModule.FileName)
                    {
                        UserApps[i].CurrentProcess = process;
                        UserApps.Insert(0, UserApps[i]);
                        UserApps.RemoveAt(i + 1);
                        isnew = false;
                        break;
                    }
                }
                if (isnew)
                {
                    UserApps.Insert(0, new UserApp(process));
                }
            }
            else if (same == ProcessSame.SameProcessDifferentTitle || same == ProcessSame.SamePathDifferentProcess)
            {
                CurrentApp.CurrentProcess = process;
            }
           
         
        }
    }
}
