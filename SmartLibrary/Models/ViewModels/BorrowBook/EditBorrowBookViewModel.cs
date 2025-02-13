using System;
using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models.ViewModels.BorrowBook
{
    public class EditBorrowBookViewModel
    {
        public int BorrowTransactionId { get; set; }
        [Display(Name = "Người mượn")]
        public string UserId { get; set; }
        [Display(Name = "Tên sách")]
        public int BookId { get; set; }
        [Display(Name = "Ngày mượn")]
        public DateTime BorrowDate { get; set; }

        [Display(Name = "Ngày hết hạn")]
        public DateTime? DueDate { get; set; }

        [Display(Name = "Ngày trả")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
        public string UserName { get; set; }
        public string BookTitle { get; set; }
    }
}