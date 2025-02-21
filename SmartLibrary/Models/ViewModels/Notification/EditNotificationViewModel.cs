using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.Notification
{
    public class EditNotificationViewModel
    {
        public int NotificationId { get; set; }

        [Required]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Nội dung")]
        public string Message { get; set; }
        [Display(Name = "Loại thông báo")]
        public string NotificationType { get; set; }
        [Display(Name = "Đường dẫn")]
        public string RedirectUrl { get; set; }

        public bool IsRead { get; set; }

        public List<string> UserIds { get; set; } = new List<string>();
    }

}