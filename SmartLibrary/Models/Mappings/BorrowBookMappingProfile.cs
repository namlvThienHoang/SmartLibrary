using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.BorrowBook;
using SmartLibrary.Models.ViewModels.User;

namespace SmartLibrary.Models.Mappings
{
    public class BorrowBookMappingProfile : Profile
    {
        public BorrowBookMappingProfile()
        {
            // Mapping from ApplicationUser to UserViewModel
            CreateMap<BorrowTransaction, BorrowBookViewModel>()
                .ForMember(des => des.BookTitle, act => act.MapFrom(src => src.Book.Title))
                .ForMember(des => des.UserName, act => act.MapFrom(src => src.User.FullName));

            CreateMap<CreateBorrowBookViewModel, BorrowTransaction>();

            CreateMap<EditBorrowBookViewModel, BorrowTransaction>();
            CreateMap<BorrowTransaction, EditBorrowBookViewModel>()
                .ForMember(des => des.BookTitle, act => act.MapFrom(src => src.Book.Title))
                .ForMember(des => des.UserName, act => act.MapFrom(src => src.User.FullName));
        }
    }
}