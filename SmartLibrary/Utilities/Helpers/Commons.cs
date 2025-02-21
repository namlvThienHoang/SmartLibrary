using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Utilities.Helpers
{
    public static class Commons
    {
        public static class StatusList
        {
            public static List<SelectListItem> GetStatuses()
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Đang mượn", Value = "Borrowed" },
                    new SelectListItem { Text = "Đã trả", Value = "Returned" },
                    new SelectListItem { Text = "Đã hết hạn", Value = "Expired" }
                };
            }
            public static string GetStatusText(string value)
            {
                var status = GetStatuses().FirstOrDefault(s => s.Value == value);
                return status?.Text ?? "Không xác định";
            }
        }

        public static class ReservationStatusList
        {
            public static List<SelectListItem> GetReservationStatuses()
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Text = "Đang chờ", Value = "Pending" },
                    new SelectListItem { Text = "Đã duyệt", Value = "Approved" },
                    new SelectListItem { Text = "Đã hủy", Value = "Cancelled" },
                    new SelectListItem { Text = "Đã hoàn tất", Value = "Completed" }
                };
            }

            public static string GetStatusText(string value)
            {
                var status = GetReservationStatuses().FirstOrDefault(s => s.Value == value);
                return status?.Text ?? "Không xác định";
            }
        }

        public static class ToastType
        {
            public const string Success = "success";
            public const string Error = "error";
            public const string Info = "info";
            public const string Warning = "warning";
        }

        public static class ActionNameText
        {
            public const string Edit = "Edit";
            public const string Create = "Create";
            public const string Delete = "Delete";

            public static string GetActionName(string actionName)
            {
                switch (actionName)
                {
                    case Edit:
                        return "Chỉnh sửa";
                    case Create:
                        return "Thêm mới";
                    case Delete:
                        return "Xóa";
                    default:
                        return actionName;
                }
            }
        }

        public static class ControllerNameText
        {
            public const string AuditLog = "AuditLog";
            public const string Author = "Author";
            public const string Book = "Book";
            public const string BookReservation = "BookReservation";
            public const string BorrowBook = "BorrowBook";
            public const string Category = "Category";
            public const string Notification = "Notification";
            public const string User = "User";

            public static string GetControllerName(string controller)
            {
                switch (controller)
                {
                    case AuditLog:
                        return "hoạt động";
                    case Author:
                        return "tác giả";
                    case Book:
                        return "sách";
                    case BookReservation:
                        return "đặt trước sách";
                    case BorrowBook:
                        return "mượn/trả sách";
                    case Category:
                        return "loại sách";
                    case Notification:
                        return "thông báo";
                    case User:
                        return "người dùng";
                    default:
                        return controller;
                }
            }
        }
    }
}