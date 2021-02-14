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

namespace SuperKanban.View
{
    /// <summary>
    /// CardView.xaml 的交互逻辑
    /// </summary>
    /// 

    public partial class CardView : UserControl
    {
        public CardView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }



        private void Flipper_IsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
        {

            //Height = Flipper.IsFlipped? BackContent.Height : FrontContent.Height;
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        public double CardHeight
        {
            get
            {
                return cardTextBlock.ActualHeight + cardItemsControl.ActualHeight + cardTextBlock2.ActualHeight + 220;
            }
        }
    }
}
