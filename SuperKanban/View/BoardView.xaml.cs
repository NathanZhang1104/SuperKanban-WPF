﻿using System;
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
using SuperKanban.View.Templates;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Threading;
using Serilog;
using System.ComponentModel;

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

            viewModel.SelectedCard = (Card)e.SelectedCard.Content;
            ClearTaskTagEntry();
            CardShowView.ShowMe = true;
            if (string.IsNullOrWhiteSpace(CardShowView.title_text.Text))
            {
                CardShowView.title_text.Focus();
            }
            CardShowView.scrollviewer.ScrollToVerticalOffset(0);

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
        public static T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            if (obj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }

        [DllImport("user32.dll")]
        static extern void GetCursorPos(ref PInPoint p);
        private bool carddrag = false;
        private void sfKanban_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

            if (carddrag) return;
            var items = (SfKanban)sender;
            ScrollViewer scroll = FindVisualChild<ScrollViewer>(items);
            if (scroll != null)
            {
                PInPoint pointRef = new PInPoint();
                GetCursorPos(ref pointRef);
                Point cur_drag_point = new Point(pointRef.X, pointRef.Y);
                Point ab_cur_drag_point = items.PointFromScreen(cur_drag_point);
                if (items.ActualWidth - ab_cur_drag_point.X < 200)
                {
                    scroll.LineRight();
                }
                else if (ab_cur_drag_point.X < 200)
                {
                    scroll.LineLeft();

                }
                if (items.ActualWidth - ab_cur_drag_point.X < 100)
                {
                    scroll.LineRight();
                }
                else if (ab_cur_drag_point.X < 100)
                {
                    scroll.LineLeft();

                }


            }
        }

        private void sfKanban_CardDragStart(object sender, KanbanDragStartEventArgs e)
        {
            carddrag = true;
        }

        private void sfKanban_CardDragOver(object sender, KanbanDragOverEventArgs e)
        {
            carddrag = false;

        }

        private void sfKanban_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CardShowView.ShowMe = false;
            var boardViewModel = DataContext as BoardViewModel;
            boardViewModel.RaisePropertyChanged("Board");
            boardViewModel.BoardWindow = this;
            sfKanban.Columns.Clear();
            //TODO:置空
            if (boardViewModel.Board == null) return;
            foreach (var colitem in boardViewModel.Board.BoardColumns)
            {
                var cur_col = new KanbanColumn() { Title = colitem.Title, Categories = colitem.Category };
                sfKanban.Columns.Add(cur_col);
            }
            var collectionView = CollectionViewSource.GetDefaultView(sfKanban.ItemsSource);
            collectionView.SortDescriptions.Clear();
            collectionView.SortDescriptions.Add(new SortDescription(nameof(Card.Index), ListSortDirection.Ascending));
            collectionView.Refresh();


        }


        private void sfKanban_CardDragLeave(object sender, KanbanDragLeaveEventArgs e)
        {
          ;
        }
    }
    }
    