using System;
using System.Web.Mvc;
using NUnit.Framework;
using SwissKnife.Web.Mvc;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class ControllerHelperTests
    {
        #region GetControllerNameFromControllerType
        [Test]
        public void GetControllerNameFromControllerType_ControllerTypeNameEndsWithControler_ReturnsControllerName()
        {
            Assert.That(ControllerHelper.GetControllerNameFromControllerType(typeof(ControllerNameController)), Is.EqualTo("ControllerName"));
        }

        [Test]
        public void GetControllerNameFromControllerType_ControllerTypeNameEndsWithControlerCaseSensitive_ReturnsControllerName()
        {
            Assert.That(ControllerHelper.GetControllerNameFromControllerType(typeof(ControllerNameCoNtRoLlEr)), Is.EqualTo("ControllerName"));
        }

        [Test]
        public void GetControllerNameFromControllerType_ControllerTypeNameEndsWithControler_ReturnsControllerNameCaseSensitive()
        {
            Assert.That(ControllerHelper.GetControllerNameFromControllerType(typeof(CONTROLLERNAMEController)), Is.EqualTo("CONTROLLERNAME"));
        }

        [Test]
        public void GetControllerNameFromControllerType_ControllerTypeNameDoesNoteEndWithControler_ReturnsTypeName()
        {
            Assert.That(ControllerHelper.GetControllerNameFromControllerType(typeof(TyPeNaMe)), Is.EqualTo("TyPeNaMe"));
        }

        [Test]
        public void GetControllerNameFromControllerType_ControllerTypeNameIsControler_ReturnsControler()
        {
            Assert.That(ControllerHelper.GetControllerNameFromControllerType(typeof(Controller)), Is.EqualTo("Controller"));
        }

        [Test]
        public void GetControllerNameFromControllerType_ControllerTypeNameIsControlerCaseSensitive_ReturnsTypeName()
        {
            Assert.That(ControllerHelper.GetControllerNameFromControllerType(typeof(CoNtRoLlEr)), Is.EqualTo("CoNtRoLlEr"));
        }

        class ControllerNameController { }
        class ControllerNameCoNtRoLlEr { }
        class CONTROLLERNAMEController { }
        class TyPeNaMe { }
        class Controller { }
        class CoNtRoLlEr { }
        #endregion

        #region GetActionNameFromActionExpression
        [Test]
        public void GetActionNameFromActionExpression_ActionExpressionNotValid_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => ControllerHelper.GetActionNameFromActionExpression<TestController>(c => new EmptyResult()));
            Assert.That(exception.ParamName, Is.EqualTo("actionExpression"));
            Assert.That(exception.Message.Contains("Action expression is not a valid action expression."));
            Assert.That(exception.Message.Contains("'c => new EmptyResult()'"));
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void GetActionNameFromActionExpression_ActionWithoutParameters_ReturnsActionName()
        {
            Assert.That(ControllerHelper.GetActionNameFromActionExpression<TestController>(c => TestController.ActionWithoutParameters()), Is.EqualTo("ActionWithoutParameters"));
        }

        [Test]
        public void GetActionNameFromActionExpression_ActionWithParameters_ReturnsActionName()
        {
            Assert.That(ControllerHelper.GetActionNameFromActionExpression<TestController>(c => TestController.ActionWithParameters(0, null, null)), Is.EqualTo("ActionWithParameters"));
        }

        [Test]
        public void GetActionNameFromActionExpression_ActionWithActionNameAttribute_ReturnsActionNameFromTheAttribute()
        {
            Assert.That(ControllerHelper.GetActionNameFromActionExpression<TestController>(c => TestController.ActionWithActionNameAttribute()), Is.EqualTo("ActionNameFromTheAttribute"));
        }

        class TestController : System.Web.Mvc.Controller
        {
            public static ActionResult ActionWithoutParameters() { return null; }
            // ReSharper disable UnusedParameter.Local
            public static ActionResult ActionWithParameters(int a, string b, object c) { return null; }
            // ReSharper restore UnusedParameter.Local
            [ActionName("ActionNameFromTheAttribute")]
            public static ActionResult ActionWithActionNameAttribute() { return null; }
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
