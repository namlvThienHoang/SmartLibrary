using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.Mappings
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<Reservation, ReservationViewModel>()
                .ForMember(des => des.BookTitle, act => act.MapFrom(src => src.Book.Title))
                .ForMember(des => des.UserName, act => act.MapFrom(src => src.User.FullName));

            CreateMap<CreateReservationViewModel, Reservation>();

            CreateMap<EditReservationViewModel, Reservation>();
            CreateMap<Reservation, EditReservationViewModel>()
                .ForMember(des => des.BookTitle, act => act.MapFrom(src => src.Book.Title))
                .ForMember(des => des.UserName, act => act.MapFrom(src => src.User.FullName));
        }
    }
}