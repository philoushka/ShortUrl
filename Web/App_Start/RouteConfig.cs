using System.Web.Mvc;
using System.Web.Routing;

namespace ShortUrl
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Redir","redir/{token}", new { controller = "Url", action = "RedirectForToken" });
            routes.MapRoute("Home", "", new { controller = "Url", action = "Index" });
            routes.MapRoute("About", "About", new { controller = "Meta", action = "About" });
            routes.MapRoute("Contact", "Contact", new { controller = "Meta", action = "Contact" });
            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Url", action = "Index" }
            );
        }
    }
}
