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
            saveboard();
            //BoardViewModel boardViewModel = mainboard.DataContext as BoardViewModel;

            //boardViewModel.Board = listbox1.SelectedItem as Board;
            //canvas.RegisterName("newButton", btn);//注册名字，以便以后使用  
            Board curboard = listbox1.SelectedItem as Board;
            BoardViewModel curbdvm = new BoardViewModel() { Board = curboard };
            //mainboard = new BoardView();
            mainboard.DataContext = curbdvm;


        }

        private void listbox1_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            //BoardViewModel boardViewModel = mainboard.DataContext as BoardViewModel;
            //boardViewModel.Board = listbox1.SelectedItem as Board;


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
                boardViewModel.Board.BoardColumns.Clear();
                foreach (var item in mainboard.sfKanban.Columns)
                {
                    boardViewModel.Board.BoardColumns.Add(new BoardColumn(item, boardViewModel.Board));
                }
                ;
                App.UnitOfWork.Boards.Update(boardViewModel.Board);
                ;
            }
        }

    }
}
