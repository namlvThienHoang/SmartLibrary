using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.Author;

namespace SmartLibrary.Models.Mappings
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile()
        {
            CreateMap<Author, AuthorViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.AuthorId));

            CreateMap<CreateAuthorViewModel, Author>();
            CreateMap<EditAuthorViewModel, Author>()
                .ForMember(des => des.AuthorId, act => act.MapFrom(src => src.Id));
        }
    }
}