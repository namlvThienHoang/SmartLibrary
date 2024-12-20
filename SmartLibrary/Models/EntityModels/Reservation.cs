using System;

namespace SmartLibrary.Models.EntityModels
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime? CancelDate { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
    }
}