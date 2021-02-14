using System;
using System.Collections.Generic;
using System.Text;
using SuperKanban.Model.Entities;
using GalaSoft.MvvmLight;
namespace SuperKanban.ViewModel
{
    public class AccrTimeSelectorViewModel : ViewModelBase
    {
        public AccrTimeSelectorViewModel()
        {
        }
        public IEnumerable<LimitType> LimitTypes => new[] { LimitType.Block,LimitType.Limit };
        public IEnumerable<string> LimitTimes=> new[] { "15分钟", "30分钟" , "45分钟" , "60分钟" , "90分钟" , "120分钟" };

        public TimeRuleOne CurrentTimeRuleOne;
        public LimitType CurrentType { get; set; } = LimitType.Block;
        public TimeLimit TimeLimit { get; set; } = new TimeLimit();

    }
}
