using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Interfaces
{
    public interface IBookReservationService
    {
        Task<PagedResult<ReservationViewModel>> GetReservations(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<ReservationViewModel> GetReservationById(int id);
        Task<EditReservationViewModel> GetReservationEditById(int id);
        Task CreateReservation(CreateReservationViewModel reservationVM);
        Task EditReservation(EditReservationViewModel reservationVM);
        Task DeleteReservation(int id);
    }
}
