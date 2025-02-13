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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Category> Categorys, int TotalCount)> GetCategoriesAsync(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.Categories.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.CategoryName.Contains(searchString));
            }
            int totalCount = await query.CountAsync();

            // Sắp xếp
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(b => b.CategoryName);
                    break;
                default:
                    query = query.OrderBy(b => b.CategoryName);
                    break;
            }

            // Phân trang
            var Categorys = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (Categorys, totalCount);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task CreateCategoryAsync(Category Category)
        {
            _context.Categories.Add(Category);
        }

        public async Task UpdateCategoryAsync(Category Category)
        {
            var existingCategory = await _context.Categories
                .FindAsync(Category.CategoryId);

            if (existingCategory == null)
            {
                throw new InvalidOperationException("Không tìm thấy loại sách.");
            }

            // Cập nhật thông tin sách
            _context.Entry(existingCategory).CurrentValues.SetValues(Category);
        }

        public async Task DeleteAsync(int id)
        {
            var Category = await _context.Categories.FindAsync(id);
            if (Category != null)
            {
                _context.Categories.Remove(Category);
            }
        }
    }
}