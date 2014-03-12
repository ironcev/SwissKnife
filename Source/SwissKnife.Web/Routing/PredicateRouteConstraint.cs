using System;
using System.Linq;
using System.Web;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Routing
{
    /// <summary>
    /// <see cref="IRouteConstraint"/> implementation which uses an arbitrary predicate that checks whether a URL parameter value is valid.
    /// </summary>
    public class PredicateRouteConstraint : IRouteConstraint
    {
        private readonly Predicate<object> predicate; 

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateRouteConstraint"/> class using the specified <paramref name="constraint"/> given as <see cref="Predicate{T}"/>.
        /// </summary>
        /// <param name="constraint">The constraint that checks whether a URL parameter value is valid.</param>
        /// <exception cref="ArgumentNullException"><paramref name="constraint"/> is null.</exception>
        public PredicateRouteConstraint(Predicate<object> constraint)
        {
            #region Preconditions
            Argument.IsNotNull(constraint, "constraint");
            #endregion

            predicate = constraint;
        }

        /// <summary>
        /// Determines whether the URL parameter contains a valid value for this constraint.
        /// </summary>
        /// <returns>
        /// true if the URL parameter contains a valid value; otherwise, false.
        /// </returns>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request. <b>This value is ignored</b> in the check whether a URL parameter value is valid.</param>
        /// <param name="route">The object that this constraint belongs to. <b>This value is ignored</b> in the check whether a URL parameter value is valid.</param>
        /// <param name="parameterName">The name of the parameter that is being checked.</param>
        /// <param name="values">An object that contains the parameters for the URL.</param>
        /// <param name="routeDirection">An object that indicates whether the constraint check is being performed when an incoming request is being handled or when a URL is being generated. <b>This value is ignored</b> in the check whether a URL parameter value is valid.</param>
        /// <exception cref="ArgumentException"><paramref name="parameterName"/> is empty or white space.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="values"/> or <paramref name="parameterName"/> is null.</exception>
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            #region Preconditions
            Argument.IsNotNullOrWhitespace(parameterName, "parameterName");
            Argument.IsNotNull(values, "values");
            Argument.IsValid(values.ContainsKey(parameterName),
                                                string.Format("The values does not contain the parameter '{1}'.{0}" +
                                                              "The contained parameters are:{0}" +
                                                              "{2}",
                                                              Environment.NewLine,
                                                              parameterName,
                                                              values.Keys.Count <= 0 ? "<Values do not contain any parameters.>" + Environment.NewLine
                                                                                       : values.Keys.Aggregate(string.Empty, (result, current) => result + current + Environment.NewLine)),
                                                "values");
            #endregion

            return predicate(values[parameterName]);
        }
    }
}
