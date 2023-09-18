using System;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace DesafioTecnicoCrud
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.EnableFriendlyUrls(); 

            routes.MapPageRoute("Default", "DesafioCrudInfo/", "~/DesafioCrudInfo.aspx");
        }
    }
}
