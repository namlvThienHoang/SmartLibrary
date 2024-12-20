using System;

namespace SmartLibrary.Models.EntityModels
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }

        public virtual ApplicationUser User { get; set; }
    }



}