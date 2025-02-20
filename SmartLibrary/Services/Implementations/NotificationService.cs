using SmartLibrary.Hubs;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IUnitOfWork _unitOfWork;
        public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
        {
            _notificationRepository = notificationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(string userId)
        {
            return await _notificationRepository.GetNotificationsByUserIdAsync(userId);
        }

        public async Task<int> GetUnreadNotificationCountAsync(string userId)
        {
            return await _notificationRepository.GetUnreadNotificationCountAsync(userId);
        }

        public async Task<Notification> GetNotificationByIdAsync(int notificationId)
        {
            return await _notificationRepository.GetNotificationByIdAsync(notificationId);
        }

        public async Task CreateNotificationAsync(Notification notification)
        {
            // Đặt mặc định thời gian tạo và trạng thái chưa đọc
            notification.CreatedDate = DateTime.Now;
            notification.IsRead = false;
            await _notificationRepository.AddNotificationAsync(notification);
        }

        public async Task CreateNotificationForAllUsersAsync(string message, List<string> userIds)
        {
            var notifications = userIds.Select(userId => new Notification
            {
                UserId = userId,
                Message = message,
                CreatedDate = DateTime.Now,
                IsRead = false
            }).ToList();

            foreach(var item in notifications)
            {
                await _notificationRepository.AddNotificationAsync(item);
                NotificationHub.Notify(item);
            }
        }


        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(notificationId);
            if (notification != null && !notification.IsRead)
            {
                notification.IsRead = true;
                await _notificationRepository.UpdateNotificationAsync(notification);
            }
        }

        public async Task MarkAllNotificationsAsReadAsync(string userId)
        {
            var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId);
            foreach (var notification in notifications.Where(n => !n.IsRead))
            {
                notification.IsRead = true;
                await _notificationRepository.UpdateNotificationAsync(notification);
            }
        }

        public async Task DeleteNotificationAsync(int notificationId)
        {
            var notification = await _notificationRepository.GetNotificationByIdAsync(notificationId);
            if (notification != null)
            {
                await _notificationRepository.DeleteNotificationAsync(notification);
            }
        }

        public async Task UpdateNotificationAsync(Notification notification)
        {
            // Đặt mặc định thời gian tạo và trạng thái chưa đọc
            notification.CreatedDate = DateTime.Now;
            notification.IsRead = false;
            await _notificationRepository.UpdateNotificationAsync(notification);
        }
    }

}