using System;

namespace SmartLibrary.Models.EntityModels
{
    public class BookReview
    {
        public int BookReviewId { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; } // 1-5
        public string Review { get; set; }
        public DateTime ReviewDate { get; set; }

        public virtual Book Book { get; set; }
        public virtual ApplicationUser User { get; set; }
    }


}