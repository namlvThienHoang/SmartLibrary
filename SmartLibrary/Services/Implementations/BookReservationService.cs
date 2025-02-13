using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Reservation;
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
    public class BookReservationService : IBookReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReservationViewModel>> GetReservations(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var (reservations, totalCount) = await _unitOfWork.BookReservationRepository.GetReservationsAsync(searchString, sortOrder, pageNumber, pageSize);

            var reservationViewModels = _mapper.Map<IEnumerable<ReservationViewModel>>(reservations);

            return new PagedResult<ReservationViewModel>
            {
                Items = reservationViewModels,
                Pagination = new PaginationInfo
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalCount
                }
            };
        }

        public async Task<ReservationViewModel> GetReservationById(int id)
        {
            var reservation = await _unitOfWork.BookReservationRepository.GetReservationByIdAsync(id);
            return _mapper.Map<ReservationViewModel>(reservation);
        }

        public async Task CreateReservation(CreateReservationViewModel reservationVM)
        {
            var reservation = _mapper.Map<Reservation>(reservationVM);
            reservation.Status = ReservationStatus.DangCho;
            await _unitOfWork.BookReservationRepository.AddReservationAsync(reservation);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteReservation(int id)
        {
            await _unitOfWork.BookReservationRepository.DeleteReservationAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<EditReservationViewModel> GetReservationEditById(int id)
        {
            var reservation = await _unitOfWork.BookReservationRepository.GetReservationByIdAsync(id);
            return _mapper.Map<EditReservationViewModel>(reservation);
        }

        public async Task EditReservation(EditReservationViewModel reservationVM)
        {
            var reservation = _mapper.Map<Reservation>(reservationVM);
            await _unitOfWork.BookReservationRepository.UpdateReservationAsync(reservation);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}