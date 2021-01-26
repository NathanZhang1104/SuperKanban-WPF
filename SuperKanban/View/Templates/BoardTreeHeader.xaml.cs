using System;
using System.Collections.Generic;
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

namespace SuperKanban.View.Templates
{
    /// <summary>
    /// BoardTreeHeader.xaml 的交互逻辑
    /// </summary>
    public partial class BoardTreeHeader : UserControl
    {
        public BoardTreeHeader()
        {
            InitializeComponent();
        }
        private void titleedit_LostFocus(object sender, RoutedEventArgs e)
        {
            titlelabel.Visibility = Visibility.Visible; ;

            titleedit.Visibility = Visibility.Collapsed;

            if (string.IsNullOrWhiteSpace(titleedit.Text))
            {

            }
            //timer2.Stop();

        }

        private void titleedit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                titlelabel.Visibility = Visibility.Visible; ;

                titleedit.Visibility = Visibility.Collapsed;
                e.Handled = true;

                if (string.IsNullOrWhiteSpace((string)(titleedit.Text)))
                {


                }

                //timer2.Stop();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            titlelabel.Visibility = Visibility.Collapsed;

            titleedit.Visibility = Visibility.Visible;
            titleedit.Focus();
        }
    }


}
