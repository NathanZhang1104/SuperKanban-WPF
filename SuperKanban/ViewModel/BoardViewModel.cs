using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using DeepCopy;
using SuperKanban.Database;
using SuperKanban.View;
using SuperKanban.Commands;
using SuperKanban.Model.Entities;
using GalaSoft.MvvmLight;
using SuperKanban.ViewModel;
using Syncfusion.UI.Xaml.Kanban;
namespace SuperKanban.ViewModel
{
    public class BoardViewModel : ViewModelBase
    {
        public BoardView BoardWindow { get; set; }

        private Board _board;

        public Board Board
        {
            get => _board;
            set
            {
                _board = value;
                RaisePropertyChanged();
            }
        }

        private Card _selectedCard=new Card();

        public Card SelectedCard
        {
            get => _selectedCard;
            set
            {
                CardShowViewModel = new CardShowViewModel() { Card = value };
                _selectedCard.IsSelected = false;
                _selectedCard = value;
                _selectedCard.IsSelected = true;

                RaisePropertyChanged();
                RaisePropertyChanged("CardShowViewModel");
                CardShowViewModel.RaisePropertyChanged("SelectedCard");
            }
        }

        private SubTask _newSubTask = new SubTask();
        public int aaa { get; set; }
        public SubTask NewSubTask
        {
            get => _newSubTask;
            set
            {
                _newSubTask = value;
                RaisePropertyChanged();
            }
        }

        private int _taskViewWidth;

        public int TaskViewWidth
        {
            get => _taskViewWidth;
            set
            {
                _taskViewWidth = value;
                RaisePropertyChanged();
            }
        }
        public CardShowViewModel CardShowViewModel { get; set; } = new CardShowViewModel();
        public ObservableCollection<CardViewModel> CardViewModels { get; set; } = new ObservableCollection<CardViewModel>();



        public ICommand CloseBoardCommand { get; }
        public ICommand AddTagCommand { get; }
        public ICommand AddSubTaskCommand { get; }
        public ICommand DeleteSubTaskCommand { get; }
        public ICommand DeleteTagCommand { get; }
        public ICommand DeleteCardCommand { get; }
        public ICommand CloseTaskViewCommand { get; }
        public ICommand AddCardCommand { get; }
        public ICommand SaveBoardCommand { get; }
        public ICommand AddColumnCommand { get; }
        public ICommand MoveColumnCommand { get; }

        public BoardViewModel()
        {
            Board = new Board();
            CloseBoardCommand = new RelayCommand(CloseBoardShowMain, o => true);
            AddCardCommand = new RelayCommand(AddCard, o => true);
            //AddTagCommand = new RelayCommand(AddNewTag, o => !string.IsNullOrWhiteSpace(o.ToString()));
            AddSubTaskCommand = new RelayCommand(AddNewSubTask, o => !string.IsNullOrWhiteSpace(_newSubTask.Title));
            SaveBoardCommand = new RelayCommand(SaveBoard, o => true);
            DeleteSubTaskCommand = new RelayCommand(o => SelectedCard.SubTasks.Remove((SubTask)o), o => true);
            DeleteTagCommand = new RelayCommand(o => SelectedCard.Tags.Remove((Tag)o), o => true);
            DeleteCardCommand = new RelayCommand(RemoveSelectedCard, o => SelectedCard != null);
            CloseTaskViewCommand = new RelayCommand(o => TaskViewWidth = 0, o => true);
            AddColumnCommand = new RelayCommand(AddColumn, o => true);
            MoveColumnCommand = new RelayCommand(MoveColumn, o => true);


        }

        private void CloseBoardShowMain(object parameter)
        {
            SaveBoard(parameter);
            //App.WindowService.ShowMainCloseBoard();
        }

        private void RemoveSelectedCard(object parameter)
        {
            if(parameter as Card == null)
            {
                Board.Cards.Remove(SelectedCard);
            }
            else
            {
                Board.Cards.Remove(parameter as Card);

            }
        }

        private void AddNewSubTask(object parameter)
        {
            SelectedCard.SubTasks.Add(new SubTask { Completed = NewSubTask.Completed, Title = NewSubTask.Title });
            BoardWindow.ClearSubTaskEntry();
        }

        //private void AddNewTag(object parameter)
        //{
        //    SelectedCard.Tags.Add(new Tag { Name = parameter.ToString() });
        //    BoardWindow.ClearTaskTagEntry();
        //}

        private void AddCard(object parameter)
        {
            Card newcard;
            var inputvard = parameter as Card;
            if (inputvard == null)
            {
                newcard = new Card
                {
                    BoardId = Board.Id,
                    Category = (string)parameter,
                    CreatedAt = DateTime.Now,
                    Description = "",
                    Title = "",
                    SubTasks = new ObservableCollection<SubTask>(),
                    Priority = "None",
                    Tags = new ObservableCollection<Tag>()
                };
                SelectedCard = newcard;
                Board.Cards.Insert(0, newcard);

            }
            else
            {
                newcard = DeepCopier.Copy<Card>(inputvard);
                SelectedCard = newcard;
                Board.Cards.Add(newcard);

            }
            if (string.IsNullOrWhiteSpace(BoardWindow.CardShowView.title_text.Text))
            {
                BoardWindow.CardShowView.title_text.Focus();
            }
        }

        public void SaveBoard(object parameter)
        {
            //App.UnitOfWork.Boards.Update(Board);
        }

        private void AddColumn(object parameter) {
            var cur_index = BoardWindow.sfKanban.Columns.IndexOf(parameter as KanbanColumn);
            BoardWindow.sfKanban.Columns.Insert(cur_index+1,new KanbanColumn() { Title = "untitle", Categories = "dwdw" }); ;


        }

        private void MoveColumn(object parameter)
        {
            var cur_index=BoardWindow.sfKanban.Columns.IndexOf(parameter as KanbanColumn);
            BoardWindow.sfKanban.Columns.Move(cur_index, cur_index + 1);

        }
    }
}
