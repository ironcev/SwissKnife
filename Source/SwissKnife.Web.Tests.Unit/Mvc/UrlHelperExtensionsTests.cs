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

            Assert.Throws<InvalidOperationException>(() => urlHelper.RouteAbsoluteUrl("RouteWithParameters"));
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

            Assert.Throws<InvalidOperationException>(() => urlHelper.RouteAbsoluteUrl("RouteWithParameters", routeValues));
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
    }
    // ReSharper restore InconsistentNaming
}
