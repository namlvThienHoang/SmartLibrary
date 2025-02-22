﻿using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SmartLibrary.App_Start;
using SmartLibrary.Models;
using SmartLibrary.Models.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SmartLibrary
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Initialize AutoMapper
            Mapper.Initialize(cfg => cfg.AddProfiles(new[]
            {
                typeof(UserMappingProfile),
                typeof(BookMappingProfile),
                typeof(AuthorMappingProfile),
                typeof(BorrowBookMappingProfile),
                typeof(CategoryMappingProfile),
                typeof(AuditLogMappingProfile),
                typeof(ReservationMappingProfile),
            }));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Đăng ký Autofac
            AutofacConfig.RegisterDependencies();
        }

        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode == 403)
            {
                Response.ClearContent();
                Response.Redirect("~/Home/AccessDenied");
            }
        }
    }
}
