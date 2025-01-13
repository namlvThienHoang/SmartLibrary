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
            CreateMap<ApplicationUser, UserViewModel>(); // If you have roles

            CreateMap<CreateUserViewModel, ApplicationUser>();

            CreateMap<EditUserViewModel, ApplicationUser>();
            CreateMap<ApplicationUser, EditUserViewModel>();
        }
    }
}