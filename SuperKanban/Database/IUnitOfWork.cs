using System;
using SuperKanban.Model.Entities;
namespace SuperKanban.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Board> Boards { get; }
    }
}