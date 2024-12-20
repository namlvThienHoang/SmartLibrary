using System;

namespace SmartLibrary.Models.EntityModels
{
    public class FineTransaction
    {
        public int FineTransactionId { get; set; }
        public string UserId { get; set; }
        public int BorrowTransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime FineDate { get; set; }
        public bool IsPaid { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual BorrowTransaction BorrowTransaction { get; set; }
    }


}