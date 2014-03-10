using System;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    internal static class ExtensionsHelper
    {
        internal static string GetControllerNameFromType(Type controllerType)
        {
            var controllerName = controllerType.Name;

            if (controllerName.EndsWith("Controller", StringComparison.InvariantCultureIgnoreCase))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - "Controller".Length);
            }

            return controllerName;
        }

        internal static string GetControllerNamespaceFromType(Type controllerType)
        {
            var controllerNamespace = controllerType.FullName;

            controllerNamespace = controllerNamespace.Substring(0, controllerNamespace.Length - (controllerType.Name.Length + 1));

            return controllerNamespace;
        }

        internal static string GetActionNameFromExpression(Expression actionBody)
        {
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