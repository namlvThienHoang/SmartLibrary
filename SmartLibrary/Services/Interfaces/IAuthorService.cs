using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Author;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<PagedResult<AuthorViewModel>> GetAuthors(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<IEnumerable<AuthorViewModel>> GetAllAuthors();
        Task<AuthorViewModel> GetAuthorById(int id);
        Task<EditAuthorViewModel> GetAuthorEditById(int id);
        Task CreateAuthor(CreateAuthorViewModel AuthorViewModel);
        Task EditAuthor(EditAuthorViewModel AuthorViewModel);
        Task DeleteAuthor(int id);
    }
}
