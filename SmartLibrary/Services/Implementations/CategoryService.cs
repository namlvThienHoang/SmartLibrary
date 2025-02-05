using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Category;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<CategoryViewModel>> GetCategories(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var (Categorys, totalCount) = await _unitOfWork.CategoryRepository.GetCategoriesAsync(searchString, sortOrder, pageNumber, pageSize);

            var CategoryViewModels = _mapper.Map<IEnumerable<CategoryViewModel>>(Categorys);

            return new PagedResult<CategoryViewModel>
            {
                Items = CategoryViewModels,
                Pagination = new PaginationInfo
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalCount
                },
                SearchString = searchString,
                SortOrder = sortOrder
            };
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var Categorys = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryViewModel>>(Categorys);
        }

        public async Task<CategoryViewModel> GetCategoryById(int id)
        {
            var Category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<CategoryViewModel>(Category);
        }

        public async Task<EditCategoryViewModel> GetCategoryEditById(int id)
        {
            var Category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<EditCategoryViewModel>(Category);
        }

        public async Task CreateCategory(CreateCategoryViewModel CategoryViewModel)
        {
            var Category = _mapper.Map<Category>(CategoryViewModel);
            await _unitOfWork.CategoryRepository.CreateCategoryAsync(Category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditCategory(EditCategoryViewModel CategoryViewModel)
        {
            var Category = _mapper.Map<Category>(CategoryViewModel);
            await _unitOfWork.CategoryRepository.UpdateCategoryAsync(Category);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}