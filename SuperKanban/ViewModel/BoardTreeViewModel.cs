using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HandyControl.Tools.Extension;
using SuperKanban.Commands;
using SuperKanban.Model.Entities;
using SuperKanban.View.Basic;

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
            Boards[0].Cards.Add(new Card());
            GlobalFinder.Boards = Boards;
            ;

        }

        private void AddBoard(object parameter)
        {
            Board b1 = new Board() { Name ="未命名看板",Category= parameter  as string};
            b1.BoardColumns.Add(new BoardColumn("Todo", "212131"));
            Boards.Add(b1);
            App.UnitOfWork.Boards.Insert(b1);
        }


        private void RemoveBoard(object parameter)
        {
            Task<bool> task1 = RemoveBoardDialog(parameter);
     
    


        }
        private async Task<bool> RemoveBoardDialog(object parameter)
        {

            Board b1 = parameter as Board;
            foreach (var card in b1.Cards)
            {
                if (card.SLock.Active)
                {
                    HandyControl.Controls.Growl.Warning("无法删除看板，请解除被锁定的卡片后尝试");
                    return false;
                }
            }
            var DialogResult = true;
            DialogResult = await HandyControl.Controls.Dialog.Show<InteractiveDialog>()
    .Initialize<InteractiveDialogViewModel>(vm => vm.Message = "您确定要删除此卡片吗？")
    .GetResultAsync<bool>();
            if (DialogResult)
            {
                Boards.Remove(b1);
                App.UnitOfWork.Boards.Delete(b1.Id);

            }

            return DialogResult;
        }

        //public ObservableCollection<BoardCategory> BoardCategories { get; } = new ObservableCollection<BoardCategory>();
        public object SelectedItem { get => _selectedItem; set => _selectedItem = value; }
    }
}
