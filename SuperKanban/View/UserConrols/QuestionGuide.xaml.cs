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

namespace SuperKanban.View.UserConrols
{
    /// <summary>
    /// QuestionGuide.xaml 的交互逻辑
    /// </summary>
    public partial class QuestionGuide 
    {
        public QuestionGuide( )
        {
            InitializeComponent();
            Url = "https://www.wolai.com/wolai/rvKDNiaGBEqMvoydV8yuS6";
        }

        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public static readonly DependencyProperty UrlProperty =
                    DependencyProperty.Register("Url", typeof(string), typeof(QuestionGuide), new PropertyMetadata(null));



      
    }
}
