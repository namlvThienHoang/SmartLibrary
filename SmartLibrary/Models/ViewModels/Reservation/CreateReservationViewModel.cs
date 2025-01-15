using System;
using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models.ViewModels.Reservation
{
    public class CreateReservationViewModel
    {
        [Required]
        [Display(Name = "Tên sách")]
        public int BookId { get; set; }

        [Required]
        [Display(Name = "Ngày đặt trước")]
        public DateTime ReservationDate { get; set; }

        [Required]
        [Display(Name = "Tên người dùng")]
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }

        [Display(Name = "Ngày hủy")]
        public DateTime? CancelDate { get; set; }
    }
}