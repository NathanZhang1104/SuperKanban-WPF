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
using SuperKanban.View;
using System.Linq;
using System.Collections.Generic;
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
                //_selectedCard.IsSelected = false;
                _selectedCard = value;
                //_selectedCard.IsSelected = true;

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
        public ICommand DeleteColumnCommand { get; }
        public ICommand CopyColumnCommand { get; }


        public BoardViewModel()
        {
            Board = new Board();
            s_AddColumnCount = randomer.NextDouble();
            CloseBoardCommand = new RelayCommand(CloseBoardShowMain, o => true);
            AddCardCommand = new RelayCommand(AddCard, o => true);
            //AddTagCommand = new RelayCommand(AddNewTag, o => !string.IsNullOrWhiteSpace(o.ToString()));
            AddSubTaskCommand = new RelayCommand(AddNewSubTask, o => !string.IsNullOrWhiteSpace(_newSubTask.Title));
            SaveBoardCommand = new RelayCommand(SaveBoard, o => true);
            DeleteSubTaskCommand = new RelayCommand(o => SelectedCard.SubTasks.Remove((SubTask)o), o => true);
            DeleteTagCommand = new RelayCommand(o => SelectedCard.Tags.Remove((Tag)o), o => true);
            DeleteCardCommand = new RelayCommand(RemoveSelectedCard, o => SelectedCard.Editable);
            CloseTaskViewCommand = new RelayCommand(o => TaskViewWidth = 0, o => true);
            AddColumnCommand = new RelayCommand(AddColumn, o => true);
            MoveColumnCommand = new RelayCommand(MoveColumn, o => true);
            CopyColumnCommand = new RelayCommand(CopyColumn, o => true);
            DeleteColumnCommand = new RelayCommand(DeleteColumn, o => true);
    


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
                var card = parameter as Card;
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
                    Category = (string)(parameter as KanbanColumn).Categories,
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
            BoardWindow.CardShowView.ShowMe = true;
            if (string.IsNullOrWhiteSpace(BoardWindow.CardShowView.title_text.Text))
            {
                BoardWindow.CardShowView.title_text.Focus();
            }
        }


        public void SaveBoard(object parameter)
        {
            //App.UnitOfWork.Boards.Update(Board);
        }

        private double s_AddColumnCount;
        private Random randomer = new Random();

        private void AddColumn(object parameter) {
            var cur_index = BoardWindow.sfKanban.Columns.IndexOf((parameter as KanbanColumn));
            BoardWindow.sfKanban.Columns.Insert(cur_index+1,new KanbanColumn() { Title = "", Categories = Interop.NativeMethods.GetRnd(6, custom: "@")});
            //Board.BoardColumns.Insert(cur_index + 1, new BoardColumn(  "", s_AddColumnCount.ToString()));
        }
        private void CopyColumn(object parameter)
        {
            var colum = parameter as KanbanColumn;
            var cur_index = BoardWindow.sfKanban.Columns.IndexOf(colum);
            var newcolumn = new KanbanColumn() { Title = colum.Title, Categories = Interop.NativeMethods.GetRnd(6, custom: "@") };
            BoardWindow.sfKanban.Columns.Insert(cur_index + 1, newcolumn);
            //Board.BoardColumns.Insert(cur_index + 1, new BoardColumn(colum.Title as string, s_AddColumnCount.ToString()));
            var toRemove = Board.Cards.Where(x => x.Category == colum.Categories).ToList();

            foreach (var item in toRemove)
            {
                Card newcard = DeepCopier.Copy(item);
                newcard.Category = newcolumn.Categories;

                Board.Cards.Add(newcard);
            }

        }
        private void DeleteColumn(object parameter)
        {
            var colum = parameter as KanbanColumn;
            var toRemove = Board.Cards.Where(x => x.Category == colum.Categories).ToList();
            foreach (var item in toRemove)
                Board.Cards.Remove(item);
            //Board.BoardColumns.RemoveAt(BoardWindow.sfKanban.Columns.IndexOf(colum));
            BoardWindow.sfKanban.Columns.Remove(colum);
        }
        private void MoveColumn(object parameter)
        {
            var header = parameter as  View.Templates.ColumHeader;
            var drop_index=BoardWindow.sfKanban.Columns.IndexOf(header.KanbanColumn);
            var drag_index = BoardWindow.sfKanban.Columns.IndexOf(header.Dropin_KanbanColumn);
            if (drop_index == drag_index) return;
            else if (drag_index < drop_index)
            {
                if (header.left)
                {
                    drop_index -= 1;
                }

            }
            else if(drag_index > drop_index)
            {
                if (!header.left)
                {
                    drop_index += 1;
                }

            }

            if (drop_index == drag_index) return;
            //BoardWindow.sfKanban.Columns.Add(null);
            if (drop_index< BoardWindow.sfKanban.Columns.Count)
            {
                BoardWindow.sfKanban.Columns.Move(drag_index, drop_index);
                //Board.BoardColumns.Move(drag_index, drop_index);

            }
            else
            {
                for (int i = 0; i < drop_index-1-drag_index; i++)
                {
                    BoardWindow.sfKanban.Columns.Move(drop_index-1, drag_index);
                    //Board.BoardColumns.Move(drop_index - 1, drag_index);

                }
            }
            //BoardWindow.sfKanban.Columns.RemoveAt(BoardWindow.sfKanban.Columns.Count - 1);

        }
    }
}
