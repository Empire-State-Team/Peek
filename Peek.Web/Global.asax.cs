﻿namespace Peek.Web
{
    using System.Data.Entity;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Peek.Data;
    using Peek.Data.Migrations;
    using Peek.Web.App_Start;

    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperConfig.Configure();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PeekDbContext, Configuration>());
        }
    }
}
