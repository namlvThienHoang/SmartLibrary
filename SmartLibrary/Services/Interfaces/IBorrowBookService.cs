using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.BorrowBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Interfaces
{
    public interface IBorrowBookService
    {
        Task<PagedResult<BorrowBookViewModel>> GetBorrowedBooks(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<BorrowBookViewModel> GetBorrowBookById(int id);
        Task<EditBorrowBookViewModel> GetBorrowBookEditById(int id);
        Task CreateBorrowBook(CreateBorrowBookViewModel borrowBookVM);
        Task EditBorrowBook(EditBorrowBookViewModel borrowBookVM);
        Task DeleteBorrowBook(int id);
    }
}
