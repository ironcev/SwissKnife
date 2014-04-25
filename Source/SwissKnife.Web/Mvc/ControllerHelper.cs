using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Mvc
{
    internal static class ControllerHelper
    {
        private const string DefaultControllerNameSuffix = "Controller";

        internal static string GetControllerName(Type controllerType)
        {
            System.Diagnostics.Debug.Assert(controllerType != null);

            string controllerName = controllerType.Name;

            if (controllerName.EndsWith(DefaultControllerNameSuffix, StringComparison.InvariantCultureIgnoreCase) &&
                !controllerName.Equals(DefaultControllerNameSuffix, StringComparison.InvariantCultureIgnoreCase))
            {
                controllerName = controllerName.Substring(0, controllerName.Length - DefaultControllerNameSuffix.Length);
            }

            return controllerName;
        }

        internal static string GetActionName<TController>(Expression<Func<TController, ActionResult>> actionExpression) where TController : Controller
        {
            System.Diagnostics.Debug.Assert(actionExpression != null);

            return GetActionNameFromActionExpressionCore(actionExpression.Body);
        }

        internal static string GetActionName<TController>(Expression<Func<TController, Task<ActionResult>>> actionExpression) where TController : Controller
        {
            System.Diagnostics.Debug.Assert(actionExpression != null);

            return GetActionNameFromActionExpressionCore(actionExpression.Body);
        }

        private static string GetActionNameFromActionExpressionCore(Expression actionExpression)
        {
            Argument.IsValid(actionExpression is MethodCallExpression,
                             string.Format("Action expression is not a valid action expression.{0}" +
                                           "The body of a valid action expression must consist of a single method call (e.g. 'userController => userController.EditUser(0)').{0}" +
                                           "The body of the action expression was: '{1}'.",
                                           Environment.NewLine,
                                           actionExpression),
                             "actionExpression");

            MethodInfo actionMethodInfo = ((MethodCallExpression)actionExpression).Method;

            // By default, the action name is the name of the controller action method.
            string actionName = actionMethodInfo.Name;

            // If the action method has the ActionNameAttribute specified,
            // take the action name from the attribute.
            ActionNameAttribute[] actionNameAttributes = (ActionNameAttribute[])actionMethodInfo.GetCustomAttributes(typeof(ActionNameAttribute), false);
            if (actionNameAttributes.Length > 0)
                actionName = actionNameAttributes[0].Name; // ActionNameAttribute can be applied only once.

            return actionName;            
        }
    }
}