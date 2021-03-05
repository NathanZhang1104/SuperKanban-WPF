using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SuperKanban.Properties;
namespace SuperKanban.View.Main
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings
    {
        public Settings()
        {
            InitializeComponent();
        }


        private void AutoRunTgBtn_Click(object sender, RoutedEventArgs e)
        {

            Interop.AutoRun.defalut.SetMeAutoStart(Properties.Settings.Default.AutoRun);
            //Interop.AutoRunByRegistry.SetMeStart(Properties.Settings.Default.AutoRun);
        }
    }
}
