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
using Serilog;
using SuperKanban.Properties;
namespace SuperKanban
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public static partial class GlobalFinder
    {
        public static Board CurBoard { get; set;}
        private static ObservableCollection<Board> boards { get; set; }

        public static KanbanColumnsCollection CurColumns { get; set; }
        public static ObservableCollection<Board> Boards
        {
            get => boards; set
            {
                boards = value;
                if (Loaded!=null)
                {
                    Loaded();
                }
            }
        }
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

        public static Action Loaded;
    }   

    public partial class App : Application
    {
        public static IUnitOfWork UnitOfWork { get; } = new UnitOfWork();
        LierdaCracker cracker = new LierdaCracker();
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug() // 所有Sink的最小记录级别
                 .WriteTo.Console()    //输出到控制台
                 .WriteTo.File("00_Logs\\log.log", //$"{AppContext.BaseDirectory}00_Logs\log.log"
                               rollingInterval: RollingInterval.Day,
                                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                               //,retainedFileCountLimit: 31
                               )
                 .CreateLogger();
            Log.Information("程序开始！");

            var t1 = new Task(() => MonitorMethod());
            t1.Start();
            cracker.Cracker();
            base.OnStartup(e);

            var _paletteHelper = new MaterialDesignThemes.Wpf.PaletteHelper();
            if (SuperKanban.Properties.Settings.Default.Theme == null)
            {
                ;
            }
            else
            {
                _paletteHelper.SetTheme(SuperKanban.Properties.Settings.Default.Theme);
            }
          
        }

        protected override void OnExit(ExitEventArgs e)
        {
           SuperKanban.Properties.Settings.Default.Save();
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
