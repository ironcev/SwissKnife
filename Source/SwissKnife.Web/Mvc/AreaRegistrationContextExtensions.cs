/*
 * Original proposal for this class comes from Marin Rončević (http://github.com/mroncev).
 */
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace SwissKnife.Web.Mvc
{
    /// <threadsafety static="true"/>
    public static class AreaRegistrationContextExtensions // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed.
    {
        public static Route MapRoute<TController>(this AreaRegistrationContext context, string name, string url, Expression<Func<TController, ActionResult>> actionExpression)
            where TController : Controller
        {
            return context.MapRoute<TController>(name, url, actionExpression, null);
        }

        public static Route MapRoute<TController>(this AreaRegistrationContext context, string name, string url, Expression<Func<TController, ActionResult>> actionExpression, object defaults)
            where TController : Controller
        {
            return context.MapRoute<TController>(name, url, actionExpression, defaults, null);
        }

        public static Route MapRoute<TController>(this AreaRegistrationContext context, string name, string url, Expression<Func<TController, ActionResult>> actionExpression, object defaults, object constraints)
            where TController : Controller
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }
            if (string.IsNullOrEmpty("name"))
            {
                throw new ArgumentNullException("name");
            }

            RouteValueDictionary defaultValues = new RouteValueDictionary(defaults);


            // Add default controller to the route values dictionary.
            defaultValues["controller"] = ControllerHelper.GetControllerName(typeof(TController));

            // Add default action to the route values dictionary.
            defaultValues["action"] = ControllerHelper.GetActionName(actionExpression);

            // Hack: We cannot pass RouteValueDictionary to the MapRoute method of AreaRegistrationContext. We will set it up later directly on the newly created route.
            Route route = context.MapRoute(name, url, null, constraints, new[] { typeof(TController).Namespace });
            route.Defaults = defaultValues;
            return route;
        }
    }
}