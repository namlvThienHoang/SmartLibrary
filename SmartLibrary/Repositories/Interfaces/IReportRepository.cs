using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Repositories.Interfaces
{
    public interface IReportRepository
    {
        int GetTotalBooks();
        int GetTotalBorrowedBooks();
        int GetTotalReturnedBooks();
        Dictionary<int, int> GetBooksBorrowedByMonth(int year);
    }
}