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
            CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.BookId))
                .ForMember(dest => dest.Authors, act => act.MapFrom(src =>
                    string.Join(", ", src.BookAuthors.Select(ba => ba.Author.AuthorName))))
                .ForMember(dest => dest.Categories, act => act.MapFrom(src =>
                    string.Join(", ", src.BookCategories.Select(ba => ba.Category.CategoryName))));

            CreateMap<CreateBookViewModel, Book>();
            CreateMap<EditBookViewModel, Book>()
                .ForMember(dest => dest.BookId, act => act.MapFrom(src => src.Id));
            CreateMap<Book, EditBookViewModel>()
                .ForMember(dest => dest.AuthorIds, act => act.MapFrom(src =>
                    src.BookAuthors.Select(ba => ba.Author.AuthorId)))
                .ForMember(dest => dest.CategoryIds, act => act.MapFrom(src =>
                    src.BookCategories.Select(ba => ba.Category.CategoryId)));

            CreateMap<BookReview, BookReviewViewModel>()
                .ForMember(dest => dest.Id, act => act.MapFrom(src => src.BookReviewId));
        }
    }
}