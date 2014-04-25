/*
 * Original proposal for this class comes from Marin Rončević (http://github.com/mroncev).
 */
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed.
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
            return routes.MapRouteCore<TController>(name, url, ControllerHelper.GetActionName(action), defaults, constraints, routeHandler);
        }

        public static Route MapRoute<TController>(this RouteCollection routes, string name, string url, Expression<Func<TController, Task<ActionResult>>> action, object defaults, object constraints, IRouteHandler routeHandler) where TController : Controller
        {
            return routes.MapRouteCore<TController>(name, url, ControllerHelper.GetActionName(action), defaults, constraints, routeHandler);
        }

        // TODO-IG: This implementation method shouldn't be extension method.
        private static Route MapRouteCore<TController>(this RouteCollection routes, string name, string url, string actionName, object defaults, object constraints, IRouteHandler routeHandler) where TController : Controller
        {
            Argument.IsNotNull(routes, "routes");
            Argument.IsNotNull(url, "url");

            var defaultValues = new RouteValueDictionary(defaults);

            defaultValues["controller"] = ControllerHelper.GetControllerName(typeof(TController));
            defaultValues["action"] = actionName;

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