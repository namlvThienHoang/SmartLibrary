using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; } // User's unique identifier (for editing or deleting)

        [Display(Name = "Tên người dùng")]
        public string UserName { get; set; }

        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [Display(Name = "Quyền")]
        public string Role { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
        [Display(Name = "Hình ảnh")]
        public string AvatarURL { get; set; }
    }
}