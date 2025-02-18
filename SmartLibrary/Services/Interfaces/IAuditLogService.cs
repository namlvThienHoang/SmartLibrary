using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.AuditLog;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task LogActionAsync(string action, string entity, string details, string userId);
        Task<PagedResult<AuditLogViewModel>> GetAuditLogs(string userId, string searchString, string sortOrder, int pageNumber, int pageSize);
        Task<IEnumerable<AuditLogViewModel>> GetAllAuditLogs();
        Task<AuditLogViewModel> GetAuditLogById(int id);
    }

}
