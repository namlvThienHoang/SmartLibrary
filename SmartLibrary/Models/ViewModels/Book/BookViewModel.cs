using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models.ViewModels.Book
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tiêu đề")]
        public string Title { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Nhà xuất bản")]
        public string Publisher { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày xuất bản")]
        public DateTime PublishedDate { get; set; }

        [Display(Name = "Mã số")]
        public string ISBN { get; set; }

        [Display(Name = "Tổng số bản sao")]
        public int TotalCopies { get; set; }

        [Display(Name = "Có sẵn")]
        public int AvailableCopies { get; set; }

        [Display(Name = "Ảnh bìa")]
        public string CoverImage { get; set; }


        // Các ID cho quan hệ nhiều-nhiều

        [Display(Name = "Tác giả")]
        public string Authors { get; set; }
        [Display(Name = "Thể loại")]
        public string Categories { get; set; }

        // Danh sách đánh giá
        public List<BookReviewViewModel> Reviews { get; set; } = new List<BookReviewViewModel>();

        // Điểm trung bình
        public double AverageRating { get; set; }
    }
}