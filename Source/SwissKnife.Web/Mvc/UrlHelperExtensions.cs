/*
 * Original proposal for this class comes from Marin Rončević (http://github.com/mroncev).
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Mvc
{
    /// <summary>
    /// Contains extension methods to build URLs based on routes, controllers and actions.
    /// </summary>
    /// <threadsafety static="true"/>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Converts a relative or absolute URL to an absolute URL presented in the unescaped canonical representation.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the <paramref name="relativeOrAbsoluteUrl"/> is already an absolute URL it unescaped canonical representation will be returned.<br/>
        /// For example for the absolute URL <a href="http://www.thehumbleprogrammer.com/about">HTTP://WWW.TheHumbleProgramer.COM/About</a> the
        /// <a href="http://www.thehumbleprogrammer.com/about">http://www.thehumbleprogrammer.com/About</a> will be return.
        /// </para>
        /// <para>
        /// If the <paramref name="relativeOrAbsoluteUrl"/> is a relative URL it will be appended to the authority (see <see cref="Uri.Authority"/>) of the current absolute URL.<br/>
        /// For example for the current URL <i>http://Localhost/current/url/</i> and the relative URL <i>relative/url</i>
        /// <i>http://localhost/relative/url</i> will be return. (Because <i>http://Localhost/</i> is the authority of the current URL.)
        /// </para>
        /// <para>
        /// The relative URL can both start with '/' (or '\') or not. In both cases, the relative URL will be appended to the authority of the current absolute URL.<br/>
        /// For example for the current URL <i>http://Localhost/current/url/</i> all of the following relative URL-s <i>relative/url</i>, <i><b>/</b>relative/url</i>, and <i><b>\</b>relative/url</i>
        /// will produce the same output: <i>http://localhost/relative/url</i>.
        /// </para>
        /// </remarks>
        /// <param name="urlHelper">The url helper used to get the current URL.</param>
        /// <param name="relativeOrAbsoluteUrl">Relative or absolute URL to convert to an absolute URL.</param>
        /// <returns>
        /// An absolute URL presented in the unescaped canonical representation.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="urlHelper"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RequestContext"/> is null.<br/>-or-<br/>
        /// <paramref name="relativeOrAbsoluteUrl"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="relativeOrAbsoluteUrl"/> does not represent a valid relative or absolute URL.</exception>
        /// <exception cref="InvalidOperationException">The current HTTP request has no URL defined.</exception>
        public static string ToAbsoluteUrl(this UrlHelper urlHelper, string relativeOrAbsoluteUrl)
        {
            Argument.IsNotNull(urlHelper, "urlHelper");
            Argument.IsNotNull(urlHelper.RequestContext, "urlHelper.RequestContext");
            Argument.IsNotNullOrWhitespace(relativeOrAbsoluteUrl, "relativeOrAbsoluteUrl");

            Uri relativeOrAbsoluteUrlAsUri;
            if (!Uri.TryCreate(relativeOrAbsoluteUrl, UriKind.RelativeOrAbsolute, out relativeOrAbsoluteUrlAsUri))
                throw new ArgumentException(string.Format("Relative or absolute URL does not represent a valid relative or absolute URL.{0}" +
                                                          "The relative or absolute URL was: '{1}'.",
                                                          Environment.NewLine,
                                                          relativeOrAbsoluteUrl),
                                           "relativeOrAbsoluteUrl");

            if (relativeOrAbsoluteUrlAsUri.IsAbsoluteUri) return relativeOrAbsoluteUrlAsUri.ToString();


            // We have a relative URL and we want to append it to the authority of the request URL.
            Uri requestUrl = urlHelper.RequestContext.HttpContext.Request.Url;

            Operation.IsValid(requestUrl != null, "The HTTP request has no URL defined.");

            // We want to append the relative URL to the authority no matter if it starts with '/' (or '\') or if it doesn't.
            // The Uri class constructor will do that automatically if the relative URL starts with '/' (or '\').
            // Therefore, we will form the relative URL to always start with with '/' (or '\').
            string relativeUrl = relativeOrAbsoluteUrlAsUri.ToString();
            if (!(relativeUrl.StartsWith("/") || relativeUrl.StartsWith("\\")))
                relativeUrl = '/' + relativeUrl;

            // We checked that the requestUrl is not null.
            // ReSharper disable AssignNullToNotNullAttribute
            return new Uri(requestUrl, relativeUrl).ToString();
            // ReSharper restore AssignNullToNotNullAttribute
        }

        /// <summary>
        /// Gets the current URL as defined in the request context.
        /// </summary>
        /// <param name="urlHelper">The url helper used to get the current URL.</param>
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

        #pragma warning disable 1573 // Parameter does not have XML comment.
        // TODO-IG: Add an example to each paragraph that describes the detailed method semantics.
        /// <summary>
        /// Generates a new URL by taking the current URL as defined in the request context and replacing the existing route and query string parameters with provided new parameters.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If a parameter defined in the <paramref name="newRouteAndQueryStringParameters"/> is one of the route parameters,
        /// the existing parameter in the current route will be replaced by that parameter value.
        /// </para>
        /// <para>
        /// TODO-IG Route parameter is null -> ArgumentException: The parameter '{0}' was null. A route or query string parameter cannot be set to null. If you want to remove the parameter from the route parameter collection or from the query string set it to 'UrlParameter.Optional'.
        /// </para>
        /// <para>
        /// TODO-IG Route parameter is "" -> ArgumentException: The route parameter '{0}' was empty string. A route parameter cannot be set to empty string. If you want to remove the parameter from the route parameter collection set it to 'UrlParameter.Optional'.
        /// </para>
        /// <para>
        /// TODO-IG Route parameter is UrlParameter.Optional -> Removed from the route parameters
        /// </para>
        /// <para>
        /// If a parameter defined in the <paramref name="newRouteAndQueryStringParameters"/> is not one of the current route parameters,
        /// it will either be added to the query string or an existing parameter in the query string will be replaced by that parameter value.
        /// </para>
        /// <para>
        /// If a parameter exists both as a route parameter and as a query string parameter, query string parameters have precedence.
        /// </para>
        /// <para>
        /// TODO-IG Query string parameter is null -> ArgumentException: The query string parameter '{0}' was null. A query string parameter cannot be set to null. If you want to remove the parameter from the query string set it to 'UrlParameter.Optional'.
        /// </para>
        /// <para>
        /// TODO-IG Query string parameter is "" -> ArgumentException: The query string parameter '{0}' was empty string. A query string parameter cannot be set to empty string. If you want to remove the parameter from the query string set it to 'UrlParameter.Optional'.
        ///         DECISION AND NOTE: NameValueCollection supports empty strings. Parameters are removed from the query string. We wanted to get consistent API and uniform treatment of route and query parameters. Problem s with setting route parameters to empty string.
        /// </para>
        /// <para>
        /// TODO-IG Query string parameter is UrlParameter.Optional -> Removed from the query string
        /// </para>
        /// <para>
        /// TODO-IG Query string IEnumerable parameter -> replaces all existing parameters with same name.
        ///         DECISION: Replaces and not extends. Consistent API because non-enumerable parameters also replace.
        /// </para>
        /// <para>
        /// TODO-IG Query string IEnumerable parameter contains UrlParameter.Optional -> they are simply ignored.
        /// </para>
        /// <para>
        /// TODO-IG Query string IEnumerable parameter contains null -> ArgumentException: The item on the zero-index {0} of the enumerable query string parameter '{1}' was null. An enumerable item cannot be null.
        /// </para>
        /// <para>
        /// TODO-IG Query string IEnumerable parameter contains empty string -> ArgumentException: The item on the zero-index {0} of the enumerable query string parameter '{1}' was empty string. An enumerable item cannot be empty string.
        /// </para>
        /// <para>
        /// TODO-IG Route IEnumerable parameter. What to do?
        ///         Option #1: If no route parameter with that name add to the query string; otherwise throw InvalidOperationException
        ///         Option #2: Always add IEnumerable parameter to the query string.
        ///         DECISION: #1 - easier to comprehend; we don't support *parameter parameters in the route.
        /// </para>
        /// <para>
        /// TODO-IG Parameter specified more than once (p1 => 1, p1 => 2) -> ArgumentException: Each new route and query string parameter can be specified only once. The following parameters were specified more than once: 
        /// </para>
        /// <para>
        /// TODO-IG Non-enumerable parameter replaces enumerable parameters (p=1&p=2&p=3; p => 0 -> p=0)
        /// </para>
        /// <para>
        /// TODO-IG Parameters are converted to string by calling ToString().
        /// </para>
        /// <para>
        /// TODO-IG NameValueCollection and RouteCollection have no special treatment. They are treated as any other collection of objects.
        /// </para>
        /// <para>
        /// For example, let us assume that the current route is defined as "<i>{language}/{year}</i>" and that the current URL is "<i>/en-US/1999</i>".<br/>
        /// The following call:<br/>
        /// <code>
        /// urlHelper.CurrentUrl(language => "hr-HR", year => 2000, p1 => "firstParameter", p2 => 1, p3 => 1.23);
        /// </code>
        /// will return "<i>/hr-HR/2000?p1=firstParameter&amp;p2=1&amp;p3=1.23</i>".
        /// </para>
        /// <para>
        /// Parameter names are case-insensitive.
        /// </para>
        /// <note type="caution">
        /// If any of the <paramref name="newRouteAndQueryStringParameters"/> throws an exception, that exception will be propagated to the caller.
        /// </note>
        /// </remarks>
        /// <inheritdoc cref="CurrentUrl(UrlHelper)" source="param[@name='urlHelper']" />
        /// <param name="newRouteAndQueryStringParameters">URL parameters and their new values defined as lambda expressions.</param>
        /// <returns>
        /// Current URL as defined in the request context with original URL parameters replaced by <paramref name="newRouteAndQueryStringParameters"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="urlHelper"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RequestContext"/> is null.<br/>-or-<br/>
        /// <paramref name="newRouteAndQueryStringParameters"/> is null.
        /// </exception>
        public static string CurrentUrl(this UrlHelper urlHelper, params Func<object, object>[] newRouteAndQueryStringParameters)
        {
            Argument.IsNotNull(urlHelper, "urlHelper");
            Argument.IsNotNull(urlHelper.RequestContext, "urlHelper.RequestContext");
            Argument.IsNotNull(newRouteAndQueryStringParameters, "newRouteAndQueryStringParameters");

            RouteValueDictionary newRouteValues;
            NameValueCollection newQueryString;
            GetNewRouteDataValuesAndQueryString(urlHelper.RequestContext.RouteData.Values, urlHelper.RequestContext.HttpContext.Request.QueryString, newRouteAndQueryStringParameters,
                                                out newRouteValues, out newQueryString);

            return CreateUrlWithQueryString(urlHelper.RouteUrl(newRouteValues) ?? string.Empty, newQueryString);
        }
        #pragma warning restore 1573
        private static string CreateUrlWithQueryString(string originalUrl, NameValueCollection queryStringParameters)
        {
            if (!queryStringParameters.HasKeys()) return originalUrl;

            StringBuilder sb = new StringBuilder(originalUrl);

            if (originalUrl == string.Empty)
                sb.Append('/');

            // If the URL string does not contain the query part.
            // TODO-IG: Find better way to check if the URL already contains query string.
            if (!originalUrl.Contains("?"))
                sb.Append('?');
            else
            {
                // If the '?' is not at the end of the URL, we assume that some query string parameters are already there.
                if (!originalUrl.EndsWith("?"))
                    sb.Append("&");
            }                

            foreach (string key in queryStringParameters.AllKeys.Where(key => key != null))
            {
                // The key is surely not null, but ReSharper unfortunately does not see that.
                // ReSharper disable PossibleNullReferenceException
                foreach (string value in queryStringParameters.GetValues(key))
                // ReSharper restore PossibleNullReferenceException
                    sb.AppendFormat("{0}={1}&", key, HttpUtility.HtmlEncode(value));
            }

            // Remove the last "&".
            if (sb.ToString().EndsWith("&"))
                sb.Length -= 1; // "&".Length is 1;

            return sb.ToString();
        }

        /// <summary>
        /// Gets the current absolute URL as defined in the request context.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If there is a parameter in the current URL query string that has the same name as one of the current route parameters, the existing parameter in the current route will be replaced by the value of the parameter in the query string.
        /// </para>
        /// </remarks>
        /// <inheritdoc cref="CurrentUrl(UrlHelper)" source="param[@name='urlHelper']" />
        /// <returns>
        /// Current absolute URL as defined in the request context.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="urlHelper"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RouteCollection"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RequestContext"/> is null.<br/>-or-<br/>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Current absolute URL is invalid (e.g. not all route parameters are defined or the HTTP request has no URL defined at all).<br/>-or-<br/>
        /// The HTTP request has a URL scheme that do not correspond to any of the web protocols defined in the <see cref="Protocol"/> enumeration.
        /// </exception>
        public static string CurrentAbsoluteUrl(this UrlHelper urlHelper)
        {
            return CurrentAbsoluteUrl(urlHelper, new Func<object, object>[0]);
        }

        /// <summary>
        /// Generates a new absolute URL by taking the current URL as defined in the request context and replacing the existing route parameters with provided new route parameters.
        /// </summary>
        /// <remarks>
        /// <para>
        /// TODO-IG: For complete documentation see CurrentUrl()
        /// If a parameter defined in the <paramref name="newRouteAndQueryStringParameters"/> is one of the current route parameters, the existing parameter in the current route will be replaced by that parameter value.<br/>
        /// If a parameter defined in the <paramref name="newRouteAndQueryStringParameters"/> is not one of the current route parameters, it will be added to the query string.<br/>
        /// If there is a parameter in the current URL query string that has the same name as one of the current route parameters, the existing parameter in the current route will be replaced by the value of the parameter in the query string.
        /// </para>
        /// <para>
        /// For example, let us assume that the current route is defined as "<i>{language}/{year}</i>" and that the current URL is "<i>http://localhost/en-US/1999</i>".<br/>
        /// The following call:<br/>
        /// <code>
        /// urlHelper.CurrentAbsoluteUrl(language => "hr-HR", year => 2000, p1 => "firstParameter", p2 => 1, p3 => 1.23);
        /// </code>
        /// will return "<i>http://localhost/hr-HR/2000?p1=firstParameter&amp;p2=1&amp;p3=1.23</i>".
        /// </para>        
        /// <note type="caution">
        /// If any of the <paramref name="newRouteAndQueryStringParameters"/> throws an exception, that exception will be propagated to the caller.
        /// </note>
        /// </remarks>
        /// <inheritdoc cref="CurrentUrl(UrlHelper)" source="param[@name='urlHelper']" />
        /// <inheritdoc cref="CurrentUrl(UrlHelper)" source="param[@name='newRouteAndQueryStringParameters']" />
        /// <returns>
        /// Current absolute URL as defined in the request context with original URL parameters replaced by <paramref name="newRouteAndQueryStringParameters"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="urlHelper"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RouteCollection"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RequestContext"/> is null.<br/>-or-<br/>
        /// <paramref name="newRouteAndQueryStringParameters"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Current absolute URL is invalid (e.g. not all route parameters are defined or the HTTP request has no URL defined at all).<br/>-or-<br/>
        /// The HTTP request has a URL scheme that do not correspond to any of the web protocols defined in the <see cref="Protocol"/> enumeration.
        /// </exception>
        public static string CurrentAbsoluteUrl(this UrlHelper urlHelper, params Func<object, object>[] newRouteAndQueryStringParameters)
        {
            Argument.IsNotNull(urlHelper, "urlHelper");
            Argument.IsNotNull(urlHelper.RouteCollection, "urlHelper.RouteCollection");
            Argument.IsNotNull(urlHelper.RequestContext, "urlHelper.RequestContext");
            Argument.IsNotNull(newRouteAndQueryStringParameters, "newRouteAndQueryStringParameters");

            return CurrentAbsoluteUrlCore(urlHelper, ProtocolHelper.GetProtocolFromHttpRequest(urlHelper.RequestContext.HttpContext.Request), newRouteAndQueryStringParameters);
        }

        #pragma warning disable 1573 // Parameter does not have XML comment.
        /// <inheritdoc cref="CurrentAbsoluteUrl(UrlHelper, Func{object, object}[])" />
        /// <param name="protocol">The protocol used as URI schema in the returned absolute URL. Use this parameter if you want to override the original URI scheme of the current URL.</param>
        /// <returns>
        /// Current absolute URL as defined in the request context with original URL parameters replaced by <paramref name="newRouteAndQueryStringParameters"/>
        /// and with the original URI scheme replaced by the one that corresponds to the <paramref name="protocol"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="urlHelper"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RouteCollection"/> is null.<br/>-or-<br/>
        /// <paramref name="urlHelper.RequestContext"/> is null.<br/>-or-<br/>
        /// <paramref name="newRouteAndQueryStringParameters"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">Current absolute URL is invalid (e.g. not all route parameters are defined).</exception>
        public static string CurrentAbsoluteUrl(this UrlHelper urlHelper, Protocol protocol, params Func<object, object>[] newRouteAndQueryStringParameters)
        {
            Argument.IsNotNull(urlHelper, "urlHelper");
            Argument.IsNotNull(urlHelper.RouteCollection, "urlHelper.RouteCollection");
            Argument.IsNotNull(urlHelper.RequestContext, "urlHelper.RequestContext");
            Argument.IsNotNull(newRouteAndQueryStringParameters, "newRouteAndQueryStringParameters");
            // TODO-IG: Check that the protocol is in range.

            return CurrentAbsoluteUrlCore(urlHelper, protocol, newRouteAndQueryStringParameters);
        }
        #pragma warning restore 1573

        /// <remarks>
        /// We extracted this method to avoid duplicated check of preconditions between <see cref="CurrentAbsoluteUrl(UrlHelper, Func{object, object}[])"/>
        /// and <see cref="CurrentAbsoluteUrl(UrlHelper, Protocol, Func{object, object}[])"/>.
        /// </remarks>
        private static string CurrentAbsoluteUrlCore(UrlHelper urlHelper, Protocol protocol, Func<object, object>[] newRouteAndQueryStringParameters)
        {
            RouteValueDictionary newRouteValues;
            NameValueCollection newQueryString;
            GetNewRouteDataValuesAndQueryString(urlHelper.RequestContext.RouteData.Values, urlHelper.RequestContext.HttpContext.Request.QueryString, newRouteAndQueryStringParameters,
                                                out newRouteValues, out newQueryString);

            string url = UrlHelper.GenerateUrl(null, // routeName
                                               null, // actionName
                                               null, // controllerName
                                               protocol.ToString().ToLowerInvariant(),
                                               null, // hostName,
                                               null, // fragment
                                               newRouteValues,
                                               urlHelper.RouteCollection,
                                               urlHelper.RequestContext,
                                               false // includeImplicitMvcValues
                                               );

            if (string.IsNullOrWhiteSpace(url))
                throw new InvalidOperationException(string.Format("The absolute URL for the current route and the protocol '{1}' cannot be generated.{0}" +
                                                                  "The route URL was: '{2}'.{0}" +
                                                                  "The route data values were:{0}" +
                                                                  "{3}{0}" +
                                                                  "The new route parameters were:{0}" +
                                                                  "{4}{0}" +
                                                                  "Make sure that the route values are supplied for all the parameters defined in the route URL.",
                                                                  Environment.NewLine,
                                                                  protocol,
                                                                  urlHelper.RequestContext.RouteData.Route as Route == null ? "<unable to detect route URL>"
                                                                                                                              : ((Route) urlHelper.RequestContext.RouteData.Route).Url,
                                                                  RouteValueDictionaryToString(urlHelper.RequestContext.RouteData.Values),
                                                                  RouteValueDictionaryToString(
                                                                      new RouteValueDictionary(newRouteAndQueryStringParameters.ToDictionary(function => function.Method.GetParameters()[0].Name, function => function(null))))
                                                                 )
                                                  );

            return CreateUrlWithQueryString(url, newQueryString);
        }

        private static void GetNewRouteDataValuesAndQueryString(RouteValueDictionary currentRouteValues, NameValueCollection currentQueryString, IEnumerable<Func<object, object>> newRouteAndQueryStringParameters,
                                                                out RouteValueDictionary newRouteValues, out NameValueCollection newQueryString)
        {
            // Take over all existing query string parameters.
            newQueryString = new NameValueCollection(currentQueryString);
            HashSet<string> currentQueryStringKeys = new HashSet<string>(currentQueryString.AllKeys);

            // Take over all existing route parameters.
            newRouteValues = new RouteValueDictionary(currentRouteValues);

            HashSet<string> newParameterKeys = new HashSet<string>();
            foreach (var newParameter in newRouteAndQueryStringParameters)
            {
                string key = newParameter.Method.GetParameters()[0].Name;
                object value = newParameter(null);

                if (value == null)
                    throw new ArgumentException(string.Format("The parameter '{0}' was null. " +
                                                              "A route or query string parameter cannot be set to null. " +
                                                              "If you want to remove the parameter from the route parameter collection " +
                                                              "or from the query string set it to 'UrlParameter.Optional'.", key),
                                                              "newRouteAndQueryStringParameters");

                if ((value as string) == string.Empty)
                    throw new ArgumentException(string.Format("The parameter '{0}' was empty string. " +
                                                              "A route or query string parameter cannot be set to empty string. " +
                                                              "If you want to remove the parameter from the route parameter collection " +
                                                              "or from the query string set it to 'UrlParameter.Optional'.", key),
                                                              "newRouteAndQueryStringParameters");

                // Check for duplicates in keys.
                if (newParameterKeys.Contains(key))
                    throw new ArgumentException(string.Format("Each new route and query string parameter can be specified only once. " +
                                                              "The parameter '{0}' was specified more than once.", key), "newRouteAndQueryStringParameters");

                newParameterKeys.Add(key);

                // If the parameter exists in the query string
                if (currentQueryStringKeys.Contains(key, StringComparer.InvariantCultureIgnoreCase))
                {
                    // Remove it explicitly (in case it has more than one value assigned or if it has to be removed because new value is UrlParameter.Optional)...
                    newQueryString.Remove(key);

                    // ...and replace it with new one only if the new one is not UrlParameter.Optional.
                    if (Equals(value, UrlParameter.Optional)) continue;

                    var items = UrlParameterValueAsEnumerable(value);
                    if (items != null)
                    {
                        AddEnumerableParameterToQueryString(newQueryString, key, items);
                    }
                    else // It is not an enumerable but just a single value.
                    {
                        // We already know that it is not UrlParameter.Optional (checked at the beginning) so we just have to set it.
                        newQueryString.Add(key, value.ToString());
                    }
                }
                else // If the parameter does not exist in the query string.
                {
                    // If it is an enumerable parameter, than we still want to add it to the query string.
                    var items = UrlParameterValueAsEnumerable(value);
                    if (items != null)
                    {
                        AddEnumerableParameterToQueryString(newQueryString, key, items);
                    }
                    else // It is not an enumerable but just a single value.
                    {
                        // If the key does not exist in route parameters, it will simply be added to the query string.
                        // This is a desired behavior.
                        // However, if the key does not exist in the rout parameters and is set to UrlParameter.Optional
                        // then we don't want to set it. In that case it would appear in the URL as a query parameter without value.
                        // That's why we check for that case.
                        if (Equals(value, UrlParameter.Optional) && !currentRouteValues.ContainsKey(key)) continue;

                        newRouteValues[key] = value;
                    }                    
                }
            }
        }

        private static IEnumerable UrlParameterValueAsEnumerable(object value)
        {
            // So far, we have to treat only the String as a non enumerable.
            return value is string ? null : value as IEnumerable;
        }


        private static void AddEnumerableParameterToQueryString(NameValueCollection queryString, string key, IEnumerable enumerable)
        {
            int i = 0;
            foreach (var item in enumerable)
            {
                // Silence ReSharper because it does not recognize the "newRouteAndQueryStringParameters".
                // ReSharper disable NotResolvedInText
                if (item == null)
                    throw new ArgumentException(string.Format("The item on the zero-index {0} of the enumerable query string parameter '{1}' was null. " +
                                                              "An enumerable item cannot be null.", i, key), "newRouteAndQueryStringParameters");

                if ((item as string) == string.Empty)
                    throw new ArgumentException(string.Format("The item on the zero-index {0} of the enumerable query string parameter '{1}' was empty string. " +
                                                              "An enumerable item cannot be empty string.", i, key), "newRouteAndQueryStringParameters");
                // ReSharper restore NotResolvedInText

                if (!Equals(item, UrlParameter.Optional)) // Ignore the UrlParameter.Optional.
                    queryString.Add(key, item.ToString());

                i++;
            }                                    
        }

        /// <summary>
        /// Generates a fully qualified absolute URL for the specified parameterless route and the HTTP protocol.
        /// </summary>
        /// <param name="urlHelper">The url helper used to generate the absolute URL.</param>
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
        /// <param name="urlHelper">The url helper used to generate the absolute URL.</param>
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
        /// Implicit MVC values "actionExpression" and "controller" are not automatically included. 
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

        #pragma warning disable 1573 // Parameter does not have XML comment.
        /// <summary>
        /// Generates a fully qualified absolute URL for the specified route, its values and the protocol.
        /// </summary>
        /// <inheritdoc cref="RouteAbsoluteUrl(UrlHelper, string, Option{object})" source="param" />
        /// <param name="protocol">The protocol used as URI schema in the absolute URL.</param>
        /// <inheritdoc cref="RouteAbsoluteUrl(UrlHelper, string, Option{object})" source="returns" />
        /// <inheritdoc cref="RouteAbsoluteUrl(UrlHelper, string, Option{object})" source="remarks" />
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
        #pragma warning restore 1573

        /// <inheritdoc cref="RouteAbsoluteUrl(UrlHelper, string, Option{object})" />
        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName, Option<RouteValueDictionary> routeValues)
        {
            return urlHelper.RouteAbsoluteUrl(routeName, routeValues, Protocol.Http);
        }

        /// <inheritdoc cref="RouteAbsoluteUrl(UrlHelper, string, Option{object}, Protocol)" />
        public static string RouteAbsoluteUrl(this UrlHelper urlHelper, string routeName, Option<RouteValueDictionary> routeValues, Protocol protocol)
        {
            Argument.IsNotNull(urlHelper, "urlHelper");
            Argument.IsNotNull(urlHelper.RouteCollection, "urlHelper.RouteCollection");
            Argument.IsNotNull(urlHelper.RequestContext, "urlHelper.RequestContext");
            Argument.IsNotNullOrWhitespace(routeName, "routeName");
            // TODO-IG: Check that the protocol is in range.

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

        /// <summary>
        /// Generates a fully qualified URL to an action method by using action name and controller name.
        /// </summary>
        /// <remarks>
        /// <para>
        /// A valid <paramref name="actionExpression"/> consists of a single controller method call. For example '<i>userController => userController.EditUser(0)</i>'.
        /// The action name is then taken from the method name ('<i>EditUser</i>' in the above example).
        /// If the controller method specified in the <paramref name="actionExpression"/> has the <see cref="ActionNameAttribute"/> applied,
        /// the action name is then taken from the <see cref="ActionNameAttribute.Name"/>.
        /// </para>
        /// <para>
        /// The controller name is taken from the name of the controller type which is provided through <typeparamref name="TController"/>
        /// using the standard MVC convention. For example, if the controller type name is <i>UserController</i>, the controller name will be <i>User</i>.
        /// </para>
        /// </remarks>
        /// <param name="urlHelper">The url helper used to generate the action URL.</param>
        /// <param name="actionExpression">Action expression that returns the action name.</param>
        /// <typeparam name="TController">The controller type.</typeparam>
        /// <returns>
        /// The fully qualified URL to an action method.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="urlHelper"/> is null.<br/>-or-<br/>
        /// <paramref name="actionExpression"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="actionExpression"/> is not a valid action expression.</exception>
        public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, ActionResult>> actionExpression) where TController : Controller
        {
            return urlHelper.Action(actionExpression, new RouteValueDictionary());
        }

        #pragma warning disable 1573 // Parameter does not have XML comment.
        /// <inheritdoc href="Action{TController}(UrlHelper, Expression{Func{TController, ActionResult}})" />
        /// <summary>
        /// Generates a fully qualified URL to an action method by using action name, controller name, and route values.
        /// </summary>
        /// <param name="routeValues">The object that contains the parameters for the <see cref="Route"/>.</param>
        public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, ActionResult>> actionExpression, Option<object> routeValues) where TController : Controller
        {
            // The constructor of the RouteValueDictionary class accepts null as a valid argument.
            return urlHelper.Action(actionExpression, new RouteValueDictionary(routeValues.ValueOrNull));
        }
        #pragma warning restore 1573

        /// <inheritdoc href="Action{TController}(UrlHelper, Expression{Func{TController, ActionResult}}, Option{object})" />
        public static string Action<TController>(this UrlHelper urlHelper, Expression<Func<TController, ActionResult>> actionExpression, Option<RouteValueDictionary> routeValues) where TController : Controller
        {
            Argument.IsNotNull(urlHelper, "urlHelper");
            Argument.IsNotNull(actionExpression, "actionExpression");

            var actionName = ControllerHelper.GetActionName(actionExpression);
            var controllerName = ControllerHelper.GetControllerName(typeof(TController));

            return urlHelper.Action(actionName, controllerName, routeValues.ValueOrNull);
        }
    }
}