using System;
using System.Collections.ObjectModel;
using SuperKanban.ViewModel;
namespace SuperKanban.Model.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public ObservableCollection<Card> Cards { get; set; } = new ObservableCollection<Card>();
    }
}