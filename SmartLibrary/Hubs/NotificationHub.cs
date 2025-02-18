using Microsoft.AspNet.SignalR;
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
    }
}