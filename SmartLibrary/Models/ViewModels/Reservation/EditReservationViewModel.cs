using System;
using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models.ViewModels.Reservation
{
    public class EditReservationViewModel
    {
        public int ReservationId { get; set; }

        [Display(Name = "Tên sách")]
        public int BookId { get; set; }

        [Display(Name = "Ngày đặt trước")]
        public DateTime ReservationDate { get; set; }

        [Display(Name = "Tên người dùng")]
        public string UserId { get; set; }

        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [Display(Name = "Ngày hủy")]
        public DateTime? CancelDate { get; set; }
        public string UserName { get; set; }
        public string BookTitle { get; set; }
    }
}