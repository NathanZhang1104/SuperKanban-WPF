using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SuperKanban.ViewModel;
using Syncfusion.UI.Xaml.Kanban;
namespace SuperKanban.Model.Entities
{
    public struct BoardColumn{
        public string Title { get; set; }
        public string Category { get; set; }
        public List<int> CardIndexList { get; set; }

        public BoardColumn(KanbanColumn kanbanColumn,Board board) 
        {
            CardIndexList = new List<int>();

            if (kanbanColumn != null)
            {
                Title = kanbanColumn.Title.ToString();
                Category = kanbanColumn.Categories as string;
                foreach (var item in kanbanColumn.Cards)
                {
                   var card= item.Content as Card;
                    
                    CardIndexList.Add(board.Cards.IndexOf(card));

                }

            }
            else
            {
                Title = "none";
                Category = "null";

            }
        }

        public BoardColumn(string name, string id)
        {
            this.Title = name;
            Category = id;
            CardIndexList = new List<int>();
        }
    }
    public class Board
    {
        public Board()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; } = "none";
        public string Description { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public ObservableCollection<Card> Cards { get; set; } = new ObservableCollection<Card>();
        public ObservableCollection<BoardColumn> BoardColumns { get; set; } = new ObservableCollection<BoardColumn>();
    }
}