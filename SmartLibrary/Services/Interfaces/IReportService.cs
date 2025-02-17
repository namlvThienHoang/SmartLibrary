using SmartLibrary.Models.ViewModels.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Interfaces
{
    public interface IReportService
    {
        int GetTotalBooks();
        int GetTotalBorrowedBooks();
        int GetTotalReturnedBooks();
        Task<List<BookCategoryStatistics>> GetBookCategoryStatsAsync();
        Dictionary<int, int> GetBooksBorrowedByMonth(int year);
    }
}
