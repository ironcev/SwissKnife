/*
 * Original proposal for this class comes from Marin Rončević (http://github.com/mroncev).
 */
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SwissKnife.Web.Mvc
{
    /// <preliminary/>
    /// <threadsafety static="true"/>
    public static class ChildActionExtensions // TODO-IG: This type is in development. Review and refactoring is needed.
    {
        public static void RenderAction<TController>(this HtmlHelper htmlHelper, Expression<Func<TController, ActionResult>> actionExpression) where TController : Controller
        {
            RenderAction(htmlHelper, actionExpression, (object)null);
        }

        public static void RenderAction<TController>(this HtmlHelper htmlHelper, Expression<Func<TController, ActionResult>> actionExpression, object routeValues) where TController : Controller
        {
            RenderAction(htmlHelper, actionExpression, new RouteValueDictionary(routeValues));
        }

        public static void RenderAction<TController>(this HtmlHelper htmlHelper, Expression<Func<TController, ActionResult>> actionExpression, RouteValueDictionary routeValues) where TController : Controller
        {
            var actionName = ControllerHelper.GetActionName(actionExpression);
            var controllerName = ControllerHelper.GetControllerName(typeof(TController));

            htmlHelper.RenderAction(actionName, controllerName, routeValues);
        }
    }
}