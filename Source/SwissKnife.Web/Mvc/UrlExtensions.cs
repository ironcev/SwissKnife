using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    public static class UrlExtensions
    {
        public static MvcHtmlString Current(this UrlHelper helper, params Func<object, object>[] urlParametersAndDefaultValues)
        {
            var rvd = new RouteValueDictionary(helper.RequestContext.RouteData.Values);
            var qs = helper.RequestContext.HttpContext.Request.QueryString;

            foreach (var param in qs.Cast<string>().Where(param => !string.IsNullOrEmpty(qs[param])))
            {
                rvd[param] = qs[param];
            }

            if (urlParametersAndDefaultValues != null)
            {
                foreach (var function in urlParametersAndDefaultValues)
                {
                    rvd[function.Method.GetParameters()[0].Name] = function(null);
                }
            }

            var url = helper.RouteUrl(rvd);

            return new MvcHtmlString(url);
        }

        public static string RouteAbsoluteUrl(this UrlHelper helper, string routeName)
        {
            return helper.RouteAbsoluteUrl(routeName, null);
        }

        public static string RouteAbsoluteUrl(this UrlHelper helper, string routeName, object routeValues)
        {
            var url = helper.RequestContext.HttpContext.Request.Url;

            return url.AbsoluteUri.Replace(url.PathAndQuery, string.Empty) + helper.RouteUrl(routeName, routeValues);
        }

        public static string Action<TController>(this UrlHelper helper, Expression<Func<TController, ActionResult>> action) where TController : Controller
        {
            return helper.Action(action, (object)null);
        }

        public static string Action<TController>(this UrlHelper helper, Expression<Func<TController, ActionResult>> action, object routeValues) where TController : Controller
        {
            return helper.Action(action, new RouteValueDictionary(routeValues));
        }

        public static string Action<TController>(this UrlHelper helper, Expression<Func<TController, ActionResult>> action, RouteValueDictionary routeValues) where TController : Controller
        {
            var actionName = ControllerHelper.GetActionNameFromActionExpression(action.Body);
            var controllerName = ControllerHelper.GetControllerNameFromControllerType(typeof(TController));

            return helper.Action(actionName, controllerName, routeValues);
        }
    }
}