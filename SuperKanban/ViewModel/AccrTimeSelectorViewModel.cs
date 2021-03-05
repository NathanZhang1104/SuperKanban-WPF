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


        public IEnumerable<int> LimitTimes=> new[] {5, 15, 30 , 45 , 60 , 90 , 120,150,180,240,300,360,420,480 };

        public TimeRuleOne CurrentTimeRuleOne;
        public LimitType CurrentType { get; set; } = LimitType.Block;
        public TimeLimit TimeLimit { get; set; } = new TimeLimit();

    }
}
