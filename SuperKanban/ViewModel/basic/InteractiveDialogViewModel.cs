using System;
using System.Collections.Generic;
using System.Text;
using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Tools.Extension;
namespace SuperKanban.ViewModel
{
    public class InteractiveDialogViewModel : ViewModelBase, IDialogResultable<bool>
    {
        public Action CloseAction { get; set; }

        private bool _result;


        private string _message;

        public string Message
        {
            get => _message;

            set => Set(ref _message, value);
        }

        public RelayCommand<bool> CloseCmd => new Lazy<RelayCommand<bool>>(() => new RelayCommand<bool>(close)).Value;

        bool IDialogResultable<bool>.Result { get => _result; set => _result = value; }

        private void close( bool parameter)
        {
            _result = parameter;
            CloseAction?.Invoke();//TODO,这样不用null判断
        }
    }
}
