using AutoMapper;
using SmartLibrary.Models.ViewModels.Report;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<BookCategoryStatistics>> GetBookCategoryStatsAsync()
        {
            var books = await _unitOfWork.CategoryRepository.GetAllCategoriesAsync();
            return books.Select(g => new BookCategoryStatistics
                        {
                            CategoryName = g.CategoryName,
                            NumberOfBooks = g.BookCategories.Count()
                        }).ToList();
        }

        public Dictionary<int, int> GetBooksBorrowedByMonth(int year)
        {
            return _unitOfWork.ReportRepository.GetBooksBorrowedByMonth(year);
        }

        public int GetTotalBooks()
        {
            return _unitOfWork.ReportRepository.GetTotalBooks(); 
        }

        public int GetTotalBorrowedBooks()
        {
            return _unitOfWork.ReportRepository.GetTotalBorrowedBooks();
        }

        public int GetTotalReturnedBooks()
        {
            return _unitOfWork.ReportRepository.GetTotalReturnedBooks();
        }
    }
}