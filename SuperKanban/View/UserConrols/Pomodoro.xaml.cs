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
        private DispatcherTimer timer;
        private TimeSpan currentTimeSpan;
        private TimeSpan pomodoro=new TimeSpan(0,25,0);
        private TimeSpan shortBreak = new TimeSpan(0, 5, 0);
        private TimeType timeType;
        public TimeType TimeType
        {
            get { return timeType; }
            set
            {
                timeType = value;
                if (timeType == TimeType.Pomodoro)
                {
                    ;
                }
                else if (timeType == TimeType.ShortBreak)
                {
                    ;
                }
            }
        }
        public TimeSpan Time
        {
            get
            {
                switch (TimeType)
                {
                    case TimeType.Pomodoro: return pomodoro;
                    case TimeType.ShortBreak: return shortBreak;
                    //case TimeType.LongBreak: return longBreak;
                    default: return pomodoro;
                }
            }
        }

        public Pomodoro()
        {
            InitializeComponent();
            //Init Timer
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
        }
        private void StartTimer()
        {
            timer.Start();
            btnStartStop.Content = new PackIcon() { Kind = PackIconKind.Stop };
            btnStartStop.Background = this.FindResource("Background.stop") as SolidColorBrush;

        }

        private void StopTimer()
        {
            timer.Stop();
            btnStartStop.Content = new PackIcon() { Kind = PackIconKind.Play };
            ResetTimer();
            btnStartStop.Background = this.FindResource("Background.play") as SolidColorBrush;
        }
        private void ResetTimer()
        {
            currentTimeSpan = Time;
            lblTime.Text = currentTimeSpan.ToString(@"mm\:ss");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            currentTimeSpan = currentTimeSpan.Subtract(timer.Interval);

            if (currentTimeSpan > TimeSpan.Zero)
            {
                lblTime.Text = currentTimeSpan.ToString(@"mm\:ss");
            }
            else

            {
                StopTimer();

                if (TimeType == TimeType.Pomodoro)
                {
                    TimeType=TimeType.ShortBreak;
                }
                else
                {
                    TimeType = TimeType.Pomodoro;
                }
            }
        }

    }
}
