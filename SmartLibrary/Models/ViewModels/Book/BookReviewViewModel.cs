using System;

namespace SmartLibrary.Models.ViewModels.Book
{
    // ViewModel cho đánh giá sách
    public class BookReviewViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int Rating { get; set; } // 1-5
        public string Review { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}