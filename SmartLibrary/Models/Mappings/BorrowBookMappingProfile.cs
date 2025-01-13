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
            CreateMap<BorrowTransaction, BorrowBookViewModel>(); // If you have roles

            CreateMap<CreateBorrowBookViewModel, BorrowTransaction>();

            CreateMap<EditBorrowBookViewModel, BorrowTransaction>();
            CreateMap<BorrowTransaction, EditBorrowBookViewModel>();
        }
    }
}