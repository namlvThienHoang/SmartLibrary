using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SmartLibrary.Models.ViewModels.LibrarySetting
{
    public class LibrarySettingViewModel
    {
        public int LibrarySettingId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập khóa")]
        [Display(Name = "Khóa")]
        public string SettingKey { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá trị")]
        [Display(Name = "Giá trị")]
        public string SettingValue { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; } // Mô tả cấu hình

        [Required(ErrorMessage = "Vui lòng chọn kiểu dữ liệu")]
        [Display(Name = "Kiểu dữ liệu")]
        public string DataType { get; set; }

        // Dùng cho trường hợp upload hình ảnh
        public HttpPostedFileBase FileUpload { get; set; }
    }


}