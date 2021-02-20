using System;
using System.Collections.Generic;
using System.Text;
using SuperKanban.Model.Entities;
using SuperKanban.Interop;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Diagnostics;
namespace SuperKanban.Model.Control
{
    class Controler
    {
        private readonly List<Board> ruleboards; //所有的看板，与全局保持一致。
        private readonly BoardColumn curcolumn;
        
        public Controler()
        {
            ;
        }
        private delegate void aduite_dg(UserApp userApp);
        private void AuditCurColApp(UserApp userApp)
        {
            foreach (var column in GlobalFinder.CurColumns)
            {
                for (int i = 0; i < column.Cards.Count; i++)
                {
                    var card = column.Cards[i].Content as Card;

                    if (card.AppRule?.Active != true) continue;
                    var res = card.AppRule.check(userApp);
                    if ((res & Conclusion.BlockApp) == Conclusion.BlockApp)
                    {
                        Debug.WriteLine("关闭进程");
                        userApp.KillSelf();

                        //userApp;
                    }
                    else if ((res & Conclusion.BlockScreen) == Conclusion.BlockScreen)
                    {
                        if (DateTime.Now.Second % 10 == 0)
                        {
                            ;
                        }
                    }

                }
            }
        }
        public void AuditApp(UserApp userApp)
        {
            App.Current.Dispatcher.Invoke(new aduite_dg(AuditCurColApp),userApp);
            var a = GlobalFinder.Boards;
            if (GlobalFinder.Boards == null) { return; }
            foreach (var board in GlobalFinder.Boards){
                if (board == GlobalFinder.CurBoard) continue;
                foreach (var card in board.Cards)
                {
                    if (card.AppRule?.Active != true) continue;
                    var res = card.AppRule.check(userApp);
                    if ((res & Conclusion.BlockApp) == Conclusion.BlockApp)
                    {
                        Debug.WriteLine("关闭进程");
                        userApp.KillSelf();

                        //userApp;
                    }
                    else if ((res & Conclusion.BlockScreen) == Conclusion.BlockScreen)
                    {
                        if (DateTime.Now.Second % 10 == 0)
                        {
                            ;
                        }
                    }

                }
            }
        }

        public void AuditScreen()
        {
            ;
        }


        
    }
}
