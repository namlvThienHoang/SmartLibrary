using AutoMapper;
using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.AuditLog;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartLibrary.Services.Implementations
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuditLogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            await _unitOfWork.AuditLogRepository.AddAuditLogAsync(auditLog);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResult<AuditLogViewModel>> GetAuditLogs(string userId, string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            var (AuditLogs, totalCount) = await _unitOfWork.AuditLogRepository.GetAuditLogsAsync(userId, searchString, sortOrder, pageNumber, pageSize);

            var AuditLogViewModels = _mapper.Map<IEnumerable<AuditLogViewModel>>(AuditLogs);

            return new PagedResult<AuditLogViewModel>
            {
                Items = AuditLogViewModels,
                Pagination = new PaginationInfo
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalItems = totalCount
                },
                SearchString = searchString,
                SortOrder = sortOrder
            };
        }

        public async Task<IEnumerable<AuditLogViewModel>> GetAllAuditLogs()
        {
            var AuditLogs = await _unitOfWork.AuditLogRepository.GetAllAuditLogAsync();
            return _mapper.Map<IEnumerable<AuditLogViewModel>>(AuditLogs);
        }

        public async Task<AuditLogViewModel> GetAuditLogById(int id)
        {
            var AuditLog = await _unitOfWork.AuditLogRepository.GetAuditLogByIdAsync(id);
            return _mapper.Map<AuditLogViewModel>(AuditLog);
        }
    }

}
