using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SuperKanban.Database;
using SuperKanban.Model.Entities;
using SuperKanban.Model.Control;
using Syncfusion.UI.Xaml.Kanban;
using Lierda.WPFHelper;
using System.Collections.ObjectModel;

namespace SuperKanban
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public static partial class GlobalFinder
    {
        public static Board CurBoard { get; set;}
        public static KanbanColumnsCollection CurColumns { get; set; }
        public static ObservableCollection<Board> Boards { get; set; }
        public static Card FindCard(string card_id)
        {
            Card ret=CurBoard?.Cards.ToList<Card>().Find(c => c.Id == card_id);
            return ret;
        }
        public static KanbanColumn FindColumn(string categories)
        {
            KanbanColumn ret = CurColumns?.ToList<KanbanColumn>().Find(c => c.Categories  == categories);
            return ret;
        }
    }   

    public partial class App : Application
    {
        public static IUnitOfWork UnitOfWork { get; } = new UnitOfWork();
        LierdaCracker cracker = new LierdaCracker();
        protected override void OnStartup(StartupEventArgs e)
        {
            var t1 = new Task(() => MonitorMethod());
            t1.Start();
            cracker.Cracker();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            UnitOfWork.Dispose();

            base.OnExit(e);
        }

        public static void MonitorMethod()
        {
            Monitor monitor =new Monitor();
            Controler controler = new Controler();
            monitor.AduitAction += controler.AuditApp;
            monitor.Execute();
        }
    }


}
