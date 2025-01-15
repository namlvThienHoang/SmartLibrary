using System;

namespace SmartLibrary.Models.EntityModels
{

    //Đại diện cho giao dịch phạt khi người dùng trả sách muộn.
    //Nó liên kết với một BorrowTransaction và ghi lại số tiền phạt phải trả.
    //Thuộc tính có thể bao gồm BorrowTransactionId (ID giao dịch mượn), FineAmount(số tiền phạt), và FineDate(ngày phạt).
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