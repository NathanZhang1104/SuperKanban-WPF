using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SuperKanban.Model.Entities;
using SuperKanban.ViewModel;
using Syncfusion.UI.Xaml.Kanban;
using SuperKanban.View;
using GalaSoft.MvvmLight.Messaging;
using SuperKanban.View.Main;

namespace SuperKanban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            //((listBox1.DataContext) as BoardTreeViewModel).Boards = 
            ICollectionView view = CollectionViewSource.GetDefaultView(listbox1.Items);
            view.GroupDescriptions.Clear();
            view.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
            var btvm = new BoardTreeViewModel();
            listbox1.DataContext = btvm;
            foreach (var item in btvm.Boards)
            {
                if (item.Id == Properties.Settings.Default.SelectBoardId)
                {
                    BoardViewModel curbdvm = new BoardViewModel() { Board = item };
                    mainboard.DataContext = curbdvm;
                    GlobalFinder.CurBoard = curbdvm.Board;
                    GlobalFinder.CurColumns = mainboard.sfKanban.Columns;
                    break;
                }
            }
        }
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            //DataContext = ViewModelLocator.Instance.Main;
            NonClientAreaContent = new NonClientAreaContent();
        }

        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            saveboard();
            //BoardViewModel boardViewModel = mainboard.DataContext as BoardViewModel;

            //boardViewModel.Board = listbox1.SelectedItem as Board;
            //canvas.RegisterName("newButton", btn);//注册名字，以便以后使用  
            Board curboard = listbox1.SelectedItem as Board;
            BoardViewModel curbdvm = new BoardViewModel() { Board = curboard };
            //mainboard = new BoardView();
            mainboard.DataContext = curbdvm;
            GlobalFinder.CurBoard = curboard;
            GlobalFinder.CurColumns = mainboard.sfKanban.Columns;
            Properties.Settings.Default.SelectBoardId = curboard.Id;
            Properties.Settings.Default.Save();

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            saveboard();
        }

        private void saveboard()
        {
            BoardViewModel boardViewModel = mainboard.DataContext as BoardViewModel;

            
            if (boardViewModel.Board != null)
            {
                //boardViewModel.Board.Cards
                boardViewModel.Board.BoardColumns.Clear();
                for (int i = 0; i < mainboard.sfKanban.Columns.Count; i++)
                {
                    boardViewModel.Board.BoardColumns.Add(new BoardColumn( mainboard.sfKanban.Columns[i], boardViewModel.Board));
                    for (int j = 0; j < mainboard.sfKanban.Columns[i].Cards.Count; j++)
                    {
                        (mainboard.sfKanban.Columns[i].Cards[j].Content as Card).Index = j;
                    }
                }
                App.UnitOfWork.Boards.Update(boardViewModel.Board);
            }
        }

    }
}
