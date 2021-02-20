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
using SuperKanban.Model.Entities;
namespace SuperKanban.View
{
    /// <summary>
    /// AppRuleView.xaml 的交互逻辑
    /// </summary>
    public partial class AppRuleView : UserControl
    {
        public AppRuleView()
        {
            InitializeComponent();
            RuleFuncIn.ItemsSource = RuleFuncDic;
        }
        private Dictionary<RuleFunc, string> RuleFuncDic = new Dictionary<RuleFunc, string>()
{
            {RuleFunc.Title,"窗口标题"},
            {RuleFunc.Url,"网址"},
            {RuleFunc.Path,"文件路径"},
            {RuleFunc.Info,"文件信息"},
            {RuleFunc.Muti,"多重规则"},
            {RuleFunc.Screen,"屏幕"},
            {RuleFunc.ImageDetect,"图像识别"},

        };

        private void TabItem_ToolTipOpening(object sender, ToolTipEventArgs e)
        {

        }
        public bool Editable
        {
            get { return (bool)GetValue(EditableProperty); }
            set { SetValue(EditableProperty, value); }
        }

        public static readonly DependencyProperty EditableProperty =
                    DependencyProperty.Register("Editable", typeof(bool), typeof(CardShowView), new PropertyMetadata(null));

        public void SetEditable(bool editable)
        {
            timeselector.IsEnabled = editable;
            addbtn.IsEnabled = editable;
            rulegrid.IsEnabled = editable;
        }
    }
}
