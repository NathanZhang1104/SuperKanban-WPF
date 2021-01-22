using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SuperKanban.Commands;
using SuperKanban.Model.Entities;
namespace SuperKanban.ViewModel
{
    //public sealed class BoardCategory
    //{
    //    public BoardCategory(string name, params Board[] boards)
    //    {
    //        Name = name;
    //        Boards = new ObservableCollection<Board>(boards);
    //    }

    //    public string Name { get; }

    //    public ObservableCollection<Board> Boards { get; }
    //}
    public class BoardTreeViewModel
    {

        private object? _selectedItem;

        public ICommand AddBoardCommand { get; }
        public ICommand RemoveBoardCommand { get; }
        public ObservableCollection<Board> Boards { get; set; } = new ObservableCollection<Board>();

        public BoardTreeViewModel()
        {
            AddBoardCommand = new RelayCommand(AddBoard, o => true);
            RemoveBoardCommand = new RelayCommand(RemoveBoard, o =>true);
            Boards = new ObservableCollection<Board>(App.UnitOfWork.Boards.Get());

        }

        private void AddBoard(object parameter)
        {
            Board b1 = new Board() { Name ="dwqdq",Category= parameter  as string};
            Boards.Add(b1);
            App.UnitOfWork.Boards.Insert(b1);
        }


        private void RemoveBoard(object parameter)
        {
            Board b1 = parameter as Board;
            Boards.Remove(b1);
            App.UnitOfWork.Boards.Delete(b1.Id);


        }

        //public ObservableCollection<BoardCategory> BoardCategories { get; } = new ObservableCollection<BoardCategory>();
        public object SelectedItem { get => _selectedItem; set => _selectedItem = value; }
    }
}
