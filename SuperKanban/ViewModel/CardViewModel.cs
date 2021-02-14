using System;
using System.Collections.Generic;
using System.Text;
using SuperKanban.Model.Entities;
using GalaSoft.MvvmLight;
using System.Windows.Controls;
using System.Windows.Input;
using SuperKanban.Commands;

namespace SuperKanban.ViewModel
{
    public class CardViewModel : ViewModelBase
    {
        private Card card;
        public ICommand AddTagCommand { get; }
        public CardViewModel()
        {
            AddTagCommand = new RelayCommand(AddNewTag, o => !string.IsNullOrWhiteSpace((o as TextBox).Text.ToString()));

        }

        public Card Card
        {
            get => card;
            set {
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
        public string Category { 
            get
            {
                return Card.Category;
            }
            set
            {
                Card.Category = value;
            }
        }
    }
}
