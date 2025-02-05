using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<PagedResult<CategoryViewModel>> GetCategories(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<IEnumerable<CategoryViewModel>> GetAllCategories();
        Task<CategoryViewModel> GetCategoryById(int id);
        Task<EditCategoryViewModel> GetCategoryEditById(int id);
        Task CreateCategory(CreateCategoryViewModel CategoryViewModel);
        Task EditCategory(EditCategoryViewModel CategoryViewModel);
        Task DeleteCategory(int id);
    }
}
