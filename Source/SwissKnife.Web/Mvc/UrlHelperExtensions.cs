using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Mvc // TODO-IG: Write comments and tests for all methods. Properly implement Current() and Action() methods.
{
    /// <summary>
    /// Contains extension methods to build URLs based on routes, controllers and actions.
    /// </summary>
    public static class UrlHelperExtensions
    {
        public static string ToAbsoluteUrl(this UrlHelper urlHelper, string relativeOrAbsoluteUrl)
        {
            // Try using urlHelper.IsLocalUrl(...)

            Uri result;
            if (!Uri.TryCreate(relativeOrAbsoluteUrl, UriKind.RelativeOrAbsolute, out result))
                throw new ArgumentException("Relative or absolute URL does not represent a valid relative or absolute URL.", "relativeOrAbsoluteUrl");

            return result.IsAbsoluteUri ? relativeOrAbsoluteUrl : new Uri(urlHelper.RequestContext.HttpContext.Request.Url, relativeOrAbsoluteUrl).ToString();
        }

        public static MvcHtmlString CurrentUrl(this UrlHelper urlHelper, params Func<object, object>[] urlParametersAndDefaultValues) // TODO-IG: String instead of MvcHtmlString.
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

        /// <summary>
        /// Generates a fully qualified absolute URL for the specified route, its values and the protocol.
        /// </summary>
        /// <param name="urlHelper"><see cref="UrlHelper"/> used to generate the absolute URL.</param>
        /// <param name="routeName">The name of the <see cref="Route"/> that is used to generate the absolute URL.</param>
        /// <param name="routeValues">The object that contains the parameters for the <see cref="Route"/>.</param>
        /// <param name="protocol"><see cref="Protocol"/> used as URI schema in the absolute URL.</param>
        /// <returns>
        /// Generated fully qualified absolute URL for the specified route.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The URL that is returned by this method ends with the relative URL returned by the <see cref="UrlHelper.RouteUrl(RouteValueDictionary)"/> method.
        /// For example, if the relative URL is '/Home/About' a possible absolute URL could be 'https://localhost/Home/About'.
        /// </para>
        /// <para>
        /// Implicit MVC values "action" and "controller" are not automatically included. 
        /// Even if the route specify their default values, they have to be explicitly included in the <paramref name="routeValues"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="urlHelper"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RouteCollection"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RequestContext"/> is null.<br/>-or-<br/>
        /// <paramref name="routeName"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="routeName"/> is empty or white space.<br/>-or-<br/>
        /// A route with the name <paramref name="routeName"/> does not exist in the <paramref name="urlHelper.RouteCollection"/>.
        /// </exception>
        /// <exception cref="InvalidOperationException">Not all of the parameters defined in the <see cref="Route.Url"/> are provided in the <paramref name="routeValues"/>.</exception>
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
                                                                  "{4}{0}" +
                                                                  "The route data values were:{0}" +
                                                                  "{5}{0}" +
                                                                  "Make sure that the route values are supplied for all the parameters defined in the route URL.",
                                                                  Environment.NewLine,
                                                                  routeName,
                                                                  protocol,
                                                                  urlHelper.RouteCollection[routeName] as Route == null ? "<unable to detect route URL>" : ((Route)urlHelper.RouteCollection[routeName]).Url,
                                                                  RouteValueDictionaryToString(routeValues),
                                                                  RouteValueDictionaryToString(urlHelper.RequestContext.RouteData == null ? null : urlHelper.RequestContext.RouteData.Values)
                                                                  )
                                                );

            return result;
        }

        private static string RouteValueDictionaryToString(Option<RouteValueDictionary> routeValueDictionary)
        {
            return (routeValueDictionary.IsNone ? "<null>" :
                                                  routeValueDictionary.Value.Count <= 0 ? "<empty>" :
                                                        routeValueDictionary.Value.Aggregate(
                                                        string.Empty,
                                                        (output, value) => output + string.Format("\t{1}: {2}{0}", Environment.NewLine, value.Key, value.Value))).TrimEnd();
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