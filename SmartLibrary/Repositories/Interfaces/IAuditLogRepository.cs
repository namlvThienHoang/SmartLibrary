using SmartLibrary.Models.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Repositories.Interfaces
{
    public interface IAuditLogRepository
    {
        Task<(IEnumerable<AuditLog> AuditLogs, int TotalCount)> GetAuditLogsAsync(string userId, string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<IEnumerable<AuditLog>> GetAllAuditLogAsync();
        Task<AuditLog> GetAuditLogByIdAsync(int id);
        Task AddAuditLogAsync(AuditLog AuditLog);
    }
}