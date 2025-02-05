using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository BookRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}
