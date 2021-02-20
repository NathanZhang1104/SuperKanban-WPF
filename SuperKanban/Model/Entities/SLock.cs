using System;
using System.Collections.Generic;
using System.Text;

namespace SuperKanban.Model.Entities
{
    public delegate void SlockHandler(LockType type, bool islock);

    public enum LockType
    {
        ColumnLock = 0,
        CardLock = 1,
        PillLock = 2,
    }
    public enum PomodoroLinkType
    {
        CurColumn = 0,
        CurCard = 1,
        None=2
    }
    public class SLock
    {

        private LockType lockType;
        private bool allowPassword;
        private bool active;
        
        public LockType LockType { get => lockType; set => lockType = value; }
        public bool AllowPassword { get => allowPassword; set => allowPassword = value; }

        public string CardId { get; set; }

        public SLock( string cardId)
        {
            CardId = cardId;
        }
        public SLock(){
            ;
            }

        public bool Active
        {
            get => active; set
            {
                Card card = GlobalFinder.FindCard(CardId);
                if (card == null) return;
                if (!card.Editable && value)
                {
                    return;
                }
                else
                {
                    active = value;
                }

                if (LockType == LockType.CardLock&&card!=null)
                {
                    active = value;

                    card.Editable = !active;
                    card.RaisePropertyChanged("Editable");


                }
                else if ( LockType == LockType.ColumnLock && card != null )
                {
                    Syncfusion.UI.Xaml.Kanban.KanbanColumn column = GlobalFinder.FindColumn(card.Category);
                    column.AllowDrag = !active;
                    column.AllowDrop= !active;
                    foreach (var item in column.Cards)
                    {
                        var curcard = (item.Content as Card);
                      curcard.Editable= !active;
                        curcard.RaisePropertyChanged("Editable");

                    }
                }

            }
        }
    }

    public class Pomodoro
    {
        public TimeSpan WorkSpan { get; set; }
        public TimeSpan BreakSpan { get; set; }
        public int RecurTime { get; set; } = 1;
        public PomodoroLinkType PomodoroLinkType { get; set; } = PomodoroLinkType.None;

    }
}
