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
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Book> Books, int TotalCount)> GetBooksAsync(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.Books.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.Title.Contains(searchString) 
                || b.BookAuthors.Select(x => x.Author.AuthorName).Contains(searchString));
            }
            int totalCount = await query.CountAsync();

            // Sắp xếp
            switch (sortOrder)
            {
                case "title_desc":
                    query = query.OrderByDescending(b => b.Title);
                    break;
                case "publisher":
                    query = query.OrderBy(b => b.Publisher);
                    break;
                case "publisher_desc":
                    query = query.OrderByDescending(b => b.Publisher);
                    break;
                case "publishedDate":
                    query = query.OrderBy(b => b.PublishedDate);
                    break;
                case "publishedDate_desc":
                    query = query.OrderByDescending(b => b.PublishedDate);
                    break;
                default:
                    query = query.OrderBy(b => b.Title);
                    break;
            }

            // Phân trang
            var books = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (books, totalCount);
        }

        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task CreateBookAsync(Book book)
        {
            _context.Books.Add(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            var existingBook = await _context.Books
                .Include(b => b.BookAuthors)
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(b => b.BookId == book.BookId);

            if (existingBook == null)
            {
                throw new InvalidOperationException("Book not found.");
            }

            // Cập nhật thông tin sách
            _context.Entry(existingBook).CurrentValues.SetValues(book);

            // Xóa quan hệ cũ
            existingBook.BookAuthors.Clear();
            existingBook.BookCategories.Clear();
            await _context.SaveChangesAsync();

            // Cập nhật quan hệ mới
            existingBook.BookAuthors = book.BookAuthors;
            existingBook.BookCategories = book.BookCategories;
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }
    }
}