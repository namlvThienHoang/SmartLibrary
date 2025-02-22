﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartLibrary.Startup))]
namespace SmartLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
