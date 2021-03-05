using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Windows.Automation;
using SuperKanban.Interop;
using SuperKanban.Model.Entities;

namespace SuperKanban.Model.Control
{
    public class Monitor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static int timerInterval=500;

        public static UserApp ScreenApp = new UserApp() { AppType = AppType.Screen };
        public  void AuditScreen()//审核屏幕
        {
            while (ScreenApp.IamRunning != null)
            {
                ScreenApp.IamRunning -= ScreenApp.IamRunning;
            }
            AduitAction(ScreenApp);
        }
        public Action<UserApp> AduitAction;
        public void Execute()
        {
            AuditScreen();

            var timer = new System.Timers.Timer();
            timer.Elapsed += detect;

            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Interval = timerInterval;
            timer.Start();
        }
        private static Process last_process ;

        public Monitor()
        {
            AppRule.Active_changed+=AuditScreen;
            GlobalFinder.Loaded += AuditScreen;
        }

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
            DateTime t1 = DateTime.Now;

            IntPtr handle = GetForegroundWindow();   //获取当前窗口句柄;
            uint currentThreadPid;
            GetWindowThreadProcessId(handle, out currentThreadPid);
            Process process = Process.GetProcessById((int)currentThreadPid) ;
           //var ret=  System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            if (process == null || process?.Id < 100)
            {//无法获取或者锁屏
                return;
            }

            try//测试进程可读
            {
                var mM=process.MainModule;
                if (process.MainModule.FileName.EndsWith("LockApp.exe"))//"锁屏"
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }
            if (ScreenApp.IamRunning != null)// 屏幕正在使用
            {
                ScreenApp.IamRunning();

            }
            ProcessSame same = process.CompareTo(last_process);
            if (same == ProcessSame.Same)
            {
                if (CurrentApp.IamRunning != null)
                {
                    CurrentApp.IamRunning();
                }
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
       
            if (CurrentApp.AppType == AppType.Browser)
            {
                var url= CurrentApp.UpdateCurUrl();
                TimeSpan ts1 = DateTime.Now - t1;
                Debug.WriteLine(ts1);
                Debug.WriteLine(url);

            }
            last_process = process;
            
            while (CurrentApp.IamRunning!= null)
            {
                CurrentApp.IamRunning-= this.CurrentApp.IamRunning;
            }
            AduitAction(CurrentApp);
            if (CurrentApp.IamRunning != null)
            {
                CurrentApp.IamRunning();
            }

        }
    }
}
