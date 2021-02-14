using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SuperKanban.ViewModel;
using SuperKanban.Model.Entities;
using Syncfusion.UI.Xaml.Kanban;
namespace SuperKanban.View
{
    /// <summary>
    /// BoardView.xaml 的交互逻辑
    /// </summary>
    public partial class BoardView : UserControl
    {
        public BoardView()
        {
            InitializeComponent();
            (DataContext as BoardViewModel).BoardWindow = this;
        }
        private void SfKanban_OnCardTapped(object sender, KanbanTappedEventArgs e)
        {

            var viewModel = (BoardViewModel)DataContext;

            viewModel.SelectedCard= (Card)e.SelectedCard.Content;
            ClearTaskTagEntry();
            CardShowView.ShowMe=true;
            if (string.IsNullOrWhiteSpace(CardShowView.title_text.Text))
            {
                CardShowView.title_text.Focus();
            }
        }

        public void ClearSubTaskEntry()
        {
            //SubTaskIsCompletedCheckBox.IsChecked = false;
            //SubTaskTitleTextBox.Text = "";
        }

        public void ClearTaskTagEntry()
        {
            //TaskTagTextBox.Text = "";
        }

        private void BoardWindow_OnClosed(object sender, EventArgs e)
        {
            var viewModel = (BoardViewModel)DataContext;
            viewModel.SaveBoard(null);
        }

        private void AboutMenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //var window = new AboutWindow();
            //window.ShowDialog();
        }

        private void SfKanban_CardTapped(object sender, KanbanTappedEventArgs e)
        {

        }

    
    }
}
