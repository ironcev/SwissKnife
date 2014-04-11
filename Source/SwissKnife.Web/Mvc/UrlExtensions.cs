using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Mvc // TODO-IG: Write comments and tests for all methods. Properly implement Current() and Action() methods.
{
    public static class UrlExtensions
    {
        public static MvcHtmlString CurrentUrl(this UrlHelper urlHelper, params Func<object, object>[] urlParametersAndDefaultValues)
        {
            return new MvcHtmlString(urlHelper.RouteUrl(ReplaceValuesInRouteData(urlHelper, urlParametersAndDefaultValues)));
        }

        public static MvcHtmlString CurrentAbsoluteUrl(this UrlHelper urlHelper, params Func<object, object>[] urlParametersAndDefaultValues)
        {
            return CurrentAbsoluteUrl(urlHelper, Protocol.Http, urlParametersAndDefaultValues);
        }

        public static MvcHtmlString CurrentAbsoluteUrl(this UrlHelper urlHelper, Protocol protocol, params Func<object, object>[] urlParametersAndDefaultValues)
        {
            string result = UrlHelper.GenerateUrl(null, // routeName
                                      null, // actionName
                                      null, // controllerName
                                      protocol.ToString().ToLowerInvariant(),
                                      null, // hostName,
                                      null, // fragment
                                      //routeValues.ValueOrNull,
                                      ReplaceValuesInRouteData(urlHelper, urlParametersAndDefaultValues),
                                      urlHelper.RouteCollection,
                                      urlHelper.RequestContext,
                                      false // includeImplicitMvcValues
                                      );

            return new MvcHtmlString(result);
        }

        private static RouteValueDictionary ReplaceValuesInRouteData(UrlHelper urlHelper, params Func<object, object>[] urlParametersAndDefaultValues)
        {
            var result = new RouteValueDictionary(urlHelper.RequestContext.RouteData.Values);
            var qs = urlHelper.RequestContext.HttpContext.Request.QueryString;

            if (qs != null)
                foreach (var param in qs.Cast<string>().Where(param => !string.IsNullOrEmpty(qs[param])))
                {
                    result[param] = qs[param];
                }

            if (urlParametersAndDefaultValues != null)
            {
                foreach (var function in urlParametersAndDefaultValues)
                {
                    result[function.Method.GetParameters()[0].Name] = function(null);
                }
            }

            return result;
        }

        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName)
        {
            return urlHelper.RouteAbsoluteUrl(routeName, new RouteValueDictionary(), Protocol.Http);            
        }

        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName, Option<object> routeValues)
        {
            // The constructor of the RouteValueDictionary class accepts null as a valid argument!
            return urlHelper.RouteAbsoluteUrl(routeName, new RouteValueDictionary(routeValues.ValueOrNull), Protocol.Http);            
        }

        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName, Option<object> routeValues, Protocol protocol)
        {
            // The constructor of the RouteValueDictionary class accepts null as a valid argument!
            return urlHelper.RouteAbsoluteUrl(routeName, new RouteValueDictionary(routeValues.ValueOrNull), protocol);
        }

        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName, Option<RouteValueDictionary> routeValues)
        {
            return urlHelper.RouteAbsoluteUrl(routeName, routeValues, Protocol.Http);
        }

        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName, Option<RouteValueDictionary> routeValues, Protocol protocol)
        {
            Argument.IsNotNull(urlHelper, "urlHelper");
            Argument.IsNotNull(urlHelper.RouteCollection, "urlHelper.RouteCollection");
            Argument.IsNotNull(urlHelper.RequestContext, "urlHelper.RequestContext");
            Argument.IsNotNullOrWhitespace(routeName, "routeName");
            // TODO-IG: Check that protocol is in range.

            string result = UrlHelper.GenerateUrl(routeName,
                                                  null, // actionName
                                                  null, // controllerName
                                                  protocol.ToString().ToLowerInvariant(),
                                                  null, // hostName,
                                                  null, // fragment
                                                  routeValues.ValueOrNull,
                                                  urlHelper.RouteCollection,
                                                  urlHelper.RequestContext,
                                                  false // includeImplicitMvcValues
                                                  );

            if (string.IsNullOrWhiteSpace(result))
                throw new InvalidOperationException(string.Format("The absolute URL for the route '{1}' and the protocol '{2}' cannot be generated.{0}" +
                                                                  "The route URL was: '{3}'.{0}" +
                                                                  "The route values were:{0}" +
                                                                  "{4}",
                                                                  Environment.NewLine,
                                                                  routeName,
                                                                  protocol,
                                                                  urlHelper.RouteCollection[routeName] as Route == null ? "<unable to detect route URL>" : ((Route)urlHelper.RouteCollection[routeName]).Url,
                                                                  routeValues.IsNone ? "<null>" :
                                                                                        routeValues.Value.Count <= 0 ? "<empty>" :
                                                                                                          routeValues.Value.Aggregate(
                                                                                                            string.Empty,
                                                                                                            (output, value) => output + string.Format("\t{1}: {2}{0}", Environment.NewLine, value.Key, value.Value))
                                                                  )
                                                );

            return result;
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