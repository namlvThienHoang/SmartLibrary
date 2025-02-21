using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.Reservation
{
    public class ReservationViewModel
    {
        public int ReservationId { get; set; }

        [Required]
        [Display(Name = "Tên sách")]
        public string BookTitle { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        [Display(Name = "Ngày đặt trước")]
        public DateTime ReservationDate { get; set; }

        [Required]
        [Display(Name = "Tên người dùng")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ngày hủy")]
        public DateTime? CancelDate { get; set; }
    }
}