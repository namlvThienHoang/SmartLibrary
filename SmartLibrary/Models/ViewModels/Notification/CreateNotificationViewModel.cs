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
        [StringLength(200, ErrorMessage = "Nội dung thông báo không được quá 200 ký tự.")]
        public string Message { get; set; }
    }

}