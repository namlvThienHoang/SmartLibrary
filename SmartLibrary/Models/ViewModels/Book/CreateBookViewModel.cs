using SmartLibrary.Models.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Models.ViewModels.Book
{
    public class CreateBookViewModel
    {

        [Required]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Nhà xuất bản")]
        public string Publisher { get; set; }

        [Required]
        [Display(Name = "Ngày xuất bản")]
        public DateTime PublishedDate { get; set; }

        [Required]
        [Display(Name = "Mã số quốc tế")]
        public string ISBN { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Tổng số bản sao phải lớn hơn 1")]
        [Display(Name = "Tổng số bản sao")]
        public int TotalCopies { get; set; }

        [Display(Name = "Ảnh bìa")]
        public string CoverImage { get; set; }

        // Các ID cho quan hệ nhiều-nhiều

        [Display(Name = "Chọn tác giả")]
        public List<int> AuthorIds { get; set; }
        [Display(Name = "Chọn thể loại")]
        public List<int> CategoryIds { get; set; }

    }
}