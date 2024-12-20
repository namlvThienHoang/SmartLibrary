using System;
using System.Collections.Generic;

namespace SmartLibrary.Models.EntityModels
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }

        // Navigation property for BookAuthor
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }

}