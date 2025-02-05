using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Author;
using SmartLibrary.Models.ViewModels.Book;
using SmartLibrary.Models.ViewModels.Category;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryViewModel>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<List<CategoryViewModel>>(categories.ToList());
        }

        public async Task<List<AuthorViewModel>> GetAuthorsAsync()
        {
            var authors = await _unitOfWork.AuthorRepository.GetAllAuthorAsync();
            return _mapper.Map<List<AuthorViewModel>>(authors.ToList());
        }

        public async Task<PagedResult<BookViewModel>> GetBooks(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var (books, totalCount) = await _unitOfWork.BookRepository.GetBooksAsync(searchString, sortOrder, pageNumber, pageSize);

            var bookViewModels = _mapper.Map<IEnumerable<BookViewModel>>(books);

            return new PagedResult<BookViewModel>
            {
                Items = bookViewModels,
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

        public async Task<IEnumerable<BookViewModel>> GetAllBooks()
        {
            var books = await _unitOfWork.BookRepository.GetAllBookAsync();
            return _mapper.Map<IEnumerable<BookViewModel>>(books);
        }

        public async Task<BookViewModel> GetBookById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetBookByIdAsync(id);
            return _mapper.Map<BookViewModel>(book);
        }

        public async Task<EditBookViewModel> GetBookEditById(int id)
        {
            var book = await _unitOfWork.BookRepository.GetBookByIdAsync(id);
            return _mapper.Map<EditBookViewModel>(book);
        }

        public async Task CreateBook(CreateBookViewModel bookViewModel)
        {
            var book = _mapper.Map<Book>(bookViewModel);
            book.AvailableCopies = bookViewModel.TotalCopies;
            // Xử lý quan hệ nhiều-nhiều
            if (bookViewModel.AuthorIds != null && bookViewModel.AuthorIds.Any())
            {
                book.BookAuthors = bookViewModel.AuthorIds
                    .Select(authorId => new BookAuthor { AuthorId = authorId })
                    .ToList();
            }

            if (bookViewModel.CategoryIds != null && bookViewModel.CategoryIds.Any())
            {
                book.BookCategories = bookViewModel.CategoryIds
                    .Select(categoryId => new BookCategory { CategoryId = categoryId })
                    .ToList();
            }
            await _unitOfWork.BookRepository.CreateBookAsync(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditBook(EditBookViewModel bookViewModel)
        {
            var book = _mapper.Map<Book>(bookViewModel);

            // Cập nhật số lượng có sẵn nếu tổng số lượng giảm
            if (book.AvailableCopies > book.TotalCopies)
            {
                book.AvailableCopies = book.TotalCopies;
            }

            // Cập nhật quan hệ nhiều-nhiều với Tác giả (Authors)
            if (bookViewModel.AuthorIds != null)
            {
                book.BookAuthors = new List<BookAuthor>();
                // Thêm tác giả mới
                foreach (var authorId in bookViewModel.AuthorIds)
                {
                    book.BookAuthors.Add(new BookAuthor { AuthorId = authorId, BookId = book.BookId });
                }
            }

            // Cập nhật quan hệ nhiều-nhiều với Thể loại (Categories)
            if (bookViewModel.CategoryIds != null)
            {
                book.BookCategories = new List<BookCategory>();
                // Thêm thể loại mới
                foreach (var categoryId in bookViewModel.CategoryIds)
                {
                    book.BookCategories.Add(new BookCategory { CategoryId = categoryId, BookId = book.BookId });
                }
            }
            await _unitOfWork.BookRepository.UpdateBookAsync(book);
            
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            await _unitOfWork.BookRepository.DeleteBookAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}