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

namespace SuperKanban
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //((listBox1.DataContext) as BoardTreeViewModel).Boards = 
            ICollectionView view = CollectionViewSource.GetDefaultView(listbox1.Items);
            view.GroupDescriptions.Clear();
            view.GroupDescriptions.Add(new PropertyGroupDescription("Category"));
        }

        private void listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BoardViewModel boardViewModel = mainboard.DataContext as BoardViewModel;
            if (boardViewModel.Board != null)
            {
                boardViewModel.Board.BoardColumns.Clear();
                foreach (var item in mainboard.sfKanban.Columns)
                {
                    boardViewModel.Board.BoardColumns.Add(new BoardColumn(item));
                }
                App.UnitOfWork.Boards.Update(boardViewModel.Board);

            }
            boardViewModel.Board = listbox1.SelectedItem as Board;
            mainboard.sfKanban.Columns.Clear();
            foreach (var item in boardViewModel.Board.BoardColumns)
            {
                mainboard.sfKanban.Columns.Add(new KanbanColumn() { Title = item.Title, Categories = item.Category });
            }
        }

        private void listbox1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //BoardViewModel boardViewModel = mainboard.DataContext as BoardViewModel;
            //boardViewModel.Board = listbox1.SelectedItem as Board;


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            BoardViewModel boardViewModel = mainboard.DataContext as BoardViewModel;

            App.UnitOfWork.Boards.Update(boardViewModel.Board);
            foreach (var item in mainboard.sfKanban.Columns)
            {
                boardViewModel.Board.BoardColumns.Clear();
                boardViewModel.Board.BoardColumns.Add(new BoardColumn(item));
            }
        }

    }
}
