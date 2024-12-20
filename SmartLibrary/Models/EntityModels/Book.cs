using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.EntityModels
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ISBN { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string CoverImage { get; set; }

        // Navigation property
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<BookReview> BookReviews { get; set; }
        public virtual ICollection<BorrowTransaction> BorrowTransactions { get; set; }
    }


}