using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.ViewModels.AuditLog
{
    public class AuditLogViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Hành động")]
        public string Action { get; set; }

        [Display(Name = "Đối tượng")]
        public string Entity { get; set; }

        [DisplayFormat(DataFormatString = "{0:HH:mm dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Thời gian thực hiện")]
        public DateTime ActionDate { get; set; }

        [Display(Name = "Chi tiết")]
        public string Details { get; set; }

        [Display(Name = "Người thực hiện")]
        public string UserName { get; set; }
    }
}