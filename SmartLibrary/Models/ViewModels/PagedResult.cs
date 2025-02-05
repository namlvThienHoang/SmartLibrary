using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } // Danh sách các mục (sách, danh mục, v.v.)
        public PaginationInfo Pagination { get; set; }
        public string SearchString { get; set; }  // Chuỗi tìm kiếm
        public string SortOrder { get; set; }     // Thứ tự sắp xếp
    }

    public class PaginationInfo
    {
        public int PageNumber { get; set; }       // Trang hiện tại
        public int PageSize { get; set; }         // Số lượng mục mỗi trang
        public int TotalItems { get; set; }       // Tổng số mục

        // Tổng số trang
        public int TotalPages => (TotalItems + PageSize - 1) / PageSize;
    }

}