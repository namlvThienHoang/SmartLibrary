using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Repositories.Implementations
{
    public class BookReservationRepository : IBookReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public BookReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Reservation> Reservations, int TotalCount)> GetReservationsAsync(string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.Reservations.Include(r => r.Book).Include(r => r.User).AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.Book.Title.Contains(searchString));
            }
            int totalCount = await query.CountAsync();

            // Sắp xếp
            switch (sortOrder)
            {
                case "reservationDate_desc":
                    query = query.OrderByDescending(b => b.ReservationDate);
                    break;
                default:
                    query = query.OrderBy(b => b.ReservationDate);
                    break;
            }

            var reservations = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (reservations, totalCount);
        }

        public async Task<Reservation> GetReservationByIdAsync(int id)
        {
            return await _context.Reservations.Include(r => r.Book).Include(r => r.User)
                                                   .FirstOrDefaultAsync(r => r.ReservationId == id);
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            var existingReservation = await _context.Reservations
                .FindAsync(reservation.ReservationId);

            if (existingReservation == null)
            {
                throw new InvalidOperationException("Không tìm thấy tác giả.");
            }

            // Cập nhật thông tin sách
            _context.Entry(existingReservation).CurrentValues.SetValues(reservation);
        }
    }
}