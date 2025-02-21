using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.Notification;
using SmartLibrary.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SmartLibrary.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationViewModel>> GetAllAsync(string userId, bool isAdmin);
        Task<NotificationViewModel> GetByIdAsync(int id);
        Task CreateAsync(CreateNotificationViewModel model);
        Task UpdateAsync(EditNotificationViewModel model);
        Task DeleteAsync(int id);
        Task MarkAsReadAsync(int id);
    }

    


}