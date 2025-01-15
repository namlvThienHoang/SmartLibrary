using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.EntityModels
{
    public class UserSetting
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Theme { get; set; } = "light"; // light or dark
        public string Language { get; set; } = "vi"; // vi or en
        public bool EmailNotifications { get; set; } = true;
        public bool SmsNotifications { get; set; } = true;
        public bool GridView { get; set; } = true;
        public int BooksPerPage { get; set; } = 10;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public ApplicationUser User { get; set; }
    }
}