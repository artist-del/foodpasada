using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Food_Pasada
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Admin",
                url: "Admin/Index/{id}",
                defaults: new {controller="Admin", action="Index", id=UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "QualityTime",
                url: "QualiteaTime/Index/{id}",
                defaults: new {controller="QualiteaTime", action="Index", id =UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "Riders",
                url: "Riders/Index",
                defaults: new {controller="Riders", action="Index"}
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
