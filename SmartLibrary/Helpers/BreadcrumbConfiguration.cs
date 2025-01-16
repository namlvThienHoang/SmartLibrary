using SmartLibrary.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Helpers
{
    public static class BreadcrumbConfiguration
    {
        public static readonly Dictionary<string, List<BreadcrumbItem>> Breadcrumbs = new Dictionary<string, List<BreadcrumbItem>>()
        {
             {
                "Home/About",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Giới thiệu", Url = "/Home/About", IsActive = true }
                }
            },
             {
                "Home/Contact",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Liên hệ", Url = "/Home/Contact", IsActive = true }
                }
            },
             {
                "Setting/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Cài đặt", Url = "/Setting", IsActive = true }
                }
            },
            #region sách
            {
                "Book/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Sách", Url = "/Book", IsActive = true }
                }
            },
            {
                "Book/Create",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Sách", Url = "/Book", IsActive = false },
                    new BreadcrumbItem { Title = "Thêm mới sách", Url = "#", IsActive = true }
                }
            },
            {
                "Book/Edit",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Sách", Url = "/Book", IsActive = false },
                    new BreadcrumbItem { Title = "Chỉnh sửa sách", Url = "#", IsActive = true }
                }
            },
            {
                "Book/Details",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Sách", Url = "/Book", IsActive = false },
                    new BreadcrumbItem { Title = "Chi tiết sách", Url = "#", IsActive = true }
                }
            },
            {
                "Book/Delete",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Sách", Url = "/Book", IsActive = false },
                    new BreadcrumbItem { Title = "Xóa sách", Url = "#", IsActive = true }
                }
            },
            #endregion

            #region loại sách
            {
                "Category/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Loại sách", Url = "/Category", IsActive = true }
                }
            },
            {
                "Category/Create",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Loại sách", Url = "/Category", IsActive = false },
                    new BreadcrumbItem { Title = "Thêm mới loại sách", Url = "#", IsActive = true }
                }
            },
            {
                "Category/Edit",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Loại sách", Url = "/Category", IsActive = false },
                    new BreadcrumbItem { Title = "Chỉnh sửa loại sách", Url = "#", IsActive = true }
                }
            },
            {
                "Category/Details",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Loại sách", Url = "/Category", IsActive = false },
                    new BreadcrumbItem { Title = "Chi tiết loại sách", Url = "#", IsActive = true }
                }
            },
            {
                "Category/Delete",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Loại sách", Url = "/Category", IsActive = false },
                    new BreadcrumbItem { Title = "Xóa loại sách", Url = "#", IsActive = true }
                }
            },
            #endregion

            #region tác giả
            {
                "Author/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Tác giả", Url = "/Author", IsActive = true }
                }
            },
            {
                "Author/Create",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Tác giả", Url = "/Author", IsActive = false },
                    new BreadcrumbItem { Title = "Thêm mới tác giả", Url = "#", IsActive = true }
                }
            },
            {
                "Author/Edit",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Tác giả", Url = "/Author", IsActive = false },
                    new BreadcrumbItem { Title = "Chỉnh sửa tác giả", Url = "#", IsActive = true }
                }
            },
            {
                "Author/Details",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Tác giả", Url = "/Author", IsActive = false },
                    new BreadcrumbItem { Title = "Chi tiết tác giả", Url = "#", IsActive = true }
                }
            },
            {
                "Author/Delete",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Tác giả", Url = "/Author", IsActive = false },
                    new BreadcrumbItem { Title = "Xóa tác giả", Url = "#", IsActive = true }
                }
            },
            #endregion

            #region người dùng
            {
                "User/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Người dùng", Url = "/User", IsActive = true }
                }
            },
            {
                "User/Create",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Người dùng", Url = "/User", IsActive = false },
                    new BreadcrumbItem { Title = "Thêm mới người dùng", Url = "#", IsActive = true }
                }
            },
            {
                "User/Edit",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Người dùng", Url = "/User", IsActive = false },
                    new BreadcrumbItem { Title = "Chỉnh sửa người dùng", Url = "#", IsActive = true }
                }
            },
            {
                "User/Details",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Người dùng", Url = "/User", IsActive = false },
                    new BreadcrumbItem { Title = "Chi tiết người dùng", Url = "#", IsActive = true }
                }
            },
            {
                "User/Delete",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Người dùng", Url = "/User", IsActive = false },
                    new BreadcrumbItem { Title = "Xóa người dùng", Url = "#", IsActive = true }
                }
            },
            #endregion
        };
    }
}