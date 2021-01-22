using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using System.Windows.Threading;
using Syncfusion.UI.Xaml.Kanban;

namespace SuperKanban.View.Templates
{

    /// <summary>
    /// ColumHeder.xaml 的交互逻辑
    /// </summary>
    /// 
    public struct PInPoint
    {
        public int X;
        public int Y;
        public PInPoint(int x, int y)
        {
            X = x; Y = y;
        }
        public PInPoint(double x, double y)
        {
            X = (int)x; Y = (int)y;
        }
        public Point GetPoint(double xOffset = 0, double yOffet = 0)
        {
            return new Point(X + xOffset, Y + yOffet);
        }
        public Point GetPoint(Point offset)
        {
            return new Point(X + offset.X, Y + offset.Y);
        }
    }
    public partial class ColumHeader : UserControl
    {
        private static Point cur_drag_point;
        public bool left = false;
        

        private DispatcherTimer timer = new DispatcherTimer();
        private DispatcherTimer timer2 = new DispatcherTimer();
        private PInPoint pointRef = new PInPoint();

        private bool indrag = false;
        public static readonly RoutedEvent ColumDropInEnvent = EventManager.RegisterRoutedEvent("ColumDropInEnvent", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventArgs<Object>), typeof(ColumHeader));
        /// <summary>
        /// 处理各种路由事件的方法 
        /// </summary>
        /// 
        public void timeCycle(object sender, EventArgs e)
        {
            GetCursorPos(ref pointRef);
            cur_drag_point = new Point(pointRef.X, pointRef.Y);
            Point ab_cur_drag_point = userControl.PointFromScreen(cur_drag_point);
            if (ab_cur_drag_point.X < userControl.ActualWidth / 2)
            {
                left = true;
                right_l.Visibility = Visibility.Visible;
                right_r.Visibility = Visibility.Hidden;


            }
            else
            {
                left = false;
                right_r.Visibility = Visibility.Visible;
                right_l.Visibility = Visibility.Hidden;


            }
            ;
        }
        /// 
        public void timeCycle2(object sender, EventArgs e)
        {
                    titleedit.Focus();

        }
        public event RoutedEventHandler ColumDropIn
        {
            //将路由事件添加路由事件处理程序
            add { AddHandler(ColumDropInEnvent, value); }
            //从路由事件处理程序中移除路由事件
            remove { RemoveHandler(ColumDropInEnvent, value); }
        }

        public KanbanColumn KanbanColumn
        {
            get { return (KanbanColumn)GetValue(KanbanColumnProperty); }
            set { SetValue(KanbanColumnProperty, value); }
        }

        public static readonly DependencyProperty KanbanColumnProperty =
                    DependencyProperty.Register("KanbanColumn", typeof(KanbanColumn), typeof(ColumHeader), new PropertyMetadata(null));

        public KanbanColumn Dropin_KanbanColumn
        {
            get; set;
        }

        public ColumHeader()
        {
        
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += new EventHandler(timeCycle);  //你的事件
            timer2.Interval = TimeSpan.FromMilliseconds(100);
            timer2.Tick += new EventHandler(timeCycle2);  //你的事件
            InitializeComponent();
 
         

        }
        private void ColumnHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var h1 = sender as ColumHeader;
            var obj = new DataObject("Column", h1.KanbanColumn);
            DragDrop.DoDragDrop(h1, obj, DragDropEffects.Copy);
        }

        private void ColumnHeader_Drop(object sender, DragEventArgs e)
        {
            timer.Stop();

            Point p = Mouse.GetPosition(e.Source as FrameworkElement);//WPF方法

            right_l.Visibility = Visibility.Hidden;
            right_r.Visibility = Visibility.Hidden;
            // RoutedPropertyChangedEventArgs<Object> args = new RoutedPropertyChangedEventArgs<Object>("1", "2", ControlLoadOverEvent);
            Dropin_KanbanColumn = e.Data.GetData("Column") as KanbanColumn;
            RoutedEventArgs args2 = new RoutedEventArgs(ColumDropInEnvent, this);
            //引用自定义路由事件
            this.RaiseEvent(args2);
        }
        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            timer.Start();

            indrag = true;


            //borderRect.StrokeDashArray = new DoubleCollection() { 4, 2 };
            //borderRect.Stroke = Brushes.LightBlue;
        }

        private void UserControl_DragLeave(object sender, DragEventArgs e)
        {

            indrag = false;
            GetCursorPos(ref pointRef);
            cur_drag_point = new Point(pointRef.X, pointRef.Y);
            Point ab_cur_drag_point = userControl.PointFromScreen(cur_drag_point);
            if (ab_cur_drag_point.X < -10 || ab_cur_drag_point.X > userControl.ActualWidth ||
                ab_cur_drag_point.Y < -10 || ab_cur_drag_point.Y > userControl.ActualHeight)
            {
                right_l.Visibility = Visibility.Hidden;
                right_r.Visibility = Visibility.Hidden;
            }

            timer.Stop();
            //borderRect.StrokeDashArray = null;
            //borderRect.Stroke = Brushes.Black;
        }

        [DllImport("user32.dll")]
        static extern void GetCursorPos(ref PInPoint p);


        private void UserControl_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {


            GetCursorPos(ref pointRef);
            cur_drag_point = new Point(pointRef.X, pointRef.Y);
            ;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KanbanColumn.IsExpanded = false;

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            titlelabel.Visibility = Visibility.Collapsed; ;

            titleedit.Visibility = Visibility.Visible;

            titleedit.Focus();
        }

        private void titleedit_LostFocus(object sender, RoutedEventArgs e)
        {
            titlelabel.Visibility = Visibility.Visible; ;

            titleedit.Visibility = Visibility.Collapsed;
            var tag = DataContext as ColumnTag;

            if (string.IsNullOrWhiteSpace(titleedit.Text))
            {
                tag.Header = "untitled";
            KanbanColumn.Title = "untitled";

            }
            KanbanColumn.Title = titleedit.Text;
            timer2.Stop();

        }

        private void titleedit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                titlelabel.Visibility = Visibility.Visible; ;

                titleedit.Visibility = Visibility.Collapsed;
                e.Handled = true;
                var tag = DataContext as ColumnTag;

                if (string.IsNullOrWhiteSpace((string)(titleedit.Text)))
                {
                    tag.Header = "untitled";
                    KanbanColumn.Title = "untitled";

                }
                KanbanColumn.Title = titleedit.Text;

                timer2.Stop();
            }

        }

        private void userControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var tag = DataContext as ColumnTag;
            if (tag!=null)
            {
                if (string.IsNullOrWhiteSpace((string)(tag.Header)))
                {
                    titleedit.Focusable = true;
                    titlelabel.Visibility = Visibility.Collapsed; ;

                    titleedit.Visibility = Visibility.Visible;

                    titleedit.Focus();
                    timer2.Start();

                }
            }
        }
    }
}
