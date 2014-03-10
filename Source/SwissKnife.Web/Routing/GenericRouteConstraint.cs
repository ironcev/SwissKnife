using System;
using System.Web;
using System.Web.Routing;

namespace SwissKnife.Web.Routing // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    public class GenericRouteConstraint : IRouteConstraint
    {
        private readonly Func<object, bool> _constraint;

        public GenericRouteConstraint(Func<object, bool> constraint)
        {
            _constraint = constraint;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return _constraint == null || _constraint( values[parameterName] );
        }
    }
}
