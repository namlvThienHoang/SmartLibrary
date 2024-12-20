using System;

namespace SmartLibrary.Models.EntityModels
{
    public class BorrowTransaction
    {
        public int BorrowTransactionId { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; } // Borrowed, Returned, Overdue

        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
    }


}