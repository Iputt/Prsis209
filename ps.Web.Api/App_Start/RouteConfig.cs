using ps.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ps.Web.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            var isSwaggerIndex = AppConfig.GetSetting("swagger_index", "true");
            //System.Configuration.ConfigurationManager.AppSettings["swagger_index"];
            if (isSwaggerIndex.ToLower() != "true")
            {
                routes.IgnoreRoute("");
            }
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
