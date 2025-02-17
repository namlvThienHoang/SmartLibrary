using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.Report
{
    public class ReportViewModel
    {
        public int TotalBooks { get; set; }
        public int TotalBorrowedBooks { get; set; }
        public int TotalReturnedBooks { get; set; }
        public int TotalMembers { get; set; }
        public List<BookCategoryStatistics> BookCategoryStats { get; set; }
        public Dictionary<int, int> BorrowedBooksByMonth { get; set; }
    }
}