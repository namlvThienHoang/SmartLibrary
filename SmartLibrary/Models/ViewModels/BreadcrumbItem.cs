using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels
{
    public class BreadcrumbItem
    {
        public string Title { get; set; } // Tiêu đề của mục
        public string Url { get; set; }   // URL dẫn đến
        public bool IsActive { get; set; } // Đánh dấu mục hiện tại
    }
}