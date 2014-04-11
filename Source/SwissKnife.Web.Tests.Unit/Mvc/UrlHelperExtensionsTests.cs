using System;
using System.Web.Mvc;
using System.Web.Routing;
using NUnit.Framework;
using SwissKnife.Web.Mvc;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class UrlHelperExtensionsTests
    {
        #region RouteAbsoluteUrl
        [Test]
        public void RouteAbsoluteUrl_UrlHelperIsNull_ThrowsException()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => UrlHelperExtensions.RouteAbsoluteUrl(null, "routeName", new RouteValueDictionary(), Protocol.Http)).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper"));
        }

        [Test]
        public void RouteAbsoluteUrl_UrlHelperRouteCollectionIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper();

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.RouteAbsoluteUrl("routeName", new RouteValueDictionary(), Protocol.Http)).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper.RouteCollection"));
        }

        [Test]
        public void RouteAbsoluteUrl_UrlHelperRequestContextIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper();
            // It is not possible to set UrlHelper.RequestContext to null and at the same time to have RouteCollection being not null.
            // Therefor this little trick to pass the null check for the urlHelper.RouteCollection.

            urlHelper.GetType().GetProperty("RouteCollection").GetSetMethod(true).Invoke(urlHelper, new object [] { new RouteCollection() });
            Assert.That(urlHelper.RouteCollection, Is.Not.Null);

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.RouteAbsoluteUrl("routeName", new RouteValueDictionary(), Protocol.Http)).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper.RequestContext"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteNameIsNull_ThrowsException()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => MvcTestHelper.GetUrlHelper().RouteAbsoluteUrl(null, new RouteValueDictionary(), Protocol.Http)).ParamName;
            Assert.That(parameterName, Is.EqualTo("routeName"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteNameIsEmpty_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => MvcTestHelper.GetUrlHelper().RouteAbsoluteUrl(string.Empty, new RouteValueDictionary(), Protocol.Http));
            Assert.That(exception.ParamName, Is.EqualTo("routeName"));
            Assert.That(exception.Message.Contains("empty"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteNameIsWhiteSpace_ThrowsException()
        {
            var exception = Assert.Throws<ArgumentException>(() => MvcTestHelper.GetUrlHelper().RouteAbsoluteUrl(" ", new RouteValueDictionary(), Protocol.Http));
            Assert.That(exception.ParamName, Is.EqualTo("routeName"));
            Assert.That(exception.Message.Contains("white space"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithoutParameters_ReturnsAbsoluteRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", "route-without-parameters");
           
            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithoutParameters"), Is.EqualTo("http://localhost/route-without-parameters"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersRouteValuesObjectDefined_ReturnsAbsoluteRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", new { language = "en-US", year = 2000}), Is.EqualTo("http://localhost/en-US/2000"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersRouteValuesObjectDefinedDefaultRouteData_ReturnsAbsoluteRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(MvcTestHelper.GetDefaultRouteData(), routeCollection);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", new { language = "en-US", year = 2000 }), Is.EqualTo("http://localhost/en-US/2000"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersRouteValueDictionaryDefined_ReturnsAbsoluteRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            var routeValues = new RouteValueDictionary();
            routeValues.Add("language", "en-US");
            routeValues.Add("year", 2000);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", routeValues), Is.EqualTo("http://localhost/en-US/2000"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersProtocolDefined_ReturnsAbsoluteRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", new { language = "en-US", year = 2000 }, Protocol.Https), Is.EqualTo("https://localhost/en-US/2000"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersRouteValuesEmpty_ThrowsException()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            var exception = Assert.Throws<InvalidOperationException>(() => urlHelper.RouteAbsoluteUrl("RouteWithParameters"));
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersRouteValuesNotAllDefined_ThrowsException()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}/{month}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            var routeValues = new RouteValueDictionary();
            routeValues.Add("language", "en-US");
            routeValues.Add("year", 2000);

            var exception = Assert.Throws<InvalidOperationException>(() => urlHelper.RouteAbsoluteUrl("RouteWithParameters", routeValues));
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void CurrentUrl()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData rd = new RouteData();
            rd.Values.Add("language", "en-US");
            rd.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(rd, routeCollection);

            Assert.That(urlHelper.CurrentUrl(language => "hr-HR").ToString(), Is.EqualTo("/hr-HR/1999"));            
        }

        [Test]
        public void CurrentAbsoluteUrl()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData rd = new RouteData();
            rd.Values.Add("language", "en-US");
            rd.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(rd, routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "hr-HR").ToString(), Is.EqualTo("http://localhost/hr-HR/1999"));
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
