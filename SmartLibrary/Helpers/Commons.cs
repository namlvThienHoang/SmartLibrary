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
        }
    }
}