using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DeepCopy;
using GalaSoft.MvvmLight;
namespace SuperKanban.Model.Entities
{


    public class Card : ViewModelBase
    {
        
        private string title;
        private string description="";
        private bool editable = true;
        public string Id { get; set; }
        public int BoardId { get; set; }
        public int Index { get; set; } = 0;
        public Card()
        { 

            Id=Interop.NativeMethods.GetRnd(8,custom:"#");
            SLock = new SLock(Id);
            Pomodoro = new Pomodoro();
            SubTasks = new ObservableCollection<SubTask>();
            Tags = new ObservableCollection<Tag>();

        }

        public Card (Card card)
        {

            Id = Interop.NativeMethods.GetRnd(8, custom: "#");
            SLock = new SLock(Id);
            Pomodoro = new Pomodoro();
            Title = DeepCopier.Copy(card.Title);
            Tags = DeepCopier.Copy(card.Tags);
            description = DeepCopier.Copy(card.description);
            AppRule = DeepCopier.Copy(card.AppRule) ;
            SubTasks = DeepCopier.Copy(card.SubTasks);
            BoardId = card.BoardId;
            Category = card.Category;
            SubTasksShow = card.SubTasksShow;
            PomodoroShow = card.PomodoroShow;
            AppRuleShow = card.AppRuleShow;
            SLockShow = card.SLockShow;
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged();
                ;
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description=value;
                RaisePropertyChanged();
            }
        }
        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
        public ObservableCollection<SubTask> SubTasks
        {
            get;set;
        }
        private bool isSelected;
        private bool subTasksShow=false;
        private bool pomodoroShow=false;
        private bool appRuleShow=false;
        private bool sLockShow=false;

    


        public AppRule AppRule { get; set; } = new AppRule();
        public bool IsSelected
        {
            get => isSelected; set
            {
                isSelected = value;
                RaisePropertyChanged();
            }
        }
        public SLock SLock { get; set; }
        public Pomodoro Pomodoro { get; set; }
        public bool Editable { get { return editable; }
            set { editable = value;
                AppRule.Editable = value; }
        }

        public bool SubTasksShow { get => subTasksShow; set { subTasksShow = value;
                if (subTasksShow == false)
                {
                    SubTasks.Clear();
                }
                RaisePropertyChanged();
            } }
        public bool PomodoroShow { get => pomodoroShow; set { pomodoroShow = value;
                RaisePropertyChanged();
            } }
        public bool AppRuleShow { get => appRuleShow; set { if ((!AppRule.Active && !value)||value) { appRuleShow = value; }
                RaisePropertyChanged();
            } }
        public bool SLockShow { get => sLockShow; set { sLockShow = value;
                RaisePropertyChanged();
            } }
    }
}