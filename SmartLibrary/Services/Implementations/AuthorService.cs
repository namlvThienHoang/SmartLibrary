using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Author;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<AuthorViewModel>> GetAuthors(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var (Authors, totalCount) = await _unitOfWork.AuthorRepository.GetAuthorsAsync(searchString, sortOrder, pageNumber, pageSize);

            var AuthorViewModels = _mapper.Map<IEnumerable<AuthorViewModel>>(Authors);

            return new PagedResult<AuthorViewModel>
            {
                Items = AuthorViewModels,
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

        public async Task<IEnumerable<AuthorViewModel>> GetAllAuthors()
        {
            var Authors = await _unitOfWork.AuthorRepository.GetAllAuthorAsync();
            return _mapper.Map<IEnumerable<AuthorViewModel>>(Authors);
        }

        public async Task<AuthorViewModel> GetAuthorById(int id)
        {
            var Author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(id);
            return _mapper.Map<AuthorViewModel>(Author);
        }

        public async Task<EditAuthorViewModel> GetAuthorEditById(int id)
        {
            var Author = await _unitOfWork.AuthorRepository.GetAuthorByIdAsync(id);
            return _mapper.Map<EditAuthorViewModel>(Author);
        }

        public async Task CreateAuthor(CreateAuthorViewModel AuthorViewModel)
        {
            var Author = _mapper.Map<Author>(AuthorViewModel);
            await _unitOfWork.AuthorRepository.AddAuthorAsync(Author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditAuthor(EditAuthorViewModel AuthorViewModel)
        {
            var Author = _mapper.Map<Author>(AuthorViewModel);
            await _unitOfWork.AuthorRepository.UpdateAuthorAsync(Author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAuthor(int id)
        {
            await _unitOfWork.AuthorRepository.DeleteAuthorAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}