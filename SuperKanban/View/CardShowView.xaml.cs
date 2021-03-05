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
namespace SuperKanban.View
{
    /// <summary>
    /// CardShowView.xaml 的交互逻辑
    /// </summary>
    public partial class CardShowView : UserControl
    {

        public Dictionary<LockType, string> mydic = new Dictionary<LockType, string>()

            {

                {LockType.CardLock,"当前卡片"},

                //{LockType.ColumnLock,"时间胶囊"},
                    {LockType.ColumnLock,"当前卡片列"}

            };

        public bool ShowMe
        {
            get { return (bool)GetValue(ShowMeProperty); }
            set { SetValue(ShowMeProperty, value); }
        }

        public static readonly DependencyProperty ShowMeProperty =
                    DependencyProperty.Register("ShowMe", typeof(bool), typeof(CardShowView), new PropertyMetadata(null));


        public ICommand DeleteCommand
        {
            get
            {
                return (ICommand)GetValue(DeleteCommandProperty);
            }
            set
            {
                SetValue(DeleteCommandProperty, value);
            }
        }
        public static readonly DependencyProperty DeleteCommandProperty =
                        DependencyProperty.Register(
                                "DeleteCommand",
                                typeof(ICommand),
                                typeof(CardShowView),
                                new FrameworkPropertyMetadata((ICommand)null,
                                   null));


        public CardShowView()
        {
            InitializeComponent();
            combobox.ItemsSource = mydic;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as CardShowViewModel).Card.RaisePropertyChanged("SubTasks");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ShowMe = false;

        }

        private void Button_Click_delete(object sender, RoutedEventArgs e)
        {
            ShowMe = false;
            DeleteCommand.Execute(null);
        }

        private void userControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
