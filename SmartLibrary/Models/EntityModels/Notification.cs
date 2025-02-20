using System;

namespace SmartLibrary.Models.EntityModels
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; } // Mã người nhận thông báo (có thể là admin hoặc nhiều admin)
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsRead { get; set; }

        // Các thuộc tính mở rộng (tùy chọn)
        public string NotificationType { get; set; }
        public string RedirectUrl { get; set; }
        public string Title { get; set; }

        public virtual ApplicationUser User { get; set; }
    }




}