using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using System;
using System.Threading.Tasks;

namespace SmartLibrary.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        public AuditLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LogActionAsync(string action, string entity, string details, string userId)
        {
            var auditLog = new AuditLog
            {
                Action = action,
                Entity = entity,
                ActionDate = DateTime.UtcNow,
                Details = details,
                UserId = userId
            };

            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();
        }
    }

}
