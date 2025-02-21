using AutoMapper;
using SmartLibrary.Hubs;
using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.Notification;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<NotificationViewModel>> GetAllAsync(string userId, bool isAdmin)
        {
            var notifications = await _repository.GetAllAsync(userId, isAdmin);

            return notifications.Select(n => new NotificationViewModel
            {
                NotificationId = n.NotificationId,
                Title = n.Title,
                Message = n.Message,
                CreatedDate = n.CreatedDate,
                IsRead = n.IsRead,
                NotificationType = n.NotificationType,
                RedirectUrl = n.RedirectUrl,
                UserIds = n.NotificationUsers.Select(nu => nu.UserId).ToList()
            });
        }


        public async Task<NotificationViewModel> GetByIdAsync(int id)
        {
            var n = await _repository.GetByIdAsync(id);
            if (n == null) return null;

            return new NotificationViewModel
            {
                NotificationId = n.NotificationId,
                Title = n.Title,
                Message = n.Message,
                CreatedDate = n.CreatedDate,
                IsRead = n.IsRead,
                NotificationType = n.NotificationType,
                RedirectUrl = n.RedirectUrl,
                UserIds = n.NotificationUsers.Select(nu => nu.UserId).ToList()
            };
        }

        public async Task CreateAsync(CreateNotificationViewModel model)
        {
            var notification = new Notification
            {
                Title = model.Title,
                Message = model.Message,
                NotificationType = model.NotificationType,
                RedirectUrl = model.RedirectUrl,
                CreatedDate = DateTime.Now,
                IsRead = false,
                NotificationUsers = model.SendToAll
                    ? (await GetAllUsersAsync()).Select(userId => new NotificationUser { UserId = userId }).ToList()
                    : model.SelectedUserIds.Select(userId => new NotificationUser { UserId = userId }).ToList()
            };
            await _repository.AddAsync(notification);
        }

        public async Task UpdateAsync(EditNotificationViewModel model)
        {
            var notification = await _repository.GetByIdAsync(model.NotificationId);
            if (notification != null)
            {
                notification.Title = model.Title;
                notification.Message = model.Message;
                notification.NotificationType = model.NotificationType;
                notification.RedirectUrl = model.RedirectUrl;
                notification.IsRead = model.IsRead;
                notification.NotificationUsers = model.UserIds.Select(userId => new NotificationUser { UserId = userId }).ToList();
                await _repository.UpdateAsync(notification);
            }
        }

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task MarkAsReadAsync(int id) => await _repository.MarkAsReadAsync(id);

        private async Task<List<string>> GetAllUsersAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.Users.Select(u => u.Id).ToListAsync();
            }
        }
    }

}