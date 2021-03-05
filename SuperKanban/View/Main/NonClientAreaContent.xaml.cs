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


namespace SuperKanban.View.Main
{
    /// <summary>
    /// NonClientAreaContent.xaml 的交互逻辑
    /// </summary>
    public partial class NonClientAreaContent 
    {
        public NonClientAreaContent()
        {
            InitializeComponent();
        }

        private void ButtonConfig_Click(object sender, RoutedEventArgs e)
        {
            var window = new SuperKanban.View.Main.Settings();
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            window.ShowDialog();
        }
    }
}
