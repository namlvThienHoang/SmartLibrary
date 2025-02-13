using SmartLibrary.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Interfaces
{
    public interface IBookReservationRepository
    {
        Task<(IEnumerable<Reservation> Reservations, int TotalCount)> GetReservationsAsync(string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<Reservation> GetReservationByIdAsync(int id);
        Task AddReservationAsync(Reservation reservation);
        Task UpdateReservationAsync(Reservation reservation);
        Task DeleteReservationAsync(int id);
    }
}
