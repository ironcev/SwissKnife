/*
 * Original proposal for this class comes from Marin Rončević (http://github.com/mroncev).
 */
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed.
{
    /// <threadsafety static="true"/>
    public static class ChildActionExtensions
    {
        public static void RenderAction<TController>(this HtmlHelper htmlHelper, Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            RenderAction(htmlHelper, action, (object)null);
        }

        public static void RenderAction<TController>(this HtmlHelper htmlHelper, Expression<Func<TController, ActionResult>> action, object routeValues) where TController : Controller
        {
            RenderAction(htmlHelper, action, new RouteValueDictionary(routeValues));
        }

        public static void RenderAction<TController>(this HtmlHelper htmlHelper, Expression<Func<TController, ActionResult>> action, RouteValueDictionary routeValues) where TController : Controller
        {
            var actionName = ControllerHelper.GetActionNameFromActionExpression(action.Body);
            var controllerName = ControllerHelper.GetControllerNameFromControllerType(typeof(TController));

            htmlHelper.RenderAction(actionName, controllerName, routeValues);
        }
    }
}