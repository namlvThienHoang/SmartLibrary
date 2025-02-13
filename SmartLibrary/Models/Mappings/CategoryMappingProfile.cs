using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.Category;

namespace SmartLibrary.Models.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.CategoryId))
                .ForMember(des => des.Name, act => act.MapFrom(src => src.CategoryName));
            CreateMap<CreateCategoryViewModel, Category>()
                .ForMember(des => des.CategoryName, act => act.MapFrom(src => src.Name));
            CreateMap<EditCategoryViewModel, Category>()
                .ForMember(des => des.CategoryId, act => act.MapFrom(src => src.Id))
                .ForMember(des => des.CategoryName, act => act.MapFrom(src => src.Name));

            CreateMap<Category, EditCategoryViewModel>()
                .ForMember(des => des.Id, act => act.MapFrom(src => src.CategoryId))
                .ForMember(des => des.Name, act => act.MapFrom(src => src.CategoryName));
        }
    }
}