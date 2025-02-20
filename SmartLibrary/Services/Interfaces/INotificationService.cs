using SmartLibrary.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetNotificationsByUserIdAsync(string userId);
        Task<int> GetUnreadNotificationCountAsync(string userId);
        Task<Notification> GetNotificationByIdAsync(int notificationId);
        Task CreateNotificationAsync(Notification notification);
        Task CreateNotificationForAllUsersAsync(string message, List<string> userIds);
        Task MarkNotificationAsReadAsync(int notificationId);
        Task MarkAllNotificationsAsReadAsync(string userId);
        Task DeleteNotificationAsync(int notificationId);
        Task UpdateNotificationAsync(Notification notification);
    }

}