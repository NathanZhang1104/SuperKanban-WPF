using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace SuperKanban.Model.Entities
{
    //public enum PomodoroLinkType
    //{
    //    CurColumn = 0,
    //    CurCard = 1,
    //    None = 2
    //}
    public class Pomodoro:ViewModelBase
    {
        private int workmin = 25;
        private int breakmin = 5;
        private bool work=true;
        private bool isrunning;
        private DispatcherTimer timer;
        private TimeSpan remainTimeSpan;
        public ICommand PlayCmd { get; }
        public ICommand StopCmd { get; }

        private void Stop()
        {
            Isrunning = false;


        }

        private void Play()
        {
            Isrunning = true;


        }

        public Pomodoro()
        {
            RemainTimeSpan = new TimeSpan(0, work ? workmin : breakmin, 0);
            timer = new DispatcherTimer();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(1);
            PlayCmd = new RelayCommand(Play, () => !Isrunning);
            StopCmd = new RelayCommand(Stop, () => Isrunning);

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            RemainTimeSpan = RemainTimeSpan.Subtract(timer.Interval);
            if (RemainTimeSpan.TotalSeconds <= 0)
            {
                Isrunning = false;
                timer.Stop();
                Work =! work;
                RemainTimeSpan = new TimeSpan(0, work ? workmin : breakmin, 0);
            }
            else
            {
                ;
            }
            RaisePropertyChanged(nameof(RemainTimeText));

        }

        public int RecurTime { get; set; } = 1;
        //public PomodoroLinkType PomodoroLinkType { get; set; } = PomodoroLinkType.None;

        public bool Isrunning { get => isrunning; set {
                isrunning = value; RaisePropertyChanged();
                if (isrunning)
                {
                    timer.Start();

                }
                else
                {
                    timer.Stop();
                    Work = true;

               
                }

            } }
        public bool Work
        {
            get => work; set
            {

                if (!isrunning)
                {
                    work = value;
                    RaisePropertyChanged();
                    RemainTimeSpan = new TimeSpan(0, work ? workmin : breakmin, 0);
                    RaisePropertyChanged(nameof(RemainTimeText));
                }
 
           
   
            }
        }
        public String RemainTimeText
        {
            get => RemainTimeSpan.ToString(@"mm\:ss");
        }
        public TimeSpan RemainTimeSpan { get => remainTimeSpan; set => remainTimeSpan = value; }
    }
}
