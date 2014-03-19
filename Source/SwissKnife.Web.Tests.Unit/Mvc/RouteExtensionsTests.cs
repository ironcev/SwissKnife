using System;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using SwissKnife.Web.Mvc;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class RouteExtensionsTests
    {
        private Route emptyRoute;

        [SetUp]
        public void SetUp()
        {
            emptyRoute = new Route(string.Empty, new Mock<IRouteHandler>().Object);
        }

        #region SetDefault (String)
        [Test]
        public void SetDefault_RouteIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => RouteExtensions.SetDefault(null, "parameter", new object())).ParamName;
            Assert.That(parameterName, Is.EqualTo("route"));
        }

        [Test]
        public void SetDefault_UrlParameterIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => emptyRoute.SetDefault(null, new object())).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlParameter"));
        }

        [Test]
        public void SetDefault_UrlParameterIsEmpty_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => emptyRoute.SetDefault(string.Empty, new object()));
            Assert.That(exception.ParamName, Is.EqualTo("urlParameter"));
            Assert.That(exception.Message.Contains("empty string"));
        }

        [Test]
        public void SetDefault_UrlParameterIsWhitespace_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => emptyRoute.SetDefault(" ", new object()));
            Assert.That(exception.ParamName, Is.EqualTo("urlParameter"));
            Assert.That(exception.Message.Contains("white space"));
        }

        [Test]
        public void SetDefault_DefaultValueIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => emptyRoute.SetDefault("parameter", null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("defaultValue"));
        }

        [Test]
        public void SetDefault_DefaultsWasNull_SetDefault()
        {
            Route route = new Route("{language}", new Mock<IRouteHandler>().Object);
            route.Defaults = null;
            Assert.That(route.Defaults, Is.Null);

            route.SetDefault("language", "en-US");

            Assert.That(route.Defaults, Is.Not.Null);
            Assert.That(route.Defaults.Count, Is.EqualTo(1));
            Assert.That(route.Defaults.ContainsKey("language"));
            Assert.That(route.Defaults["language"], Is.EqualTo("en-US"));
        }

        [Test]
        public void SetDefault_DefaultsWasNotNull_SetDefault()
        {
            Route route = new Route("{language}", new Mock<IRouteHandler>().Object);
            route.Defaults = new RouteValueDictionary();
            Assert.That(route.Defaults, Is.Not.Null);
            Assert.That(route.Defaults.Count, Is.EqualTo(0));

            route.SetDefault("language", "en-US");

            Assert.That(route.Defaults, Is.Not.Null);
            Assert.That(route.Defaults.Count, Is.EqualTo(1));
            Assert.That(route.Defaults.ContainsKey("language"));
            Assert.That(route.Defaults["language"], Is.EqualTo("en-US"));
        }

        [Test]
        public void SetDefault_ReturnsOriginalRoute()
        {
            Route route = new Route("{language}", new Mock<IRouteHandler>().Object);
            Assert.That(route.SetDefault("language", "en-US"), Is.SameAs(route));
        }

        [Test]
        public void SetDefault_OverwritesExistingDefaultValue()
        {
            Route route = new Route("{language}", new Mock<IRouteHandler>().Object);
            route.Defaults = new RouteValueDictionary { { "language", "hr-HR" } };
            Assert.That(route.Defaults["language"], Is.EqualTo("hr-HR"));

            route.SetDefault("language", "en-US");

            Assert.That(route.Defaults.Count, Is.EqualTo(1));
            Assert.That(route.Defaults.ContainsKey("language"));
            Assert.That(route.Defaults["language"], Is.EqualTo("en-US"));
        }

        [Test]
        public void SetDefault_CanBeChained()
        {
            Route route = new Route("{language}/{year}", new Mock<IRouteHandler>().Object);

            route.SetDefault("language", "en-US").SetDefault("year", 2014);

            Assert.That(route.Defaults["language"], Is.EqualTo("en-US"));
            Assert.That(route.Defaults["year"], Is.EqualTo(2014));
        }        
        #endregion

        #region SetDefault (Lambda Expression)
        [Test]
        public void SetDefaultLambda_RouteIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => RouteExtensions.SetDefault(null, parameter => new object())).ParamName;
            Assert.That(parameterName, Is.EqualTo("route"));
        }

        [Test]
        public void SetDefaultLambda_UrlParameterAndDefaultValueIsNull_ThrowsException()
        {
            string parameterName = Assert.Throws<ArgumentNullException>(() => emptyRoute.SetDefault(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlParameterAndDefaultValue"));
        }

        [Test]
        public void SetDefaultLambda_CalculatedDefaultValueIsNull_ThrowsException()
        {
            Route route = new Route("{language}", new Mock<IRouteHandler>().Object);
            var exception = Assert.Throws<ArgumentException>(() => emptyRoute.SetDefault(language => null));
            Assert.That(exception.ParamName, Is.EqualTo("urlParameterAndDefaultValue"));
            Assert.That(exception.Message.StartsWith("The calculated default value must not be null."));
        }

        [Test]
        public void SetDefaultLambda_DefaultsWasNull_SetDefault()
        {
            Route route = new Route("{language}", new Mock<IRouteHandler>().Object);
            route.Defaults = null;
            Assert.That(route.Defaults, Is.Null);

            route.SetDefault(language => "en-US");

            Assert.That(route.Defaults, Is.Not.Null);
            Assert.That(route.Defaults.Count, Is.EqualTo(1));
            Assert.That(route.Defaults.ContainsKey("language"));
            Assert.That(route.Defaults["language"], Is.EqualTo("en-US"));
        }

        [Test]
        public void SetDefaultLambda_DefaultsWasNotNull_SetDefault()
        {
            Route route = new Route("{language}", new Mock<IRouteHandler>().Object);
            route.Defaults = new RouteValueDictionary();
            Assert.That(route.Defaults, Is.Not.Null);
            Assert.That(route.Defaults.Count, Is.EqualTo(0));

            route.SetDefault(language => "en-US");

            Assert.That(route.Defaults, Is.Not.Null);
            Assert.That(route.Defaults.Count, Is.EqualTo(1));
            Assert.That(route.Defaults.ContainsKey("language"));
            Assert.That(route.Defaults["language"], Is.EqualTo("en-US"));
        }

        [Test]
        public void SetDefaultLambda_ReturnsOriginalRoute()
        {
            Route route = new Route("{language}", new Mock<IRouteHandler>().Object);
            Assert.That(route.SetDefault(language => "en-US"), Is.SameAs(route));
        }

        [Test]
        public void SetDefaultLambda_OverwritesExistingDefaultValue()
        {
            Route route = new Route("{language}", new Mock<IRouteHandler>().Object);
            route.Defaults = new RouteValueDictionary { { "language", "hr-HR" } };
            Assert.That(route.Defaults["language"], Is.EqualTo("hr-HR"));

            route.SetDefault(language => "en-US");

            Assert.That(route.Defaults.Count, Is.EqualTo(1));
            Assert.That(route.Defaults.ContainsKey("language"));
            Assert.That(route.Defaults["language"], Is.EqualTo("en-US"));
        }

        [Test]
        public void SetDefaultLambda_CanBeChained()
        {
            Route route = new Route("{language}/{year}", new Mock<IRouteHandler>().Object);

            route.SetDefault(language => "en-US").SetDefault(year => 2014);

            Assert.That(route.Defaults["language"], Is.EqualTo("en-US"));
            Assert.That(route.Defaults["year"], Is.EqualTo(2014));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
