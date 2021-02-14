using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace SuperKanban.Model.Entities
{
    public class AppRuleOne
    {
        public string Title { get; set; }
        public string Path { get; set; }
        public string Info { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
    }
    public class AppRule
    {
        public ObservableCollection<AppRuleOne> AppRuleOneList { get; set; } = new ObservableCollection<AppRuleOne>();
        //public ObservableCollection<TimeRuleOne> TimeRuleOneList { get; set; } = new ObservableCollection<TimeRuleOne>();
        public TimeLimit TimeLimit = new TimeLimit();
    }
}
