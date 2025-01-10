using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.User
{
    public class CreateUserViewModel
    {
        [Required]
        [Display(Name = "Tên người dùng")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Địa chỉ email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} phải dài ít nhất {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
        [Display(Name = "Xác nhận mật khẩu")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Vai trò")]
        public string Role { get; set; }

        [Phone]
        [Display(Name = "Số điện thoại")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Tên đầy đủ")]
        public string FullName { get; set; }

        [Display(Name = "Ngày sinh")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
    }
}