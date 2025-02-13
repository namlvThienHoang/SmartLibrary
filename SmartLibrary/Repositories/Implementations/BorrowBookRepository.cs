using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Repositories.Implementations
{
    public class BorrowBookRepository : IBorrowBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowBookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<BorrowTransaction> BorrowedBooks, int TotalCount)> GetBorrowedBooksAsync(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.BorrowTransactions.Include(r => r.Book).Include(r => r.User).AsQueryable();
            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.Book.Title.Contains(searchString));
            }
            int totalCount = await query.CountAsync();

            // Sắp xếp
            switch (sortOrder)
            {
                case "borrowDate_desc":
                    query = query.OrderByDescending(b => b.BorrowDate);
                    break;
                default:
                    query = query.OrderBy(b => b.BorrowDate);
                    break;
            }

            var borrowedBooks = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (borrowedBooks, totalCount);
        }

        public async Task<BorrowTransaction> GetBorrowBookByIdAsync(int id)
        {
            return await _context.BorrowTransactions.FindAsync(id);
        }

        public async Task AddBorrowBookAsync(BorrowTransaction borrowBook)
        {
            _context.BorrowTransactions.Add(borrowBook);
        }

        public async Task DeleteBorrowBookAsync(int id)
        {
            var borrowBook = await _context.BorrowTransactions.FindAsync(id);
            if (borrowBook != null)
            {
                _context.BorrowTransactions.Remove(borrowBook);
            }
        }

        public async Task UpdateBorrowBookAsync(BorrowTransaction borrowBook)
        {
            var existingBorrow = await _context.BorrowTransactions
                .FindAsync(borrowBook.BorrowTransactionId);

            if (existingBorrow == null)
            {
                throw new InvalidOperationException("Không tìm thấy loại sách.");
            }

            // Cập nhật thông tin sách
            _context.Entry(existingBorrow).CurrentValues.SetValues(borrowBook);
        }
    }
}