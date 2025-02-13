using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Author> Authors, int TotalCount)> GetAuthorsAsync(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.Authors.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.AuthorName.Contains(searchString));
            }
            int totalCount = await query.CountAsync();

            // Sắp xếp
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(b => b.AuthorName);
                    break;
                default:
                    query = query.OrderBy(b => b.AuthorName);
                    break;
            }

            // Phân trang
            var Authors = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (Authors, totalCount);
        }

        public async Task<IEnumerable<Author>> GetAllAuthorAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _context.Authors.FindAsync(id);
        }

        public async Task AddAuthorAsync(Author Author)
        {
            _context.Authors.Add(Author);
        }

        public async Task UpdateAuthorAsync(Author Author)
        {
            var existingAuthor = await _context.Authors
                .FindAsync(Author.AuthorId);

            if (existingAuthor == null)
            {
                throw new InvalidOperationException("Không tìm thấy tác giả.");
            }

            // Cập nhật thông tin sách
            _context.Entry(existingAuthor).CurrentValues.SetValues(Author);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var Author = await _context.Authors.FindAsync(id);
            if (Author != null)
            {
                _context.Authors.Remove(Author);
            }
        }
    }
}