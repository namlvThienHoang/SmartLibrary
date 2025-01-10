using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.Admin
{
    public class DashboardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalBooks { get; set; }
        public int TotalLoans { get; set; }
        public int TotalReturned { get; set; }
    }

}