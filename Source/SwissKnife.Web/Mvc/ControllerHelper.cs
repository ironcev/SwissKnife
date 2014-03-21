using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SwissKnife.Web.Mvc
{
    internal static class ControllerHelper
    {
        private const string DefaultControllerNameSuffix = "Controller";

        internal static string GetControllerNameFromControllerType(Type controllerType)
        {
            System.Diagnostics.Debug.Assert(controllerType != null);

            var controllerName = controllerType.Name;

            if (controllerName.EndsWith(DefaultControllerNameSuffix, StringComparison.InvariantCultureIgnoreCase) && controllerName != DefaultControllerNameSuffix)
            {
                controllerName = controllerName.Substring(0, controllerName.Length - DefaultControllerNameSuffix.Length);
            }

            return controllerName;
        }

        internal static string GetActionNameFromActionExpression(Expression actionBody)
        {
            System.Diagnostics.Debug.Assert(actionBody != null);

            var methodInfo = ((MethodCallExpression)actionBody).Method;

            var actionName = methodInfo.Name;

            ActionNameAttribute[] actionNameAttributes = (ActionNameAttribute[])methodInfo.GetCustomAttributes(typeof(ActionNameAttribute), false);

            if (actionNameAttributes.Length > 0)
            {
                actionName = actionNameAttributes[0].Name;
            }

            return actionName;
        }
    }
}