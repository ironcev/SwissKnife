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
        /// <exception cref="ArgumentNullException">
        /// <paramref name="route"/> is null.<br/>-or-<br/><paramref name="urlParameter"/> is null.<br/>-or-<br/><paramref name="defaultValue"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="urlParameter"/> is empty string.<br/>-or-<br/><paramref name="urlParameter"/> is white space.</exception>
        public static Route SetDefault(this Route route, string urlParameter, object defaultValue)
        {
            Argument.IsNotNull(route, "route");
            Argument.IsNotNullOrWhitespace(urlParameter, "urlParameter");
            Argument.IsNotNull(defaultValue, "defaultValue");

            if (route.Defaults == null) route.Defaults = new RouteValueDictionary();

            route.Defaults[urlParameter] = defaultValue;

            return route;
        }

        /// <summary>
        /// Sets the default value for a route URL parameter.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the default value for the URL parameter is already set it will be overwritten by the new default value.
        /// </para>
        /// <note type="caution">
        /// If the <paramref name="urlParameterAndDefaultValue"/> throws an exception, that exception will be propagated to the caller.
        /// </note>
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
            Argument.IsNotNull(route, "route");
            Argument.IsNotNull(urlParameterAndDefaultValue, "urlParameterAndDefaultValue");

            if (route.Defaults == null) route.Defaults = new RouteValueDictionary();

            object defaultValue = urlParameterAndDefaultValue(null);

            Argument.IsValid(defaultValue != null, "The calculated default value must not be null.", "urlParameterAndDefaultValue");

            route.Defaults[urlParameterAndDefaultValue.Method.GetParameters()[0].Name] = defaultValue;

            return route;
        }

        /// <summary>
        /// Sets default values for route URL parameters.
        /// </summary>
        /// <remarks>
        /// <para>
        /// URL parameters are defined as lambda expressions. For example: language => "en-US" will set the "language" URL parameter to the value "en-US".
        /// </para>
        /// <para>
        /// If any of the URL parameters is already set it will be overwritten by the specified default values.
        /// If any URL parameter is specified in the <paramref name="urlParametersAndDefaultValues"/> more than once, the last definition wins.
        /// </para>
        /// <note type="caution">
        /// If any of the <paramref name="urlParametersAndDefaultValues"/> throws an exception, that exception will be propagated to the caller.
        /// </note>
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
            Argument.IsNotNull(route, "route");
            Argument.IsNotNull(urlParametersAndDefaultValues, "urlParametersAndDefaultValues");
            Argument.IsValid(urlParametersAndDefaultValues.Length > 0, "The array of URL parameters and their default values is empty. The array must contain at least one URL parameter with its default value specified.", "urlParametersAndDefaultValues");

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

        /// <summary>
        /// Sets constraint for a route URL parameter expressed as an arbitrary predicate that checks whether a URL parameter value is valid.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If a constraint for the URL parameter is already set it will be overwritten by the <paramref name="predicate"/>.
        /// </para>
        /// <note type="caution">
        /// If the <paramref name="predicate"/> throws an exception, that exception will be propagated to the caller.
        /// </note>
        /// </remarks>
        /// <param name="route">The <see cref="Route"/> whose <see cref="Route.Constraints"/> will be set.</param>
        /// <param name="urlParameter">Name of the URL parameter.</param>
        /// <param name="predicate">The predicate that returns true if the value of the <paramref name="urlParameter"/> is valid.</param>
        /// <returns>
        /// The original <paramref name="route"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="route"/> is null.<br/>-or-<br/><paramref name="urlParameter"/> is null.<br/>-or-<br/><paramref name="predicate"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="urlParameter"/> is empty string.<br/>-or-<br/><paramref name="urlParameter"/> is white space.</exception>
        public static Route SetConstraint(this Route route, string urlParameter, Predicate<object> predicate)
        {
            // Other preconditions will be checked in the calling method.
            Argument.IsNotNull(predicate, "predicate");

            return route.SetConstraint(urlParameter, new PredicateRouteConstraint(predicate));
        }

        /// <summary>
        /// Sets constraint for a route URL parameter expressed as an arbitrary route constraint.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If a constraint for the URL parameter is already set it will be overwritten by the <paramref name="constraint"/>.
        /// </para>
        /// <note type="caution">
        /// If the <paramref name="constraint"/> throws an exception, that exception will be propagated to the caller.
        /// </note>
        /// </remarks>
        /// <param name="route">The <see cref="Route"/> whose <see cref="Route.Constraints"/> will be set.</param>
        /// <param name="urlParameter">Name of the URL parameter.</param>
        /// <param name="constraint"><see cref="IRouteConstraint"/> that checks whether the value of the <paramref name="urlParameter"/> is valid.</param>
        /// <returns>
        /// The original <paramref name="route"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="route"/> is null.<br/>-or-<br/><paramref name="urlParameter"/> is null.<br/>-or-<br/><paramref name="constraint"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="urlParameter"/> is empty string.<br/>-or-<br/><paramref name="urlParameter"/> is white space.</exception>
        public static Route SetConstraint(this Route route, string urlParameter, IRouteConstraint constraint)
        {
            Argument.IsNotNull(route, "route");
            Argument.IsNotNullOrWhitespace(urlParameter, "urlParameter");
            Argument.IsNotNull(constraint, "predicate");

            if (route.Constraints == null) route.Constraints = new RouteValueDictionary();

            route.Constraints[urlParameter] = constraint;

            return route;
        }

        /// <summary>
        /// Sets predicate for a route URL parameter expressed as a regular expression.
        /// </summary>
        /// <param name="route">The <see cref="Route"/> whose <see cref="Route.Constraints"/> will be set.</param>
        /// <param name="urlParameter">Name of the URL parameter.</param>
        /// <param name="regularExpression">Regular expression that defines a valid <paramref name="urlParameter"/>.</param>
        /// <returns>
        /// The original <paramref name="route"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="route"/> is null.<br/>-or-<br/><paramref name="urlParameter"/> is null.<br/>-or-<br/><paramref name="regularExpression"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="urlParameter"/> is empty string.<br/>-or-<br/><paramref name="urlParameter"/> is white space.<br/>-or-<br/>
        /// <paramref name="regularExpression"/> is empty string.<br/>-or-<br/><paramref name="regularExpression"/> is white space.
        /// </exception>
        public static Route SetConstraint(this Route route, string urlParameter, string regularExpression)
        {
            Argument.IsNotNull(route, "route");
            Argument.IsNotNullOrWhitespace(urlParameter, "urlParameter");
            Argument.IsNotNull(regularExpression, "regularExpression");

            if (route.Constraints == null) route.Constraints = new RouteValueDictionary();

            route.Constraints[urlParameter] = regularExpression;

            return route;
        }
    }
}