using Cibertec.WebApi.App_Start;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Cibertec.WebApi.Startup))]

namespace Cibertec.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            DIConfig.ConfigureInjector(config);
            TokenConfig.ConfigureAuth(app, config);
            RouteConfig.Register(config);
            app.UseWebApi(config);
        }
    }
}