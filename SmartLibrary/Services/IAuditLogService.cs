using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Services
{
    public interface IAuditLogService
    {
        Task LogActionAsync(string action, string entity, string details, string userId);
    }

}
