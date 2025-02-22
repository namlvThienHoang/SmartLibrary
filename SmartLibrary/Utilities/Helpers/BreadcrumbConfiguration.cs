﻿using SmartLibrary.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Utilities.Helpers
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
              #region cài đặt
             {
                "Setting/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Cài đặt", Url = "/Setting", IsActive = true }
                }
            },
             {
                "Setting/Create",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Cài đặt", Url = "/Setting", IsActive = false },
                    new BreadcrumbItem { Title = "Thêm mới", Url = "#", IsActive = true }
                }
            },
            {
                "Setting/Edit",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Cài đặt", Url = "/Setting", IsActive = false },
                    new BreadcrumbItem { Title = "Chỉnh sửa", Url = "#", IsActive = true }
                }
            },
            {
                "Setting/Details",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Cài đặt", Url = "/Setting", IsActive = false },
                    new BreadcrumbItem { Title = "Chi tiết", Url = "#", IsActive = true }
                }
            },
            {
                "Setting/Delete",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Cài đặt", Url = "/Setting", IsActive = false },
                    new BreadcrumbItem { Title = "Xóa cài đặt", Url = "#", IsActive = true }
                }
            },
            #endregion

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

            #region mượn/trả sách
            {
                "BorrowBook/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Mượn/Trả sách", Url = "/BorrowBook", IsActive = true }
                }
            },
            {
                "BorrowBook/Create",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Mượn/Trả sách", Url = "/BorrowBook", IsActive = false },
                    new BreadcrumbItem { Title = "Mượn sách", Url = "#", IsActive = true }
                }
            },
            {
                "BorrowBook/Edit",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Mượn/Trả sách", Url = "/BorrowBook", IsActive = false },
                    new BreadcrumbItem { Title = "Trả sách", Url = "#", IsActive = true }
                }
            },
            {
                "BorrowBook/Details",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Mượn/Trả sách", Url = "/BorrowBook", IsActive = false },
                    new BreadcrumbItem { Title = "Chi tiết mượn/trả sách", Url = "#", IsActive = true }
                }
            },
            {
                "BorrowBook/Delete",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Mượn/Trả sách", Url = "/BorrowBook", IsActive = false },
                    new BreadcrumbItem { Title = "Xóa mượn/trả sách", Url = "#", IsActive = true }
                }
            },
            #endregion

            #region đặt trước sách
            {
                "BookReservation/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Đặt trước sách", Url = "/BookReservation", IsActive = true }
                }
            },
            {
                "BookReservation/Create",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Đặt trước sách", Url = "/BookReservation", IsActive = true },
                    new BreadcrumbItem { Title = "Đặt trước sách", Url = "#", IsActive = true }
                }
            },
            {
                "BookReservation/HuyDatTruoc",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Đặt trước sách", Url = "/BookReservation", IsActive = true },
                    new BreadcrumbItem { Title = "Hủy đặt trước sách", Url = "#", IsActive = true }
                }
            },
            {
                "BookReservation/Details",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Đặt trước sách", Url = "/BookReservation", IsActive = true },
                    new BreadcrumbItem { Title = "Chi tiết đặt trước sách", Url = "#", IsActive = true }
                }
            },
            {
                "BookReservation/Delete",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Đặt trước sách", Url = "/BookReservation", IsActive = true },
                    new BreadcrumbItem { Title = "Xóa đặt trước sách", Url = "#", IsActive = true }
                }
            },
            #endregion

            #region Nhật ký hoạt động
            {
                "AuditLog/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Nhật ký hoạt động", Url = "/AuditLog", IsActive = true }
                }
            },
            {
                "AuditLog/Details",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Nhật ký hoạt động", Url = "/AuditLog", IsActive = false },
                    new BreadcrumbItem { Title = "Chi tiết", Url = "#", IsActive = true }
                }
            },
           
            #endregion

            #region Quản lý thông báo
            {
                "Notification/Index",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Quản lý thông báo", Url = "/Notification", IsActive = true }
                }
            },
            {
                "Notification/Create",
                new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Title = "Trang chủ", Url = "/", IsActive = false },
                    new BreadcrumbItem { Title = "Quản lý thông báo", Url = "/Notification", IsActive = false },
                    new BreadcrumbItem { Title = "Thêm mới", Url = "#", IsActive = true }
                }
            }
           
            #endregion
        };
    }
}