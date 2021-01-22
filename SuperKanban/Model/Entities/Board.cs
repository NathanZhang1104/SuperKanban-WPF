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

        public BoardColumn(KanbanColumn kanbanColumn) 
        {
            if (kanbanColumn != null)
            {
                Title = kanbanColumn.Title.ToString();
                Category = kanbanColumn.Categories as string;
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
        }
    }
    public class Board
    {
        public Board()
        {
            BoardColumn bc = new BoardColumn() { Title = "todo", Category = "12123" };
            BoardColumns.Add(bc);
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