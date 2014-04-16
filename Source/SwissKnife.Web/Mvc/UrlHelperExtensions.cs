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

        /// <summary>
        /// Gets the current URL as defined in the request context.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If there is a parameter in the current URL query string that has the same name as one of the current route parameters, the existing parameter in the current route will be replaced by the value of the parameter in the query string.
        /// </para>
        /// </remarks>
        /// <param name="urlHelper"><see cref="UrlHelper"/> used to get the current URL.</param>
        /// <returns>
        /// Current URL as defined in the request context.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="urlHelper"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RequestContext"/> is null.<br/>-or-<br/>
        /// </exception>
        public static string CurrentUrl(this UrlHelper urlHelper)
        {
            return CurrentUrl(urlHelper, new Func<object, object>[0]);
        }

        /// <summary>
        /// Gets the current URL as defined in the request context and replaces existing route parameters with provided new route parameters.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If a parameter defined in the <paramref name="newRouteParameters"/> is one of the current route parameters, the existing parameter in the current route will be replaced by that parameter value.<br/>
        /// If a parameter defined in the <paramref name="newRouteParameters"/> is not one of the current route parameters, it will be added to the query string.<br/>
        /// If there is a parameter in the current URL query string that has the same name as one of the current route parameters, the existing parameter in the current route will be replaced by the value of the parameter in the query string.
        /// </para>
        /// <para>
        /// For example, let us assume that the current route is defined as "{language}/{year}" and that the current URL is "/en-US/1999".<br/>
        /// The following call:<br/>
        /// <code>
        /// urlHelper.CurrentUrl(language => "hr-HR", year => 2000, p1 => "firstParameter", p2 => 1, p3 => 1.23);
        /// </code>
        /// will return "/hr-HR/2000?p1=firstParameter&amp;p2=1&amp;p3=1.23".
        /// </para>        
        /// <note type="caution">
        /// If any of the <paramref name="newRouteParameters"/> throws an exception, that exception will be propagated to the caller.
        /// </note>
        /// </remarks>
        /// <param name="urlHelper"><see cref="UrlHelper"/> used to get the current URL.</param>
        /// <param name="newRouteParameters">URL parameters and their new values defined as lambda expressions.</param>
        /// <returns>
        /// Current URL as defined in the request context with original URL parameters replaced by <paramref name="newRouteParameters"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="urlHelper"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RequestContext"/> is null.<br/>-or-<br/>
        /// <paramref name="newRouteParameters"/> is null.
        /// </exception>
        public static string CurrentUrl(this UrlHelper urlHelper, params Func<object, object>[] newRouteParameters)
        {
            Argument.IsNotNull(urlHelper, "urlHelper");
            Argument.IsNotNull(urlHelper.RequestContext, "urlHelper.RequestContext");
            Argument.IsNotNull(newRouteParameters, "newRouteParameters");

            return urlHelper.RouteUrl(ReplaceValuesInRouteData(urlHelper, newRouteParameters)) ?? string.Empty;
        }

        public static MvcHtmlString CurrentAbsoluteUrl(this UrlHelper urlHelper, params Func<object, object>[] newRouteParameters)
        {
            return CurrentAbsoluteUrl(urlHelper, Protocol.Http, newRouteParameters);
        }

        public static MvcHtmlString CurrentAbsoluteUrl(this UrlHelper urlHelper, Protocol protocol, params Func<object, object>[] newRouteParameters)
        {
            string result = UrlHelper.GenerateUrl(null, // routeName
                                      null, // actionName
                                      null, // controllerName
                                      protocol.ToString().ToLowerInvariant(),
                                      null, // hostName,
                                      null, // fragment
                                      //routeValues.ValueOrNull,
                                      ReplaceValuesInRouteData(urlHelper, newRouteParameters),
                                      urlHelper.RouteCollection,
                                      urlHelper.RequestContext,
                                      false // includeImplicitMvcValues
                                      );

            return new MvcHtmlString(result);
        }

        private static RouteValueDictionary ReplaceValuesInRouteData(UrlHelper urlHelper, params Func<object, object>[] newRouteParameters)
        {
            var result = new RouteValueDictionary(urlHelper.RequestContext.RouteData.Values);
            var queryString = urlHelper.RequestContext.HttpContext.Request.QueryString;

            // Replace existing parameters using the values from the query string if there are parameters with the same name.
            if (queryString != null)
                foreach (var param in queryString.Cast<string>().Where(param => !string.IsNullOrEmpty(queryString[param])))
                    result[param] = queryString[param];

            // Replace existing parameters with new ones.
            foreach (var function in newRouteParameters)
                result[function.Method.GetParameters()[0].Name] = function(null);

            return result;
        }

        /// <summary>
        /// Generates a fully qualified absolute URL for the specified parameterless route and the HTTP protocol.
        /// </summary>
        /// <param name="urlHelper"><see cref="UrlHelper"/> used to generate the absolute URL.</param>
        /// <param name="routeName">The name of the <see cref="Route"/> that is used to generate the absolute URL.</param>
        /// <returns>
        /// Generated fully qualified absolute URL for the specified route.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The URL that is returned by this method ends with the relative URL returned by the <see cref="UrlHelper.RouteUrl(string)"/> method.
        /// For example, if the relative URL is '/Home/About' a possible absolute URL could be 'https://localhost/Home/About'.
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
        /// <exception cref="InvalidOperationException">The route has parameters but they are not provided.</exception>
        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName)
        {
            return urlHelper.RouteAbsoluteUrl(routeName, new RouteValueDictionary(), Protocol.Http);            
        }

        /// <summary>
        /// Generates a fully qualified absolute URL for the specified route, its values and the HTTP protocol.
        /// </summary>
        /// <param name="urlHelper"><see cref="UrlHelper"/> used to generate the absolute URL.</param>
        /// <param name="routeName">The name of the <see cref="Route"/> that is used to generate the absolute URL.</param>
        /// <param name="routeValues">The object that contains the parameters for the <see cref="Route"/>.</param>
        /// <returns>
        /// Generated fully qualified absolute URL for the specified route.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The URL that is returned by this method ends with the relative URL returned by the <see cref="UrlHelper.RouteUrl(string, RouteValueDictionary)"/> method.
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
        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName, Option<object> routeValues)
        {
            // The constructor of the RouteValueDictionary class accepts null as a valid argument.
            return urlHelper.RouteAbsoluteUrl(routeName, new RouteValueDictionary(routeValues.ValueOrNull), Protocol.Http);            
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
        /// The URL that is returned by this method ends with the relative URL returned by the <see cref="UrlHelper.RouteUrl(string, object)"/> method.
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
        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName, Option<object> routeValues, Protocol protocol)
        {
            // The constructor of the RouteValueDictionary class accepts null as a valid argument.
            return urlHelper.RouteAbsoluteUrl(routeName, new RouteValueDictionary(routeValues.ValueOrNull), protocol);
        }

        /// <summary>
        /// Generates a fully qualified absolute URL for the specified route, its values and the HTTP protocol.
        /// </summary>
        /// <param name="urlHelper"><see cref="UrlHelper"/> used to generate the absolute URL.</param>
        /// <param name="routeName">The name of the <see cref="Route"/> that is used to generate the absolute URL.</param>
        /// <param name="routeValues">The object that contains the parameters for the <see cref="Route"/>.</param>
        /// <returns>
        /// Generated fully qualified absolute URL for the specified route.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The URL that is returned by this method ends with the relative URL returned by the <see cref="UrlHelper.RouteUrl(string, RouteValueDictionary)"/> method.
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
        /// The URL that is returned by this method ends with the relative URL returned by the <see cref="UrlHelper.RouteUrl(string, RouteValueDictionary)"/> method.
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