using System;
using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models.ViewModels.BorrowBook
{
    public class CreateBorrowBookViewModel
    {
        [Required]
        [Display(Name = "Người mượn")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Tên sách")]
        public int BookId { get; set; }
        [Required]
        [Display(Name = "Ngày mượn")]
        public DateTime BorrowDate { get; set; }
        [Required]

        [Display(Name = "Ngày hết hạn")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
    }
}