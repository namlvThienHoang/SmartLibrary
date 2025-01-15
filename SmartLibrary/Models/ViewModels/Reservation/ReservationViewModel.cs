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

        [Required]
        [Display(Name = "Ngày đặt trước")]
        public DateTime ReservationDate { get; set; }

        [Required]
        [Display(Name = "Tên người dùng")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [Display(Name = "Ngày hủy")]
        public DateTime? CancelDate { get; set; }
    }
}