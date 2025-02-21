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
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync(string userId, bool isAdmin)
        {
            if (isAdmin)
            {
                return await _context.Notifications
                    .Include(n => n.NotificationUsers)
                    .ToListAsync();
            }
            else
            {
                return await _context.Notifications
                    .Where(n => n.NotificationUsers.Any(nu => nu.UserId == userId))
                    .Include(n => n.NotificationUsers)
                    .ToListAsync();
            }
        }


        public async Task<Notification> GetByIdAsync(int id) =>
            await _context.Notifications
                .Include(n => n.NotificationUsers)
                .FirstOrDefaultAsync(n => n.NotificationId == id);

        public async Task AddAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await SaveAsync();
        }

        public async Task UpdateAsync(Notification notification)
        {
            _context.Entry(notification).State = EntityState.Modified;
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var notification = await GetByIdAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await SaveAsync();
            }
        }

        public async Task MarkAsReadAsync(int id)
        {
            var notification = await GetByIdAsync(id);
            if (notification != null)
            {
                notification.IsRead = true;
                await SaveAsync();
            }
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }

}