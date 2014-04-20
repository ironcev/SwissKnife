using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    /// <threadsafety static="true"/>
    public static class RouteCollectionExtensions
    {
        public static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            return routes.MapRoute(name, url, action, null);
        }

        public static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression<Func<TController, Task<ActionResult>>> action) where TController : Controller
        {
            return routes.MapRoute(name, url, action, null);
        }

        public static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression<Func<TController, ActionResult>> action, object defaults) where TController : Controller
        {
            return routes.MapRoute(name, url, action, defaults, null);
        }

        public static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression<Func<TController, Task<ActionResult>>> action, object defaults) where TController : Controller
        {
            return routes.MapRoute(name, url, action, defaults, null);
        }

        public static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression<Func<TController, ActionResult>> action, object defaults, object constraints) where TController : Controller
        {
            return routes.MapRoute(name, url, action, defaults, constraints, null);
        }

        public static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression<Func<TController, Task<ActionResult>>> action, object defaults, object constraints) where TController : Controller
        {
            return routes.MapRoute(name, url, action, defaults, constraints, null);
        }

        public static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression<Func<TController, ActionResult>> action, object defaults, object constraints, IRouteHandler routeHandler) where TController : Controller
        {
            return routes.MapRoute<TController>(name, url, action.Body, defaults, constraints, routeHandler);
        }

        public static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression<Func<TController, Task<ActionResult>>> action, object defaults, object constraints, IRouteHandler routeHandler) where TController : Controller
        {
            return routes.MapRoute<TController>(name, url, action.Body, defaults, constraints, routeHandler);
        }

        private static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression actionBody, object defaults, object constraints, IRouteHandler routeHandler) where TController : Controller
        {
            Argument.IsNotNull(routes, "routes");
            Argument.IsNotNull(url, "url");

            var defaultValues = new RouteValueDictionary(defaults);

            defaultValues["controller"] = ControllerHelper.GetControllerNameFromControllerType(typeof(TController));
            defaultValues["action"] = ControllerHelper.GetActionNameFromActionExpression(actionBody);

            routeHandler = routeHandler ?? new MvcRouteHandler();

            var route = new Route(url, routeHandler)
            {
                Defaults = defaultValues,
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            route.DataTokens["Namespaces"] = new[] { typeof(TController).Namespace };

            routes.Add(name, route);

            return route;
        }
    }
}