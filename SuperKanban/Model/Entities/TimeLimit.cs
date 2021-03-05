using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using SuperKanban.Model.Control;
using GalaSoft.MvvmLight;
namespace SuperKanban.Model.Entities
{
    public enum LimitType

    {
        Allow,
        Limit,
        Block,
    }
    public class TimeRange
    {

        public double start { get; set; } = 0;
        public double end { get; set; } = 0;

        public TimeRange(double start, double end)
        {
            this.start = start;
            this.end = end;
        }
        
        public Double RangeLength { get { return this.end - this.start; } }


    }
    public class DateRange
    {

        public int from { get; set; } = 0;
        public int to { get; set; } = 0;

        public DateRange(int from, int to)
        {
            this.from = from;
            this.to = to;
        }

        public int RangeLength { get { return this.to - this.from + 1; } }
    }
    public class TimeRuleOne:ViewModelBase
    {
        public DateTime last_checktime { get; set; }
        private TimeRange timeRange = new TimeRange(0,0);
        private DateRange dateRange = new DateRange(0,0);
        private LimitType limitType;
        private int limitminiutes;
        private Double cumulative_time=0;

        public LimitType LimitType
        {
            get { return limitType; }
            set
            {
                if (value == LimitType.Limit)
                {
                    limitminiutes = 15;
                }
                limitType = value; RaisePropertyChanged("LimitType");
            }
        }
        private bool enable = true;


        public TimeRange TimeRange { get => timeRange; set => timeRange = value; }
        public DateRange DateRange { get => dateRange; set => dateRange = value; }
        public bool Enable
        {
            get => enable; set
            {
                enable = value;
                RaisePropertyChanged();
            }
        }

        public int Limitminiutes { get => limitminiutes; set { if (this.limitType == LimitType.Limit) { limitminiutes = value; } } }

        public double Cumulative_time { get => cumulative_time; set { cumulative_time = value;
                RaisePropertyChanged();
            }}

        public void Update(Point p1,Point p2,Size block_size)
        {
            p1.X= Math.Round(p1.X / (block_size.Width/6)) * (block_size.Width/6); 
            p2.X= Math.Round(p2.X / (block_size.Width / 6)) * (block_size.Width / 6);

            timeRange.start = Math.Min(p1.X, p2.X) / block_size.Width;
            timeRange.end= Math.Max(p1.X, p2.X)/ block_size.Width;
            dateRange.from = (int)(Math.Min(p1.Y, p2.Y) / block_size.Height);
             dateRange.to=(int)(Math.Max(p1.Y, p2.Y) / block_size.Height);
            RaisePropertyChanged("TimeRange");
            RaisePropertyChanged("DateRange");

        }
        
    }
    public class TimeLimit
    {

        public ObservableCollection<TimeRuleOne> TimeRuleOneList { get; set; } = new ObservableCollection<TimeRuleOne>();
        public int  check() {
            int ret = int.MaxValue;
            foreach (TimeRuleOne item in TimeRuleOneList)
            {
                if (DateTime.Now.IsIn(item))
                {
                    if (item.LimitType == LimitType.Block)
                    {
                        return 0;
                    }
                    else
                    {
                        ret = Math.Min(ret, item.Limitminiutes - (int)item.Cumulative_time);
                    }
                }

            }
            return ret;
        }
        internal void update()
        {
            foreach (TimeRuleOne item in TimeRuleOneList)
            {
                if(item.LimitType == LimitType.Limit){
                    if (DateTime.Now.IsIn(item))
                    {
                        if(item.last_checktime.Date != DateTime.Now.Date)
                        {
                            item.Cumulative_time = 0;

                        }
                        if (item.last_checktime.IsIn(item))

                        {
                            item.Cumulative_time += ((double)Monitor.timerInterval)/(60*1000);
                            if (item.Cumulative_time > item.Limitminiutes)
                            {
                                if (AppTimeOut != null)
                                {
                                    this.AppTimeOut();
                                }
                                if (ScreenTimeOut != null)
                                {
                                    this.ScreenTimeOut();
                                }
                            }
                        }
                        else
                        {
                            item.Cumulative_time = 0;
                        }

                        item.last_checktime = DateTime.Now;


                        //return item.LimitType;
                    }
                }else if (item.LimitType == LimitType.Block)
                {
                    if (DateTime.Now.IsIn(item))
                    {
                        if (AppTimeOut != null)
                        {
                            this.AppTimeOut();
                        }
                        if (ScreenTimeOut != null)
                        {
                            this.ScreenTimeOut();
                        }
                    }
                }
        

            };
        }
        public Action AppTimeOut;
        public Action ScreenTimeOut;

        public TimeLimit()
        {
        }
    }
}
