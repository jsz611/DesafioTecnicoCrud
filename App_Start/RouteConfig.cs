using System;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace DesafioTecnicoCrud
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.EnableFriendlyUrls(); // Ative o uso de FriendlyUrls

            // Configurar a rota padrão para a página DesafioCrudInfo.aspx
            routes.MapPageRoute("Default", "", "~/DesafioCrudInfo.aspx");
        }
    }
}
