using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } // Danh sách dữ liệu
        public int PageNumber { get; set; } // Trang hiện tại
        public int PageSize { get; set; } // Số mục mỗi trang
        public int TotalItems { get; set; } // Tổng số mục

        public int TotalPages => (TotalItems + PageSize - 1) / PageSize; // Tổng số trang
    }
}