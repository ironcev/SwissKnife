/*
 * Original proposal for this class comes from Marin Rončević (http://github.com/mroncev).
 */
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed.
{
    /// <threadsafety static="true"/>
    public static class ControllerExtensions // TODO-IG: Should this be extension one day? Base class? Both?
    {
        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, ActionResult>> actionExpression) where TController : Controller
        {
            return RedirectToAction(controller, actionExpression, (object)null);
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, Task<ActionResult>>> actionExpression) where TController : Controller
        {
            return RedirectToAction(controller, actionExpression, (object)null);
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, ActionResult>> actionExpression, object routeValues) where TController : Controller
        {
            return RedirectToAction(controller, actionExpression, new RouteValueDictionary(routeValues));
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, Task<ActionResult>>> actionExpression, object routeValues) where TController : Controller
        {
            return RedirectToAction(controller, actionExpression, new RouteValueDictionary(routeValues));
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, ActionResult>> actionExpression, RouteValueDictionary routeValues) where TController : Controller
        {
            return RedirectToActionCore<TController>(controller, ControllerHelper.GetActionName(actionExpression), routeValues);
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, Task<ActionResult>>> actionExpression, RouteValueDictionary routeValues) where TController : Controller
        {
            return RedirectToActionCore<TController>(controller, ControllerHelper.GetActionName(actionExpression), routeValues);
        }

        private static RedirectToRouteResult RedirectToActionCore<TController>(Controller controller, string actionName, RouteValueDictionary routeValues) where TController : Controller
        {
            var controllerName = ControllerHelper.GetControllerName(typeof(TController));

            var methodInfo = typeof(TController).GetMethod("RedirectToAction", BindingFlags.NonPublic | BindingFlags.Instance, null, new[]
                                                                       {
                                                                           typeof (string), typeof (string),
                                                                           typeof (RouteValueDictionary)
                                                                       }, null);

            return (RedirectToRouteResult)methodInfo.Invoke(controller, new object[] { actionName, controllerName, routeValues });
        }
    }
}