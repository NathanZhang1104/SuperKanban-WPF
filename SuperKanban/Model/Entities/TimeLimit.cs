using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

using GalaSoft.MvvmLight;
namespace SuperKanban.Model.Entities
{
    public enum LimitType

    {
        Allow,
        Limit,
        Block,
    }
    public struct TimeRange
    {

        public double start;
        public double end;

        public TimeRange(double start, double end)
        {
            this.start = start;
            this.end = end;
        }
        public Double RangeLength { get { return this.end - this.start; } }


    }
    public struct DateRange
    {

        public int from;
        public int to;

        public DateRange(int from, int to)
        {
            this.from = from;
            this.to = to;
        }
        public int RangeLength { get { return this.to - this.from + 1; } }
    }
    public class TimeRuleOne:ViewModelBase
    {
        private TimeRange timeRange;
        private DateRange dateRange;
        private LimitType limitType;
        public string limitstring;
        public LimitType LimitType
        {
            get { return limitType; }
            set
            {
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
        public void Update(Point p1,Point p2,Size block_size)
        {
            p1.X= Math.Round(p1.X / (block_size.Width/6)) * (block_size.Width/6); 
            p2.X= Math.Round(p2.X / (block_size.Width / 6)) * (block_size.Width / 6);
         
            timeRange = new TimeRange(Math.Min(p1.X, p2.X)/ block_size.Width, Math.Max(p1.X, p2.X)/ block_size.Width);
            dateRange = new DateRange((int)(Math.Min(p1.Y, p2.Y)/block_size.Height), (int)(Math.Max(p1.Y, p2.Y) / block_size.Height));
            RaisePropertyChanged("TimeRange");
            RaisePropertyChanged("DateRange");

        }
    }
    public class TimeLimit
    {

        public ObservableCollection<TimeRuleOne> TimeRuleOneList { get; set; } = new ObservableCollection<TimeRuleOne>();
    }
}
