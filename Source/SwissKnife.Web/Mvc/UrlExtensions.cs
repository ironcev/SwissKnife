using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    public static class UrlExtensions
    {
        public static MvcHtmlString Current(this UrlHelper helper, params object[] routeValueDictionaryKeysAndValues)
        {
            Argument.IsValid((routeValueDictionaryKeysAndValues != null && routeValueDictionaryKeysAndValues.Length % 2 == 0) || routeValueDictionaryKeysAndValues == null, @"Current() route value dictionary keys and values must have even number of objects. First object represents route value dictionary key, second is corresponding value and so on.", "routeValueDictionaryKeysAndValues");
            Argument.IsValid((routeValueDictionaryKeysAndValues != null && !routeValueDictionaryKeysAndValues.Where((item, index) => (item == null || string.IsNullOrWhiteSpace(item.ToString())) && index % 2 == 0).Any()) || routeValueDictionaryKeysAndValues == null, @"Current() route value dictionary keys must not be null or whitespace.", "routeValueDictionaryKeysAndValues");

            var rvd = new RouteValueDictionary(helper.RequestContext.RouteData.Values);
            var qs = helper.RequestContext.HttpContext.Request.QueryString;

            foreach (var param in qs.Cast<string>().Where(param => !string.IsNullOrEmpty(qs[param])))
            {
                rvd[param] = qs[param];
            }

            if (routeValueDictionaryKeysAndValues != null)
            {
                for (var i = 0; i < routeValueDictionaryKeysAndValues.Length - 1; i += 2)
                {
                    rvd[routeValueDictionaryKeysAndValues[i].ToString()] = routeValueDictionaryKeysAndValues[i + 1];
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
            var actionName = ExtensionsHelper.GetActionNameFromExpression(action.Body);
            var controllerName = ExtensionsHelper.GetControllerNameFromType(typeof(TController));

            return helper.Action(actionName, controllerName, routeValues);
        }
    }
}