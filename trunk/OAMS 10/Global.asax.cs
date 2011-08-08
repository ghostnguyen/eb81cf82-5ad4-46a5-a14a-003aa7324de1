using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using OAMS.Models;
using MvcMiniProfiler.MVCHelpers;
using WebActivator;
using MvcMiniProfiler;

namespace OAMS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            

            ControllerActionRepository actionAuthorizationRepo = new ControllerActionRepository();
            actionAuthorizationRepo.UpdateActionList();

            AppSettingRepository appSettingRepository = new AppSettingRepository();
            appSettingRepository.Reload();

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);

            var copy = ViewEngines.Engines.ToList();
            ViewEngines.Engines.Clear();
            foreach (var item in copy)
            {
                ViewEngines.Engines.Add(new ProfilingViewEngine(item));
            }

            GlobalFilters.Filters.Add(new ProfilingActionFilter());
            //RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal) { MiniProfiler.Start(); } //or any number of other checks, up to you 
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop(); //stop as early as you can, even earlier with MvcMiniProfiler.MiniProfiler.Stop(discardResults: true);
        }
    }
}