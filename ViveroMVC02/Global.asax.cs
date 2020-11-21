using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ViveroMVC02.App_Start;
using ViveroMVC02.Classes;

namespace ViveroMVC02
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            Mapper.Initialize(cfg => { cfg.AddProfile<MappingProfile>(); });
            //CreateRolesAndSuperUser();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //private void CreateRolesAndSuperUser()
        //{
        //    UsersHelpers.CheckRole("Admin");
        //    UsersHelpers.CheckRole("Cliente");
        //    UsersHelpers.CheckSuperUser();
        //}
    }
}
