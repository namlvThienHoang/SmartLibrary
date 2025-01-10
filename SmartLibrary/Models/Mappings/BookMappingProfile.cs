using AutoMapper;
using SmartLibrary.Models.EntityModels;
using SmartLibrary.Models.ViewModels.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartLibrary.Models.Mappings
{
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            CreateMap<Book, BookViewModel>();

            CreateMap<CreateBookViewModel, Book>();
            CreateMap<EditBookViewModel, Book>();
        }
    }
}