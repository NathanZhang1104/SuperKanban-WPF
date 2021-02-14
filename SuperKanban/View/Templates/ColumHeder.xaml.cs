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
    /// ColumHeder.xaml 的交互逻辑
    /// </summary>
    public partial class ColumHeder : UserControl
    {
        public ColumHeder()
        {
            InitializeComponent();
        }
        private void ColumnHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var h1 = sender as Border;
            DragDrop.DoDragDrop(h1, h1.DataContext, DragDropEffects.Copy);
        }

        private void ColumnHeader_Drop(object sender, DragEventArgs e)
        {
            ;
        }
    }
}
