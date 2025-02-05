using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Author;
using SmartLibrary.Models.ViewModels.Book;
using SmartLibrary.Models.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Interfaces
{
    public interface IBookService
    {
        Task<PagedResult<BookViewModel>> GetBooks(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<IEnumerable<BookViewModel>> GetAllBooks();
        Task<BookViewModel> GetBookById(int id);
        Task<EditBookViewModel> GetBookEditById(int id);
        Task CreateBook(CreateBookViewModel bookViewModel);
        Task EditBook(EditBookViewModel bookViewModel);
        Task DeleteBook(int id);

        Task<List<CategoryViewModel>> GetCategoriesAsync();

        Task<List<AuthorViewModel>> GetAuthorsAsync();
    }
}
