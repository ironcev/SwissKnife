/*
 * This class is inspired by a similar one originally developed by Marin Rončević (http://github.com/mroncev).
 */
using System;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;
using SwissKnife.Web.Routing;

namespace SwissKnife.Web.Mvc
{
    /// <summary>
    /// Contains extension methods for the <see cref="Route"/> class that enable defining routes by using fluent interface.
    /// </summary>
    public static class RouteExtensions
    {
        /// <summary>
        /// Sets the default value for a route URL parameter.
        /// </summary>
        /// <remarks>
        /// If the default value for the <paramref name="urlParameter"/> is already set it will be overwritten by the <paramref name="defaultValue"/>.
        /// </remarks>
        /// <param name="route">The <see cref="Route"/> whose <see cref="Route.Defaults"/> will be set.</param>
        /// <param name="urlParameter">Name of the URL parameter.</param>
        /// <param name="defaultValue">Default value for the <paramref name="urlParameter"/>.</param>
        /// <returns>
        /// The original <paramref name="route"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="route"/> is null.<br/>-or-<br/><paramref name="urlParameter"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="urlParameter"/> is empty string.<br/>-or-<br/><paramref name="urlParameter"/> is white space.</exception>
        public static Route SetDefault(this Route route, string urlParameter, object defaultValue)
        {
            #region Preconditions
            Argument.IsNotNull(route, "route");
            Argument.IsNotNullOrWhitespace(urlParameter, "urlParameter");
            Argument.IsNotNull(defaultValue, "defaultValue");
            #endregion

            if (route.Defaults == null) route.Defaults = new RouteValueDictionary();

            route.Defaults[urlParameter] = defaultValue;

            return route;
        }

        /// <summary>
        /// Sets the default value for a route URL parameter.
        /// </summary>
        /// <remarks>
        /// If the default value for the URL parameter is already set it will be overwritten by the new default value.
        /// <br/>
        /// <br/>
        /// <b>Note</b>
        /// <br/>
        /// If the <paramref name="urlParameterAndDefaultValue"/> throws an exception, that exception will be propagated to the caller.
        /// </remarks>
        /// <param name="route">The <see cref="Route"/> whose <see cref="Route.Defaults"/> will be set.</param>
        /// <param name="urlParameterAndDefaultValue">URL parameter and its values defined as lambda expressions.</param>
        /// <returns>
        /// The original <paramref name="route"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="route"/> is null.<br/>-or-<br/><paramref name="urlParameterAndDefaultValue"/> is null.</exception>
        /// <exception cref="ArgumentException">The calculated default value is null.</exception>
        public static Route SetDefault(this Route route, Func<object, object> urlParameterAndDefaultValue)
        {
            #region Preconditions
            Argument.IsNotNull(route, "route");
            Argument.IsNotNull(urlParameterAndDefaultValue, "urlParameterAndDefaultValue");
            #endregion

            if (route.Defaults == null) route.Defaults = new RouteValueDictionary();

            object defaultValue = urlParameterAndDefaultValue(null);

            Argument.IsValid(defaultValue != null, "The calculated default value must not be null.", "urlParameterAndDefaultValue");

            route.Defaults[urlParameterAndDefaultValue.Method.GetParameters()[0].Name] = defaultValue;

            return route;
        }

        /// <summary>
        /// Sets default values for a route URL parameters.
        /// </summary>
        /// <remarks>
        /// URL parameters are defined as lambda expressions. For example: language => "en-US" will set the "language" URL parameter to the value "en-US".
        /// <br/>
        /// If any of the URL parameters is already set it will be overwritten by the specified default values.
        /// If any URL parameter is specified in the <paramref name="urlParametersAndDefaultValues"/> more than once, the last definition wins.
        /// <br/>
        /// <br/>
        /// <b>Note</b>
        /// <br/>
        /// If any of the <paramref name="urlParametersAndDefaultValues"/> throws an exception, that exception will be propagated to the caller.
        /// </remarks>
        /// <param name="route">The <see cref="Route"/> whose <see cref="Route.Defaults"/> will be set.</param>
        /// <param name="urlParametersAndDefaultValues">URL parameters and their values defined as lambda expressions.</param>
        /// <returns>
        /// The original <paramref name="route"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="route"/> is null.<br/>-or-<br/><paramref name="urlParametersAndDefaultValues"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="urlParametersAndDefaultValues"/> is empty.</exception>
        public static Route SetDefaults(this Route route, params Func<object, object>[] urlParametersAndDefaultValues)
        {
            #region Preconditions
            Argument.IsNotNull(route, "route");
            Argument.IsNotNull(urlParametersAndDefaultValues, "urlParametersAndDefaultValues");
            Argument.IsValid(urlParametersAndDefaultValues.Length > 0, "The array of URL parameters and their default values is empty. The array must contain at least one URL parameter with its default value specified.", "urlParametersAndDefaultValues");
            #endregion

            if (route.Defaults == null) route.Defaults = new RouteValueDictionary();

            foreach (var function in urlParametersAndDefaultValues)
            {
                object defaultValue = function(null);
                string urlParameter = function.Method.GetParameters()[0].Name;

                Argument.IsValid(defaultValue != null, string.Format("The calculated default value must not be null. The calculated value of the parameter '{0}' was null.", urlParameter), "urlParametersAndDefaultValues");

                route.Defaults[urlParameter] = defaultValue;
            }

            return route;
        }

        public static Route AddConstraint(this Route route, string key, Predicate<object> constraint)
        {
            return route.AddConstraint(key, new PredicateRouteConstraint(constraint));
        }

        public static Route AddConstraint(this Route route, string key, IRouteConstraint constraint)
        {
            Argument.IsNotNullOrWhitespace(key, "key");

            route.Constraints[key] = constraint;

            return route;
        }
    }
}