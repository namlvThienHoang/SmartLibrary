using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.Helpers
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
    }
}