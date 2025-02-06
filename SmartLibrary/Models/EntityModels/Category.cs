using System.Collections.Generic;

namespace SmartLibrary.Models.EntityModels
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }

        // Navigation property
        public virtual ICollection<BookCategory> BookCategories { get; set; }
    }
}