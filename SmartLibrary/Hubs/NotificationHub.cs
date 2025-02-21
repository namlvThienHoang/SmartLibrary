using Microsoft.AspNet.SignalR;
using SmartLibrary.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Hubs
{
    public class NotificationHub : Hub
    {
        public static void NotifyAdmin(string message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.receiveNotification(message);
        }

        // Gửi thông báo từ Admin đến tất cả người dùng
        public static void Notify(Notification notification)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            var notificationData = new
            {
                id = notification.NotificationId,
                message = notification.Message,
                createdDate = notification.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                isRead = notification.IsRead
            };
            context.Clients.All.SendAsync("ReceiveNotification", notificationData);
        }
    }
}