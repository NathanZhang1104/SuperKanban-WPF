using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using SuperKanban.Model.Entities;
using SuperKanban.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using SuperKanban.View;
namespace SuperKanban.ViewModel
{
    public class CardShowViewModel : ViewModelBase
    {
        private Card card;
        public ICommand AddTagCommand { get; }
        public ICommand AddSubTaskCommand { get; }
        public ICommand RunDialogCommand { get; }
        public bool ShowV { get; set; }
        public CardShowViewModel()
        {
            AddTagCommand = new RelayCommand(AddNewTag, o => !string.IsNullOrWhiteSpace((o as TextBox).Text.ToString()));
            AddSubTaskCommand = new RelayCommand(AddSubTask, o => !string.IsNullOrWhiteSpace((o as TextBox).Text.ToString()));
            RunDialogCommand = new RelayCommand(ExecuteRunDialog, o => true);


        }

        public Card Card
        {
            get => card;
            set
            {
                card = value;
                RaisePropertyChanged();
            }
        }

        public void AddNewTag(object parameter)
        {

            var textBlock = parameter as TextBox;


            Card.Tags.Add(new Tag { Name = textBlock.Text.ToString() });
            textBlock.Text = "";
        }

        public void AddSubTask(object parameter)
        {

            var textBlock = parameter as TextBox;

            Card.SubTasks.Add(new SubTask { Title = textBlock.Text.ToString(), Completed = false });
            textBlock.Text = "";
            Card.RaisePropertyChanged("SubTasks");
        }
        public string Category
        {
            get
            {
                return Card.Category;
            }
            set
            {
                Card.Category = value;
            }
        }

        private void ClosingEventHandler(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
            => Console.WriteLine("You can intercept the closing event, and cancel here.");

        private async void ExecuteRunDialog(object parameters)
        {
            var view = new AppRuleView
            {
                DataContext = new AppRuleViewModel() { AppRule = Card.AppRule }
            };

            //show the dialog
            var result = await MaterialDesignThemes.Wpf.DialogHost.Show(view, "RootDialog", ClosingEventHandler);

            //check the result...
            Console.WriteLine("Dialog was closed, the CommandParameter used to close it was: " + (result ?? "NULL"));

        }
    }
}
