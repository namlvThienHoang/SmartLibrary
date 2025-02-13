using SmartLibrary.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Interfaces
{
    public interface IBorrowBookRepository
    {
        Task<(IEnumerable<BorrowTransaction> BorrowedBooks, int TotalCount)> GetBorrowedBooksAsync(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<BorrowTransaction> GetBorrowBookByIdAsync(int id);
        Task AddBorrowBookAsync(BorrowTransaction borrowBook);
        Task UpdateBorrowBookAsync(BorrowTransaction borrowBook);
        Task DeleteBorrowBookAsync(int id);
    }
}
