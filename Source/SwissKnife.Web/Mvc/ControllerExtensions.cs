using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    /// <threadsafety static="true"/>
    public static class ControllerExtensions // TODO-IG: Should this be extension one day? Base class? Both?
    {
        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            return RedirectToAction(controller, action, (object)null);
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, Task<ActionResult>>> action) where TController : Controller
        {
            return RedirectToAction(controller, action, (object)null);
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, ActionResult>> action, object routeValues) where TController : Controller
        {
            return RedirectToAction(controller, action, new RouteValueDictionary(routeValues));
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, Task<ActionResult>>> action, object routeValues) where TController : Controller
        {
            return RedirectToAction(controller, action, new RouteValueDictionary(routeValues));
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, ActionResult>> action, RouteValueDictionary routeValues) where TController : Controller
        {
            return RedirectToAction<TController>(controller, action.Body, routeValues);
        }

        public static RedirectToRouteResult RedirectToAction<TController>(this Controller controller, Expression<Func<TController, Task<ActionResult>>> action, RouteValueDictionary routeValues) where TController : Controller
        {
            return RedirectToAction<TController>(controller, action.Body, routeValues);
        }

        private static RedirectToRouteResult RedirectToAction<TController>(Controller controller, Expression actionBody, RouteValueDictionary routeValues) where TController : Controller
        {
            var actionName = ControllerHelper.GetActionNameFromActionExpression(actionBody);
            var controllerName = ControllerHelper.GetControllerNameFromControllerType(typeof(TController));

            var methodInfo = typeof(TController).GetMethod("RedirectToAction", BindingFlags.NonPublic | BindingFlags.Instance, null, new[]
                                                                       {
                                                                           typeof (string), typeof (string),
                                                                           typeof (RouteValueDictionary)
                                                                       }, null);

            return (RedirectToRouteResult)methodInfo.Invoke(controller, new object[] { actionName, controllerName, routeValues });
        }
    }
}