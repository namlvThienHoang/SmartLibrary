using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Repositories.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Implementations
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public AuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<AuditLog> AuditLogs, int TotalCount)> GetAuditLogsAsync(string userId, string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var query = _context.AuditLogs.AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(b => b.User.Email.Contains(searchString) 
                || b.Action.Contains(searchString) 
                || b.Details.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(b => b.UserId == userId);
            }

            int totalCount = await query.CountAsync();

            // Sắp xếp
            switch (sortOrder)
            {
                case "actionDate":
                    query = query.OrderBy(b => b.ActionDate);
                    break;
                case "action":
                    query = query.OrderBy(b => b.Action);
                    break;
                case "action_desc":
                    query = query.OrderByDescending(b => b.ActionDate);
                    break;
                default:
                    query = query.OrderByDescending(b => b.ActionDate);
                    break;
            }

            // Phân trang
            var AuditLogs = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (AuditLogs, totalCount);
        }

        public async Task<IEnumerable<AuditLog>> GetAllAuditLogAsync()
        {
            return await _context.AuditLogs.ToListAsync();
        }

        public async Task<AuditLog> GetAuditLogByIdAsync(int id)
        {
            return await _context.AuditLogs.FindAsync(id);
        }

        public async Task AddAuditLogAsync(AuditLog AuditLog)
        {
            _context.AuditLogs.Add(AuditLog);
        }
    }
}