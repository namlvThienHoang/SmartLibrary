using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models.ViewModels.User
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Tên người dùng")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tên đầy dủ")]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
        [Display(Name = "Hình ảnh")]
        public string AvatarURL { get; set; }
        [Display(Name = "Vai trò")]
        public List<string> Roles { get; set; }
    }
}