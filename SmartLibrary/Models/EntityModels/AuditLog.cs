using System;

namespace SmartLibrary.Models.EntityModels
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public string Action { get; set; }
        public string Entity { get; set; }
        public DateTime ActionDate { get; set; }
        public string Details { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }


}