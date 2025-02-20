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

        // Phương thức tĩnh để gửi thông báo đến tất cả các client (admin)
        public static void Notify(Notification notification)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
            context.Clients.All.receiveNotification(new
            {
                id = notification.NotificationId,
                message = notification.Message,
                createdDate = notification.CreatedDate.ToString("dd/MM/yyyy HH:mm"),
                isRead = notification.IsRead
            });
        }
    }
}