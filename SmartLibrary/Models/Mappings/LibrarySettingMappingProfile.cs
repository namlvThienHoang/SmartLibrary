using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.LibrarySetting;

namespace SmartLibrary.Models.Mappings
{
    public class LibrarySettingMappingProfile : Profile
    {
        public LibrarySettingMappingProfile()
        {
            CreateMap<LibrarySetting, LibrarySettingViewModel>().ReverseMap();
        }
    }
}