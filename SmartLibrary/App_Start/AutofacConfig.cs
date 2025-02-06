using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.Models;
using SmartLibrary.Models.Mappings;
using SmartLibrary.Repositories.Implementations;
using SmartLibrary.Repositories.Interfaces;
using SmartLibrary.Services.Implementations;
using SmartLibrary.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SmartLibrary.App_Start
{
    public class AutofacConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            // Đăng ký DbContext
            builder.RegisterType<ApplicationDbContext>()
                   .AsSelf()  // Đăng ký là ApplicationDbContext
                   .InstancePerRequest(); // Hoặc có thể là InstancePerLifetimeScope tùy vào phạm vi


            // Đăng ký AutoMapper
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                // Thêm các Profile vào đây
                cfg.AddProfile<UserMappingProfile>();
                cfg.AddProfile<BookMappingProfile>();
                cfg.AddProfile<AuthorMappingProfile>();
                cfg.AddProfile<BorrowBookMappingProfile>();
                cfg.AddProfile<CategoryMappingProfile>();
                cfg.AddProfile<AuditLogMappingProfile>();
                cfg.AddProfile<ReservationMappingProfile>();
            }).CreateMapper()).As<IMapper>().InstancePerRequest();


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

            // Đăng ký Repository
            builder.RegisterType<BookRepository>().As<IBookRepository>().InstancePerRequest();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerRequest();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerRequest();
            builder.RegisterType<AuditLogRepository>().As<IAuditLogRepository>().InstancePerRequest();
            builder.RegisterType<BorrowBookRepository>().As<IBorrowBookRepository>().InstancePerRequest();
            builder.RegisterType<BookReservationRepository>().As<IBookReservationRepository>().InstancePerRequest();

            // Đăng ký Service
            builder.RegisterType<BookService>().As<IBookService>().InstancePerRequest();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerRequest();
            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerRequest();
            builder.RegisterType<AuditLogService>().As<IAuditLogService>().InstancePerRequest();
            builder.RegisterType<BorrowBookService>().As<IBorrowBookService>().InstancePerRequest();
            builder.RegisterType<BookReservationService>().As<IBookReservationService>().InstancePerRequest();

            builder.RegisterType<UserStore<ApplicationUser>>()
                   .As<IUserStore<ApplicationUser>>() // Đăng ký IUserStore<ApplicationUser>
                   .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationUserManager>()
                   .AsSelf()
                   .InstancePerLifetimeScope();


            // Đăng ký các Controller
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}