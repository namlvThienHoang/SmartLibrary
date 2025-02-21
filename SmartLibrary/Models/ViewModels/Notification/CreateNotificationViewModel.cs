using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.Notification
{
    public class CreateNotificationViewModel
    {
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
        public bool SendToAll { get; set; } // true: gửi tất cả, false: chọn user cụ thể
        public List<string> SelectedUserIds { get; set; } = new List<string>(); // Chứa ID các user được chọn nếu không gửi cho tất cả
    }

}