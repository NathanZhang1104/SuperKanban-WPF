using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
namespace SuperKanban.Model.Entities
{
    public class Card : ViewModelBase
    {
        private string title;
        private string description;
        public int Id { get; set; }
        public int BoardId { get; set; }
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

        public AppRule AppRule { get; set; } = new AppRule();
        public bool IsSelected
        {
            get => isSelected; set
            {
                isSelected = value;
                RaisePropertyChanged();
            }
        }


        private static int a = 0;
    }
}