using System;
using System.Linq;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;
using SwissKnife.Web.Routing;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    internal static class RouteExtensions
    {
        public static Route AddDefaults(this Route route, string parameterName, object defaultValue)
        {
            return route.AddDefaults(new[] { parameterName, defaultValue });
        }

        public static Route AddDefaults(this Route route, params object[] parameterNamesAndDefaultValues)
        {
            Argument.IsNotNull(parameterNamesAndDefaultValues, "parameterNamesAndDefaultValues");
            Argument.IsValid(parameterNamesAndDefaultValues.Length % 2 == 0, @"AddDefaults() parameter names and default values must have even number of objects. First object represents parameter name, second is corresponding default value and so on.", "parameterNamesAndDefaultValues");
            Argument.IsValid(!parameterNamesAndDefaultValues.Where((item, index) => (item == null || string.IsNullOrWhiteSpace(item.ToString())) && index % 2 == 0).Any(), @"AddDefaults() parameter names must not be null or whitespace.", "parameterNamesAndDefaultValues");

            for (var i = 0; i < parameterNamesAndDefaultValues.Length - 1; i += 2)
            {
                route.Defaults[parameterNamesAndDefaultValues[i].ToString()] = parameterNamesAndDefaultValues[i + 1];
            }

            return route;
        }

        public static Route AddConstraint(this Route route, string key, Func<object, bool> constraint)
        {
            return route.AddConstraint(key, new GenericRouteConstraint(constraint));
        }

        public static Route AddConstraint(this Route route, string key, IRouteConstraint constraint)
        {
            Argument.IsNotNullOrWhitespace(key, "key");

            route.Constraints[key] = constraint;

            return route;
        }
    }
}