using System;
using System.Web;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using SwissKnife.Web.Routing;

namespace SwissKnife.Web.Tests.Unit.Routing
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class PredicateRouteConstraintTests
    {
        private readonly HttpContextBase httpContext = new Mock<HttpContextBase>().Object;
        private readonly Route route = new Route(string.Empty, new Mock<IRouteHandler>().Object);
        private const RouteDirection routeDirection = RouteDirection.IncomingRequest;
        private const string parameterName = "ParameterName";


        [Test]
        public void Constructor_PredicateIsNull_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new PredicateRouteConstraint(null));
            Assert.That(exception.ParamName, Is.EqualTo("constraint"));
        }


        [Test]
        public void Match_ParameterNameIsNull_ThrowsException()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var exception = Assert.Throws<ArgumentNullException>(() => new PredicateRouteConstraint(o => true).Match(httpContext, route, null, new RouteValueDictionary(), routeDirection));
            // ReSharper restore AssignNullToNotNullAttribute
            Assert.That(exception.ParamName, Is.EqualTo("parameterName"));
            Assert.That(exception.Message.Contains("null"));
        }

        [Test]
        public void Match_ParameterNameIsEmpty_ThrowsException()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var exception = Assert.Throws<ArgumentException>(() => new PredicateRouteConstraint(o => true).Match(httpContext, route, string.Empty, new RouteValueDictionary(), routeDirection));
            // ReSharper restore AssignNullToNotNullAttribute
            Assert.That(exception.ParamName, Is.EqualTo("parameterName"));
            Assert.That(exception.Message.Contains("empty"));
        }

        [Test]
        public void Match_ParameterNameIsWhitespace_ThrowsException()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var exception = Assert.Throws<ArgumentException>(() => new PredicateRouteConstraint(o => true).Match(httpContext, route, " ", new RouteValueDictionary(), routeDirection));
            // ReSharper restore AssignNullToNotNullAttribute
            Assert.That(exception.ParamName, Is.EqualTo("parameterName"));
            Assert.That(exception.Message.Contains("white space"));
        }

        [Test]
        public void Match_ValuesAreNull_ThrowsException()
        {
            // ReSharper disable AssignNullToNotNullAttribute
            var exception = Assert.Throws<ArgumentNullException>(() => new PredicateRouteConstraint(o => true).Match(httpContext, route, parameterName, null, routeDirection));
            // ReSharper restore AssignNullToNotNullAttribute
            Assert.That(exception.ParamName, Is.EqualTo("values"));
            Assert.That(exception.Message.Contains("null"));
        }

        [Test]
        public void Match_ValuesAreEmpty_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new PredicateRouteConstraint(o => true).Match(httpContext, route, parameterName, new RouteValueDictionary(), routeDirection));
            Assert.That(exception.ParamName, Is.EqualTo("values"));
            Console.WriteLine(exception.Message);
            Assert.That(exception.Message.Contains("Values do not contain any parameters."));
        }

        [Test]
        public void Match_ValuesDonNotContainParameter_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => new PredicateRouteConstraint(o => true).Match(httpContext, route, parameterName, new RouteValueDictionary { { "FirstParameter", null }, { "SecondParameter", null } }, routeDirection));
            Assert.That(exception.ParamName, Is.EqualTo("values"));
            Console.WriteLine(exception.Message);
            Assert.That(exception.Message.Contains("FirstParameter"));
            Assert.That(exception.Message.Contains("SecondParameter"));
        }

        [Test]
        public void Match_CallsConstraintWithParameterName()
        {
            object parameterValue = new object();
            new PredicateRouteConstraint(o => { Assert.That(o, Is.SameAs(parameterValue)); return true; }).Match(httpContext, route, parameterName, new RouteValueDictionary { { parameterName, parameterValue } }, routeDirection);
        }

        [Test]
        public void Match_ReturnsConstraintResutl()
        {
            Assert.That(new PredicateRouteConstraint(o => true).Match(httpContext, route, parameterName, new RouteValueDictionary { { parameterName, null } }, routeDirection), Is.True);

            Assert.That(new PredicateRouteConstraint(o => false).Match(httpContext, route, parameterName, new RouteValueDictionary { { parameterName, null } }, routeDirection), Is.False);
        }
    }
    // ReSharper restore InconsistentNaming
}
