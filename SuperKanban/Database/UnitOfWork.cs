using LiteDB;
using SuperKanban.Model.Entities;
using SuperKanban.ViewModel;
namespace SuperKanban.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private const string Name = "superkanban.db";
        public IRepository<Board> Boards { get; }
        public IRepository<BoardTreeViewModel> BoardTreeViewModels { get; }
        //public IRepository<Task> Tasks { get; }

        //public IRepository<Settings> Settings { get; }

        private readonly ILiteDatabase _database;

        public UnitOfWork()
        {
            _database = new LiteDatabase(Name);
            //BoardTreeViewModels = new BaseRepository<BoardTreeViewModel>(_database, "BoardTreeViewModels");
            //if ((BoardTreeViewModels as BaseRepository<BoardTreeViewModel>).count() == 0)
            //{
            //    BoardTreeViewModels.Insert(new BoardTreeViewModel());
            //}
            Board a = new Board() { Name = "番茄看板", Category = "我的看板" };
            a.BoardColumns.Add(new BoardColumn("2323", "212131"));
            Boards = new BaseRepository<Board>(_database, "Boards");
            //Boards.Insert(a);
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}