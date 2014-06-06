using System.Web.Mvc;
using System.Web.Routing;

namespace SwissKnife.Experiments.QueryStringAndIEnumerablePropertiesInAspDotNetMvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Root", "", new { controller = "Example", action = "Get" });
            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Example", action = "Get" });
        }
    }
}

