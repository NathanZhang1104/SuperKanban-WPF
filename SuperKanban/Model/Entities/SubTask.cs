using GalaSoft.MvvmLight;
namespace SuperKanban.Model.Entities
{
    public class SubTask
    {
        private bool completed;
        public string Title { get; set; }
        public bool Completed
        {
            get
            {
                return completed;
            }
            set
            {
                completed = value;
            }
        }
    }
}
