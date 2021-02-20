using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
namespace SuperKanban.Model.Entities
{
    ///<summary>
    ///生成随机字符串 //转载请注明来自 http://www.uzhanbao.com
    ///</summary>
    ///<param name="length">目标字符串的长度</param>
    ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
    ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
    ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
    ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
    ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
    ///<returns>指定长度的随机字符串</returns>

    public class Card : ViewModelBase
    {
        
        private string title;
        private string description;
        private bool editable = true;
        public string Id { get; set; }
        public int BoardId { get; set; }

        public Card()
        { 

            Id=Interop.NativeMethods.GetRnd(8,custom:"#");
            SLock = new SLock(Id);
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged();
                ;
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description=value;
                RaisePropertyChanged();
            }
        }
        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
        public ObservableCollection<SubTask> SubTasks
        {
            get;set;
        }
        private bool isSelected;

        public AppRule AppRule { get; set; } = new AppRule();
        public bool IsSelected
        {
            get => isSelected; set
            {
                isSelected = value;
                RaisePropertyChanged();
            }
        }
        public SLock SLock { get; set; }
        public Pomodoro Pomodoro { get; set; }
        public bool Editable { get { return editable; }
            set { editable = value;
                AppRule.Editable = value; }
        } 

    }
}