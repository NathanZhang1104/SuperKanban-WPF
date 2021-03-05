using MaterialDesignThemes.Wpf;
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
using System.Windows.Threading;

namespace SuperKanban.View.UserConrols
{
    public enum TimeType
    {
        Pomodoro,
        ShortBreak,
        LongBreak
    }
    /// <summary>
    /// Pomodoro.xaml 的交互逻辑
    /// </summary>
    public partial class Pomodoro : UserControl
    {

        public Pomodoro()
        {
            InitializeComponent();
            //Init Timer
      
        }
    

    }
}
