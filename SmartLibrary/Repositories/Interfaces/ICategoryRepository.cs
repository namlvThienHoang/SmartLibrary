using SmartLibrary.Models.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<(IEnumerable<Category> Categorys, int TotalCount)> GetCategoriesAsync(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(Category Category);
        Task UpdateCategoryAsync(Category Category);
        Task DeleteAsync(int id);
    }
}