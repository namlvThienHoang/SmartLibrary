using AutoMapper;
using SmartLibrary.Models.ViewModels;
using SmartLibrary.Models.ViewModels.User;
using System.Linq;

namespace SmartLibrary.Models.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            // Mapping from ApplicationUser to UserViewModel
            CreateMap<ApplicationUser, UserViewModel>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());

            CreateMap<CreateUserViewModel, ApplicationUser>()
                     .ForMember(dest => dest.Roles, opt => opt.Ignore());

            CreateMap<EditUserViewModel, ApplicationUser>()
                 .ForMember(dest => dest.Roles, opt => opt.Ignore());
            CreateMap<ApplicationUser, EditUserViewModel>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}