using System;
using System.Collections.Generic;
using System.Text;
using SuperKanban.Model.Entities;
using SuperKanban.Commands;
using System.Windows.Input;
using GalaSoft.MvvmLight;
namespace SuperKanban.ViewModel
{
    public class AppRuleViewModel : ViewModelBase
    {
        public AppRuleViewModel()
        {
            AddAppRuleOneCommand = new RelayCommand(AddAppRuleOne, o => true);
        }
        public AccrTimeSelectorViewModel AccrTimeSelectorViewModel { get; set; } = new AccrTimeSelectorViewModel();

        private AppRule appRule;

        public AppRule AppRule
        {
            get { return appRule; }
            set
            {
                appRule = value;
                AccrTimeSelectorViewModel.TimeLimit = appRule.TimeLimit;
            }
        }
        public ICommand AddAppRuleOneCommand { get; }


        public IEnumerable<string> Foods => new[] { "Burger", "Fries", "Shake", "Lettuce" };


        private void AddAppRuleOne(object parameters)
        {
            var ruleone = new AppRuleOne();
            AppRule.AppRuleOneList.Add(ruleone);
        }


    }
}
