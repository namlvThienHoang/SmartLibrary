using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.BorrowBook;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static SmartLibrary.Models.ModelCommons;

namespace SmartLibrary.Services.Implementations
{
    public class BorrowBookService : IBorrowBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BorrowBookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<BorrowBookViewModel>> GetBorrowedBooks(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var (borrowedBooks, totalCount) = await _unitOfWork.BorrowBookRepository.GetBorrowedBooksAsync(searchString, sortOrder, pageNumber, pageSize);

            var borrowedBookViewModels = _mapper.Map<IEnumerable<BorrowBookViewModel>>(borrowedBooks);

            return new PagedResult<BorrowBookViewModel>
            {
                Items = borrowedBookViewModels,
                Pagination = new PaginationInfo
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalCount
                },
            };
        }

        public async Task<BorrowBookViewModel> GetBorrowBookById(int id)
        {
            var borrowBook = await _unitOfWork.BorrowBookRepository.GetBorrowBookByIdAsync(id);
            return _mapper.Map<BorrowBookViewModel>(borrowBook);
        }

        public async Task CreateBorrowBook(CreateBorrowBookViewModel borrowBookVM)
        {
            var borrowBook = _mapper.Map<BorrowTransaction>(borrowBookVM);
            borrowBook.Status = BorrowBookStatus.DangMuon;
            await _unitOfWork.BorrowBookRepository.AddBorrowBookAsync(borrowBook);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteBorrowBook(int id)
        {
            await _unitOfWork.BorrowBookRepository.DeleteBorrowBookAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<EditBorrowBookViewModel> GetBorrowBookEditById(int id)
        {
            var borrowBook = await _unitOfWork.BorrowBookRepository.GetBorrowBookByIdAsync(id);
            return _mapper.Map<EditBorrowBookViewModel>(borrowBook); 
        }

        public async Task EditBorrowBook(EditBorrowBookViewModel borrowBookVM)
        {

            var borrowBook = _mapper.Map<BorrowTransaction>(borrowBookVM);
            await _unitOfWork.BorrowBookRepository.UpdateBorrowBookAsync(borrowBook);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}