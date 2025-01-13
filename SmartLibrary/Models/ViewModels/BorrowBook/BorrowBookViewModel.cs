using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.BorrowBook
{
    public class BorrowBookViewModel
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; }
        public int AvailableCopies { get; set; }

        [Required(ErrorMessage = "Bắt buộc chọn người dùng")]
        public string UserId { get; set; } // Người mượn

        public DateTime BorrowDate { get; set; } = DateTime.Now;
    }
}