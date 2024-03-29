﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;
using SuperKanban.Model.Control;
using System.Text.RegularExpressions;
using SuperKanban.Interop;
using System.Diagnostics;

namespace SuperKanban.Model.Entities
{
    public enum Conclusion
    {
        Allow = 1,
        BlockApp = 2,
        BlockScreen = 4,
        None=8

    }
    public enum RuleFunc
    {
        Title = 0,
        Url = 1,
        Path = 2,
        Info = 3,
        Muti = 4,
        Screen = 5,
        ImageDetect = 6,

    }
    public class AppRuleOne
    {

        public string Title { get; set; }
        public string Path { get; set; }
        private RuleFunc ruleFunc;
        private List<string> paras;
        private string rulefuncparas;
        public string RuleFuncParas
        {
            get { return rulefuncparas; }
            set
            {
                rulefuncparas = value;
                rulefuncparas.Replace("；", ";");
                paras = rulefuncparas.Split(";").ToList();//去除空元素
                ;
            }
        }

        public RuleFunc RuleFunc
        {
            get
            {
                return ruleFunc;
            }
            set
            {
                ;
                ruleFunc = value;
            }
        }

        public Conclusion Check(UserApp app)
        {
            if (app.AppType == AppType.Screen)
            {
                if (RuleFunc == RuleFunc.Screen)
                {
                    return Conclusion.BlockScreen;
                }
                else
                {
                    return Conclusion.None;
                }
            }
            else
            {
                switch (RuleFunc)
                {
                    case RuleFunc.Title:
                        return Regex.IsMatch(app.Title, NativeMethods.WildCardToRegex(paras.ElementAtOrDefault(0) ?? ""), RegexOptions.IgnoreCase)
                            ? Conclusion.BlockApp
                            : Conclusion.None;
                        break;
                    case RuleFunc.Url:
                        if (app.Url == null) return Conclusion.None;
                        return Regex.IsMatch(app.Url, NativeMethods.WildCardToRegex(paras.ElementAtOrDefault(0) ?? ""), RegexOptions.IgnoreCase)
                            ? Conclusion.BlockApp
                            : Conclusion.None;
                        break;
                    case RuleFunc.Path:
                        return Regex.IsMatch(app.Title, NativeMethods.WildCardToRegex(paras.ElementAtOrDefault(0) ?? ""), RegexOptions.IgnoreCase)
                            ? Conclusion.BlockApp
                            : Conclusion.None;
                        break;
                    //case RuleFunc.Info:
                    //    return Regex.IsMatch(app., NativeMethods.WildCardToRegex(paras.ElementAtOrDefault(0) ?? ""), RegexOptions.IgnoreCase)
                    //        ? Conclusion.App
                    //        : Conclusion.None;
                    //    break;
                    case RuleFunc.Muti:
                        return Regex.IsMatch(app.Title, NativeMethods.WildCardToRegex(paras.ElementAtOrDefault(0) ?? "*"), RegexOptions.IgnoreCase) &&
                            Regex.IsMatch(app.Url, NativeMethods.WildCardToRegex(paras.ElementAtOrDefault(1) ?? "*"), RegexOptions.IgnoreCase) &&
                            Regex.IsMatch(app.Path, NativeMethods.WildCardToRegex(paras.ElementAtOrDefault(2) ?? "*"), RegexOptions.IgnoreCase)
                            ? Conclusion.BlockApp
                            : Conclusion.None;
                        break;
                    case RuleFunc.Screen:
                        return Conclusion.None;
                    case RuleFunc.ImageDetect:
                        return Conclusion.None;
                    default:
                        return Conclusion.None;
                        break;
                }
            }

        
        }
    }
    public class AppRule
    {
        private bool active;

        public ObservableCollection<AppRuleOne> AppRuleOneList { get; set; } = new ObservableCollection<AppRuleOne>();
        //public ObservableCollection<TimeRuleOne> TimeRuleOneList { get; set; } = new ObservableCollection<TimeRuleOne>();
        public TimeLimit TimeLimit { get; set; }  =new TimeLimit();
        public bool Editable { get; set; }
        public bool Active { get=>active; set {
                 active=value ;
                if (Active_changed != null)
                {
                    Active_changed();
                }
            } }
        public static Action Active_changed;

        //public AppRule()
        //{
        //}

        //返回是否通过检查
        public Conclusion check(UserApp app=null)
        {
            while (TimeLimit.AppTimeOut != null)
            {//清空和之前app的代理关系。
                TimeLimit.AppTimeOut -= TimeLimit.AppTimeOut;
            }
            Debug.WriteLine(Process.GetCurrentProcess().Id);
            Conclusion ret=Conclusion.None;
            foreach (AppRuleOne item in AppRuleOneList)
            {
                ret |= item.Check(app);
            }

            if(ret == Conclusion.None)
            {
                return Conclusion.None;
            }
            else
            {
                var remaintime= TimeLimit.check();
                if (remaintime == int.MaxValue)
                {
                    return Conclusion.Allow;
                }
                else
                {
                    app.IamRunning += TimeLimit.update;//委托，用于计算时间。
                    if (app.AppType == AppType.Screen)
                    {
                        TimeLimit.ScreenTimeOut += app.KillSelf;
                    }
                    else
                    {
                        TimeLimit.AppTimeOut += app.KillSelf;
                    }
                    ret = remaintime <= 0 ? ret : Conclusion.Allow;

                }
            }

            return ret;
        }
    }
}
