using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.AuditLog;
using SmartLibrary.Models.ViewModels.Author;

namespace SmartLibrary.Models.Mappings
{
    public class AuditLogMappingProfile : Profile
    {
        public AuditLogMappingProfile()
        {
            CreateMap<AuditLog, AuditLogViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.AuditLogId))
                .ForMember(des => des.UserName, act => act.MapFrom(src => src.User.UserName));
        }
    }
}