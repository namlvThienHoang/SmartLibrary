using System;

namespace SmartLibrary.Models.EntityModels
{
    //Đại diện cho các đơn đặt sách trước.
    //Nếu một cuốn sách không có sẵn, người dùng có thể đặt trước để nhận sách khi có sẵn.
    //Các thuộc tính có thể bao gồm BookId (ID sách đặt trước), UserId(ID người đặt), và ReservationDate(ngày đặt).
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public string Status { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
    }
}