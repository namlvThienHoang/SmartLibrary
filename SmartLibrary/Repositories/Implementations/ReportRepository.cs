using SmartLibrary.Models;
using SmartLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int GetTotalBooks()
        {
            return _context.Books.Count();
        }

        public int GetTotalBorrowedBooks()
        {
            return _context.BorrowTransactions.Count(b => b.ReturnDate == null);
        }

        public int GetTotalReturnedBooks()
        {
            return _context.BorrowTransactions.Count(b => b.ReturnDate != null);
        }

        public Dictionary<int, int> GetBooksBorrowedByMonth(int year)
        {
            return _context.BorrowTransactions
                .Where(x=>x.BorrowDate.Year == year)
                .GroupBy(b => b.BorrowDate.Month)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}