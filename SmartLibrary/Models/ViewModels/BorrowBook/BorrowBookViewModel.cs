using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.BorrowBook
{
    public class BorrowBookViewModel
    {
        public int BorrowTransactionId { get; set; }
        [Display(Name = "Tên người mượn")]
        public string UserName { get; set; }
        [Display(Name = "Tên sách")]
        public string BookTitle { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày mượn")]
        public DateTime BorrowDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày hết hạn")]
        public DateTime? DueDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày trả")]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
    }
}