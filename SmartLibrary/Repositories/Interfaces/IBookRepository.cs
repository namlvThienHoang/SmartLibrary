using SmartLibrary.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<(IEnumerable<Book> Books, int TotalCount)> GetBooksAsync(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<IEnumerable<Book>> GetAllBookAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task CreateBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
    }
}