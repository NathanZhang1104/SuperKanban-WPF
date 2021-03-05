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
using SuperKanban.Model.Entities;
namespace SuperKanban.View
{
    /// <summary>
    /// AccrTimeSelectorView.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class AccrTimeSelectorView : UserControl
    {
        public Dictionary<LimitType, string> LimitTypedic = new Dictionary<LimitType, string>()

            {
                    {LimitType.Block,"  阻止"},
                     
                {LimitType.Limit,"  限制"},


            };
        public SolidColorBrush CurrentBrush
        {
            get { return (SolidColorBrush)GetValue(CurrentBrushProperty); }
            set { SetValue(CurrentBrushProperty, value); }
        }

        public static readonly DependencyProperty CurrentBrushProperty =
                    DependencyProperty.Register("CurrentBrush", typeof(SolidColorBrush), typeof(AccrTimeSelectorView), new PropertyMetadata(null));
        public LimitType CurrentType
        {
            get { return (LimitType)GetValue(CurrentTypeProperty); }
            set { SetValue(CurrentTypeProperty, value); }
        }

        public static readonly DependencyProperty CurrentTypeProperty =
                    DependencyProperty.Register("CurrentType", typeof(LimitType), typeof(AccrTimeSelectorView), new PropertyMetadata(null));

        private Point last_down_point;
        public AccrTimeSelectorView()
        {
            
            CurrentType = LimitType.Block;
            InitializeComponent();
            limittype_combobox.ItemsSource = LimitTypedic;
        }
        public AccrTimeSelectorViewModel ATSViewMoel{
            get { return DataContext as AccrTimeSelectorViewModel; } }

        private void ItemsControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas el = (Canvas)sender;
     
            el.CaptureMouse();
            if (CurrentType == LimitType.Allow) return;
            Point p = Mouse.GetPosition(el);//WPF方法
            last_down_point = p;
            ATSViewMoel.CurrentTimeRuleOne = new TimeRuleOne();
            ATSViewMoel.CurrentTimeRuleOne.LimitType = CurrentType;
            ATSViewMoel.TimeLimit.TimeRuleOneList.Add(ATSViewMoel.CurrentTimeRuleOne);

        }
        private List<string> weekstringlist= new List<string> { "周一", "周二", "周三", "周四", "周五", "周六","周日" };
        
        private void ItemsControl_MouseMove(object sender, MouseEventArgs e)
        {
            Point p = Mouse.GetPosition(e.Source as FrameworkElement);//WPF方法
            if (p.X < 0) p.X = 0; if (p.Y < 0) p.Y = 0;
            if (p.X > 720) p.X = 720; if (p.Y >= 210) p.Y = 209.9;
            if (ATSViewMoel.CurrentTimeRuleOne != null)

            {
                ATSViewMoel.CurrentTimeRuleOne.Update(last_down_point, p, new Size(30, 30));
                ATSViewMoel.CurrentTimeRuleOne.RaisePropertyChanged("TimeRange");
                ATSViewMoel.CurrentTimeRuleOne.RaisePropertyChanged("DateRange");

            }
            var label_text = info_text as Label;
            label_text.Content = "2e2";
            int c_time = (int)p.X / 30;
            label_text.Content= weekstringlist[(int)p.Y / 30]+" "+c_time.ToString()+":00-"+ (c_time+1).ToString()+":00";
         
        }
        

        private void ItemsControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ATSViewMoel.CurrentTimeRuleOne != null)
            {
                if (ATSViewMoel.CurrentTimeRuleOne.TimeRange.RangeLength < 0.15 ||
               ATSViewMoel.CurrentTimeRuleOne.DateRange.RangeLength < 1)
                {
                    ATSViewMoel.TimeLimit.TimeRuleOneList.Remove(ATSViewMoel.CurrentTimeRuleOne);
                }
            }
            ATSViewMoel.CurrentTimeRuleOne = null;
            Canvas el = (Canvas)sender;
            el.ReleaseMouseCapture();

        }

        private void Chip_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as AccrTimeSelectorViewModel;
            switch ((sender as MaterialDesignThemes.Wpf.Chip).Name)
            {
                case "chip_allow":
                    vm.CurrentType = LimitType.Allow;
                    break;
                case "chip_block":
                    vm.CurrentType = LimitType.Block;
                    break;
                case "chip_limit":
                    vm.CurrentType = LimitType.Limit;
                    break;
                default:
                    break;
            }
            vm.RaisePropertyChanged("CurrentType");
            CurrentType = vm.CurrentType;

        }
    }
}
