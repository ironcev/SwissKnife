using System;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using SwissKnife.Web.Mvc;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class ControllerHelperTests
    {
        #region GetControllerName
        [Test]
        public void GetControllerName_ControllerTypeNameEndsWithControler_ReturnsControllerName()
        {
            Assert.That(ControllerHelper.GetControllerName<ControllerNameController>(), Is.EqualTo("ControllerName"));
        }

        [Test]
        public void GetControllerName_ControllerTypeNameEndsWithControlerCaseSensitive_ReturnsControllerName()
        {
            Assert.That(ControllerHelper.GetControllerName<ControllerNameCoNtRoLlEr>(), Is.EqualTo("ControllerName"));
        }

        [Test]
        public void GetControllerName_ControllerTypeNameEndsWithControler_ReturnsControllerNameCaseSensitive()
        {
            Assert.That(ControllerHelper.GetControllerName<CONTROLLERNAMEController>(), Is.EqualTo("CONTROLLERNAME"));
        }

        [Test]
        public void GetControllerName_ControllerTypeNameDoesNoteEndWithControler_ReturnsTypeName()
        {
            Assert.That(ControllerHelper.GetControllerName<TyPeNaMe>(), Is.EqualTo("TyPeNaMe"));
        }

        [Test]
        public void GetControllerName_ControllerTypeNameIsControler_ReturnsControler()
        {
            Assert.That(ControllerHelper.GetControllerName<Controller>(), Is.EqualTo("Controller"));
        }

        [Test]
        public void GetControllerName_ControllerTypeNameIsControlerCaseSensitive_ReturnsTypeName()
        {
            Assert.That(ControllerHelper.GetControllerName<CoNtRoLlEr>(), Is.EqualTo("CoNtRoLlEr"));
        }

        // ReSharper disable ClassNeverInstantiated.Local
        class ControllerNameController : IController { public void Execute(RequestContext requestContext) { } }
        class ControllerNameCoNtRoLlEr : IController { public void Execute(RequestContext requestContext) { } }
        class CONTROLLERNAMEController : IController { public void Execute(RequestContext requestContext) { } }
        class TyPeNaMe : IController { public void Execute(RequestContext requestContext) { } }
        class Controller : IController { public void Execute(RequestContext requestContext) { } }
        class CoNtRoLlEr : IController { public void Execute(RequestContext requestContext) { } }
        // ReSharper restore ClassNeverInstantiated.Local
        #endregion

        #region GetActionName
        [Test]
        public void GetActionName_ActionExpressionNotValid_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => ControllerHelper.GetActionName<TestController>(c => new EmptyResult()));
            Assert.That(exception.ParamName, Is.EqualTo("actionExpression"));
            Assert.That(exception.Message.Contains("Action expression is not a valid action expression."));
            Assert.That(exception.Message.Contains("'new EmptyResult()'"));
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void GetActionName_StaticActionWithoutParameters_ReturnsActionName()
        {
            Assert.That(ControllerHelper.GetActionName<TestController>(c => TestController.StaticActionWithoutParameters()), Is.EqualTo("StaticActionWithoutParameters"));
        }

        [Test]
        public void GetActionName_StaticActionWithParameters_ReturnsActionName()
        {
            Assert.That(ControllerHelper.GetActionName<TestController>(c => TestController.StaticActionWithParameters(0, null, null)), Is.EqualTo("StaticActionWithParameters"));
        }

        [Test]
        public void GetActionName_StaticActionWithActionNameAttribute_ReturnsActionNameFromTheAttribute()
        {
            Assert.That(ControllerHelper.GetActionName<TestController>(c => TestController.StaticActionWithActionNameAttribute()), Is.EqualTo("ActionNameFromTheAttribute"));
        }

        [Test]
        public void GetActionName_ActionWithoutParameters_ReturnsActionName()
        {
            Assert.That(ControllerHelper.GetActionName<TestController>(c => c.ActionWithoutParameters()), Is.EqualTo("ActionWithoutParameters"));
        }

        [Test]
        public void GetActionName_ActionWithParameters_ReturnsActionName()
        {
            Assert.That(ControllerHelper.GetActionName<TestController>(c => c.ActionWithParameters(0, null, null)), Is.EqualTo("ActionWithParameters"));
        }

        [Test]
        public void GetActionName_ActionWithActionNameAttribute_ReturnsActionNameFromTheAttribute()
        {
            Assert.That(ControllerHelper.GetActionName<TestController>(c => c.ActionWithActionNameAttribute()), Is.EqualTo("ActionNameFromTheAttribute"));
        }

        class TestController : System.Web.Mvc.Controller
        {
            public static ActionResult StaticActionWithoutParameters() { return null; }
            // ReSharper disable UnusedParameter.Local
            public static ActionResult StaticActionWithParameters(int a, string b, object c) { return null; }
            // ReSharper restore UnusedParameter.Local
            [ActionName("ActionNameFromTheAttribute")]
            public static ActionResult StaticActionWithActionNameAttribute() { return null; }

            // ReSharper disable MemberCanBeMadeStatic.Local
            public ActionResult ActionWithoutParameters() { return null; }
            // ReSharper disable UnusedParameter.Local
            public ActionResult ActionWithParameters(int a, string b, object c) { return null; }
            // ReSharper restore UnusedParameter.Local
            [ActionName("ActionNameFromTheAttribute")]
            public ActionResult ActionWithActionNameAttribute() { return null; }
            // ReSharper restore MemberCanBeMadeStatic.Local
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
