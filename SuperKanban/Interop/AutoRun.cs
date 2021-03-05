using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using IWshRuntimeLibrary;
using System.Diagnostics;
using Microsoft.Win32;


namespace SuperKanban.Interop
{
    //https://www.cnblogs.com/mq0036/p/12117955.html
    class AutoRun
    {

        static public AutoRun defalut=new AutoRun();
        /// <summary>
        /// 快捷方式名称-任意自定义
        /// </summary>
        private const string QuickName = "TCNVMClient";

        /// <summary>
        /// 自动获取系统自动启动目录
        /// </summary>
        private string systemStartPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.Startup); } }

        /// <summary>
        /// 自动获取程序完整路径
        /// </summary>
        private string appAllPath { get { return Process.GetCurrentProcess().MainModule.FileName; } }

        /// <summary>
        /// 自动获取桌面目录
        /// </summary>
        private string desktopPath { get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); } }

        /// <summary>
        /// 设置开机自动启动-只需要调用改方法就可以了参数里面的bool变量是控制开机启动的开关的，默认为开启自启启动
        /// </summary>
        /// <param name="onOff">自启开关</param>
        public void SetMeAutoStart(bool onOff = true)
        {
            if (onOff)//开机启动
            {
                //获取启动路径应用程序快捷方式的路径集合
                List<string> shortcutPaths = GetQuickFromFolder(systemStartPath, appAllPath);
                //存在2个以快捷方式则保留一个快捷方式-避免重复多于
                if (shortcutPaths.Count >= 2)
                {
                    for (int i = 1; i < shortcutPaths.Count; i++)
                    {
                        DeleteFile(shortcutPaths[i]);
                    }
                }
                else if (shortcutPaths.Count < 1)//不存在则创建快捷方式
                {
                    CreateShortcut(systemStartPath, QuickName, appAllPath, "中吉售货机");
                }
            }
            else//开机不启动
            {
                //获取启动路径应用程序快捷方式的路径集合
                List<string> shortcutPaths = GetQuickFromFolder(systemStartPath, appAllPath);
                //存在快捷方式则遍历全部删除
                if (shortcutPaths.Count > 0)
                {
                    for (int i = 0; i < shortcutPaths.Count; i++)
                    {
                        DeleteFile(shortcutPaths[i]);
                    }
                }
            }
            //创建桌面快捷方式-如果需要可以取消注释
            //CreateDesktopQuick(desktopPath, QuickName, appAllPath);
        }

        /// <summary>
        ///  向目标路径创建指定文件的快捷方式
        /// </summary>
        /// <param name="directory">目标目录</param>
        /// <param name="shortcutName">快捷方式名字</param>
        /// <param name="targetPath">文件完全路径</param>
        /// <param name="description">描述</param>
        /// <param name="iconLocation">图标地址</param>
        /// <returns>成功或失败</returns>
        private bool CreateShortcut(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null)
        {
            try
            {
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);                         //目录不存在则创建
                //添加引用 Com 中搜索 Windows Script Host Object Model
                string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));          //合成路径
                WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);    //创建快捷方式对象
                shortcut.TargetPath = targetPath;                                                               //指定目标路径
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);                                  //设置起始位置
                shortcut.WindowStyle = 1;                                                                       //设置运行方式，默认为常规窗口
                shortcut.Description = description;                                                             //设置备注
                shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;    //设置图标路径
                shortcut.Save();                                                                                //保存快捷方式
                return true;
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
                temp = "";
            }
            return false;
        }

        /// <summary>
        /// 获取指定文件夹下指定应用程序的快捷方式路径集合
        /// </summary>
        /// <param name="directory">文件夹</param>
        /// <param name="targetPath">目标应用程序路径</param>
        /// <returns>目标应用程序的快捷方式</returns>
        private List<string> GetQuickFromFolder(string directory, string targetPath)
        {
            List<string> tempStrs = new List<string>();
            tempStrs.Clear();
            string tempStr = null;
            string[] files = Directory.GetFiles(directory, "*.lnk");
            if (files == null || files.Length < 1)
            {
                return tempStrs;
            }
            for (int i = 0; i < files.Length; i++)
            {
                //files[i] = string.Format("{0}\\{1}", directory, files[i]);
                tempStr = GetAppPathFromQuick(files[i]);
                if (tempStr == targetPath)
                {
                    tempStrs.Add(files[i]);
                }
            }
            return tempStrs;
        }

        /// <summary>
        /// 获取快捷方式的目标文件路径-用于判断是否已经开启了自动启动
        /// </summary>
        /// <param name="shortcutPath"></param>
        /// <returns></returns>
        private string GetAppPathFromQuick(string shortcutPath)
        {
            //快捷方式文件的路径 = @"d:\Test.lnk";
            if (System.IO.File.Exists(shortcutPath))
            {
                WshShell shell = new WshShell();
                IWshShortcut shortct = (IWshShortcut)shell.CreateShortcut(shortcutPath);
                //快捷方式文件指向的路径.Text = 当前快捷方式文件IWshShortcut类.TargetPath;
                //快捷方式文件指向的目标目录.Text = 当前快捷方式文件IWshShortcut类.WorkingDirectory;
                return shortct.TargetPath;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据路径删除文件-用于取消自启时从计算机自启目录删除程序的快捷方式
        /// </summary>
        /// <param name="path">路径</param>
        private void DeleteFile(string path)
        {
            FileAttributes attr = System.IO.File.GetAttributes(path);
            if (attr == FileAttributes.Directory)
            {
                Directory.Delete(path, true);
            }
            else
            {
                System.IO.File.Delete(path);
            }
        }

        /// <summary>
        /// 在桌面上创建快捷方式-如果需要可以调用
        /// </summary>
        /// <param name="desktopPath">桌面地址</param>
        /// <param name="appPath">应用路径</param>
        public void CreateDesktopQuick(string desktopPath = "", string quickName = "", string appPath = "")
        {
            List<string> shortcutPaths = GetQuickFromFolder(desktopPath, appPath);
            //如果没有则创建
            if (shortcutPaths.Count < 1)
            {
                CreateShortcut(desktopPath, quickName, appPath, "软件描述");
            }
        }

    }


    public class AutoRunByRegistry
    {
        /// <summary>
        /// 将本程序设为开启自启
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <returns></returns>
        public static bool SetMeStart(bool onOff)
        {
            bool isOk = false;
            string appName = Process.GetCurrentProcess().MainModule.ModuleName;
            string appPath = Process.GetCurrentProcess().MainModule.FileName;
            isOk = SetAutoStart(onOff, appName, appPath);
            return isOk;
        }

        /// <summary>
        /// 将应用程序设为或不设为开机启动
        /// </summary>
        /// <param name="onOff">自启开关</param>
        /// <param name="appName">应用程序名</param>
        /// <param name="appPath">应用程序完全路径</param>
        public static bool SetAutoStart(bool onOff, string appName, string appPath)
        {
            bool isOk = true;
            //如果从没有设为开机启动设置到要设为开机启动
            if (!IsExistKey(appName) && onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            //如果从设为开机启动设置到不要设为开机启动
            else if (IsExistKey(appName) && !onOff)
            {
                isOk = SelfRunning(onOff, appName, @appPath);
            }
            return isOk;
        }

        /// <summary>
        /// 判断注册键值对是否存在，即是否处于开机启动状态
        /// </summary>
        /// <param name="keyName">键值名</param>
        /// <returns></returns>
        private static bool IsExistKey(string keyName)
        {
            try
            {
                bool _exist = false;
                RegistryKey local = Registry.LocalMachine;
                RegistryKey runs = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (runs == null)
                {
                    RegistryKey key2 = local.CreateSubKey("SOFTWARE");
                    RegistryKey key3 = key2.CreateSubKey("Microsoft");
                    RegistryKey key4 = key3.CreateSubKey("Windows");
                    RegistryKey key5 = key4.CreateSubKey("CurrentVersion");
                    RegistryKey key6 = key5.CreateSubKey("Run");
                    runs = key6;
                }
                string[] runsName = runs.GetValueNames();
                foreach (string strName in runsName)
                {
                    if (strName.ToUpper() == keyName.ToUpper())
                    {
                        _exist = true;
                        return _exist;
                    }
                }
                return _exist;

            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 写入或删除注册表键值对,即设为开机启动或开机不启动
        /// </summary>
        /// <param name="isStart">是否开机启动</param>
        /// <param name="exeName">应用程序名</param>
        /// <param name="path">应用程序路径带程序名</param>
        /// <returns></returns>
        private static bool SelfRunning(bool isStart, string exeName, string path)
        {
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey key = local.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (key == null)
                {
                    local.CreateSubKey("SOFTWARE//Microsoft//Windows//CurrentVersion//Run");
                }
                //若开机自启动则添加键值对
                if (isStart)
                {
                    key.SetValue(exeName, path);
                    key.Close();
                }
                else//否则删除键值对
                {
                    string[] keyNames = key.GetValueNames();
                    foreach (string keyName in keyNames)
                    {
                        if (keyName.ToUpper() == exeName.ToUpper())
                        {
                            key.DeleteValue(exeName);
                            key.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string ss = ex.Message;
                return false;
                //throw;
            }

            return true;
        }
    }
}
