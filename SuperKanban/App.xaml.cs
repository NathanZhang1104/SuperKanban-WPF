using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SuperKanban.Database;
namespace SuperKanban
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        public static IUnitOfWork UnitOfWork { get; } = new UnitOfWork();

        protected override void OnExit(ExitEventArgs e)
        {
            UnitOfWork.Dispose();

            base.OnExit(e);
        }
    }
}
