using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.Notification
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }
        public string Message { get; set; }
        public string CreatedDate { get; set; }
        public bool IsRead { get; set; }
    }

}