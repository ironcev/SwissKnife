using System;
using System.Collections.Specialized;
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
        public void RouteAbsoluteUrl_RouteWithoutParameters_ReturnsAbsoluteRouteUrl()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", "route-without-parameters");
           
            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithoutParameters"), Is.EqualTo("http://localhost/route-without-parameters"));
            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithoutParameters").EndsWith(urlHelper.RouteUrl("RouteWithoutParameters")));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersRouteValuesObjectDefined_ReturnsAbsoluteRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", new { language = "en-US", year = 2000 }), Is.EqualTo("http://localhost/en-US/2000"));
            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", new { language = "en-US", year = 2000 }).EndsWith(urlHelper.RouteUrl("RouteWithParameters", new { language = "en-US", year = 2000 })));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersRouteValuesObjectDefinedDefaultRouteData_ReturnsAbsoluteRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(MvcTestHelper.GetDefaultRouteData(), routeCollection);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", new { language = "en-US", year = 2000 }), Is.EqualTo("http://localhost/en-US/2000"));
            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", new { language = "en-US", year = 2000 }).EndsWith(urlHelper.RouteUrl("RouteWithParameters", new { language = "en-US", year = 2000 })));
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
            // ReSharper disable AssignNullToNotNullAttribute
            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", routeValues).EndsWith(urlHelper.RouteUrl("RouteWithParameters", routeValues)));
            // ReSharper restore AssignNullToNotNullAttribute
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersProtocolDefined_ReturnsAbsoluteRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", new { language = "en-US", year = 2000 }, Protocol.Https), Is.EqualTo("https://localhost/en-US/2000"));
            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithParameters", new { language = "en-US", year = 2000 }, Protocol.Http), Is.EqualTo("http://localhost/en-US/2000"));
        }

        [Test]
        public void RouteAbsoluteUrl_ReturnsAbsoluteRouteWithEncodedParameters()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithEncodedParameter", "{parameter}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.RouteAbsoluteUrl("RouteWithEncodedParameter", new { parameter = " &?!.,:" }), Is.EqualTo("http://localhost/%20%26%3f!.%2c%3a"));
        }

        [Test]
        public void RouteAbsoluteUrl_RouteWithParametersRouteValuesEmpty_ThrowsException()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            var exception = Assert.Throws<InvalidOperationException>(() => urlHelper.RouteAbsoluteUrl("RouteWithParameters"));
            Assert.That(exception.Message.Contains("cannot be generated"));
            Assert.That(exception.Message.Contains("'RouteWithParameters'"));
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
            // "month" parameter is missing.

            var exception = Assert.Throws<InvalidOperationException>(() => urlHelper.RouteAbsoluteUrl("RouteWithParameters", routeValues));
            Assert.That(exception.Message.Contains("cannot be generated"));
            Assert.That(exception.Message.Contains("'RouteWithParameters'"));
            Console.WriteLine(exception.Message);
        }
        #endregion

        #region CurrentUrl
        [Test]
        public void CurrentUrl_UrlHelperIsNull_ThrowsException()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => UrlHelperExtensions.CurrentUrl(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper"));
        }

        [Test]
        public void CurrentUrl_UrlHelperRequestContextIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper();
            // It is not possible to set UrlHelper.RequestContext to null and at the same time to have RouteCollection being not null.
            // Therefor this little trick to pass the null check for the urlHelper.RouteCollection.

            urlHelper.GetType().GetProperty("RouteCollection").GetSetMethod(true).Invoke(urlHelper, new object[] { new RouteCollection() });
            Assert.That(urlHelper.RouteCollection, Is.Not.Null);

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentUrl(new Func<object, object>[0])).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper.RequestContext"));
        }

        [Test]
        public void CurrentUrl_NewRouteParametersIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper(new RequestContext(), new RouteCollection());

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentUrl(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("newRouteParameters"));
        }

        [Test]
        public void CurrentUrl_EmptyRoute_ReturnsEmptyString()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            Assert.That(urlHelper.CurrentUrl(), Is.Empty);
        }

        [Test]
        public void CurrentUrl__RouteWithoutParameters_NoNewRouteParameters__ReturnsOriginalRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "route-without-parameters");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/route-without-parameters"));
        }

        [Test]
        public void CurrentUrl__RouteWithParameters_NoNewRouteParameters__ReturnsOriginalRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/en-US/1999"));
        }

        [Test]
        public void CurrentUrl__RouteWithoutParameters_NewRouteParameters__ParametersAddedToQueryString()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "route-without-parameters");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("/route-without-parameters?p1=p1&p2=1&p3=1.23"));
        }

        [Test]
        public void CurrentUrl__RouteWithParameters_NewRouteParametersThatDoNotExist__ParametersAddedToQueryString()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("/en-US/1999?p1=p1&p2=1&p3=1.23"));
        }

        [Test]
        public void CurrentUrl__RouteWithParameters_NewRouteParametersThatAlreadyExist__ParametersOverriddenInTheUrl()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(language => "hr-HR"), Is.EqualTo("/hr-HR/1999"));

            Assert.That(urlHelper.CurrentUrl(year => 2000), Is.EqualTo("/en-US/2000"));

            Assert.That(urlHelper.CurrentUrl(language => "hr-HR", year => 2000), Is.EqualTo("/hr-HR/2000"));
        }

        [Test]
        public void CurrentUrl__RouteWithParameters_NewRouteParametersThatAlreadyExistAndThatDoNotExist__ExistingParametersOverriddenInTheUrl_NonExistingParametersAddedToQueryString()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(language => "hr-HR", year => 2000, p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("/hr-HR/2000?p1=p1&p2=1&p3=1.23"));
        }

        [Test]
        public void CurrentUrl__RouteWithParameters_ParameterWithSameNameInQueryString__ParametersOverriddenInTheUrl()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection {{"language", "hr-HR"}}
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/hr-HR/1999"));
        }

        [Test]
        public void CurrentUrl__RouteWithParameters_ParameterWithSameNameInQueryStringAndInNewRouteParameters__ParametersOverriddenInTheUrl_NewRouteParametersWin()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "language", "hr-HR" }, { "year", "2000" } }
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(language => "fr-FR"), Is.EqualTo("/fr-FR/2000"));
        }

        [Test]
        public void CurrentUrl_NewRouteParametersAreCaseInsensitive()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(language => "hr-HR"), Is.EqualTo("/hr-HR"));
            Assert.That(urlHelper.CurrentUrl(LANGUAGE => "hr-HR"), Is.EqualTo("/hr-HR"));
            Assert.That(urlHelper.CurrentUrl(LaNgUaGe => "hr-HR"), Is.EqualTo("/hr-HR"));
        }

        [Test]
        public void CurrentUrl_ParametersInTheQueryStringAreCaseInsensitive()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "LANGUAGE", "hr-HR" } }
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/hr-HR/1999"));
        }

        [Test]
        public void CurrentUrl_ReturnsCurrentUrlWithEncodedParameters()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithEncodedParameter", "{parameter}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(parameter => " &?!.,:"), Is.EqualTo("/%20%26%3f!.%2c%3a"));
        }
        #endregion

        #region CurrentAbsoluteUrl
        [Test]
        public void CurrentAbsoluteUrl_UrlHelperIsNull_ThrowsException()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => UrlHelperExtensions.CurrentAbsoluteUrl(null, Protocol.Http, new Func<object, object>[0])).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper"));
        }

        [Test]
        public void CurrentAbsoluteUrl_UrlHelperRouteCollectionIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper();

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentAbsoluteUrl(Protocol.Http, new Func<object, object>[0])).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper.RouteCollection"));
        }

        [Test]
        public void CurrentAbsoluteUrl_UrlHelperRequestContextIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper();
            // It is not possible to set UrlHelper.RequestContext to null and at the same time to have RouteCollection being not null.
            // Therefor this little trick to pass the null check for the urlHelper.RouteCollection.

            urlHelper.GetType().GetProperty("RouteCollection").GetSetMethod(true).Invoke(urlHelper, new object[] { new RouteCollection() });
            Assert.That(urlHelper.RouteCollection, Is.Not.Null);

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentAbsoluteUrl(Protocol.Http, new Func<object, object>[0])).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper.RequestContext"));
        }

        [Test]
        public void CurrentAbsoluteUrl_NewRouteParametersIsNull_ThrowsException()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentAbsoluteUrl(Protocol.Http, null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("newRouteParameters"));
        }

        [Test]
        public void CurrentAbsoluteUrl_RouteWithParametersRouteValuesNotAllDefined_ThrowsException()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}/{month}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            var routeValues = new RouteValueDictionary();
            routeValues.Add("language", "en-US");
            routeValues.Add("year", 2000);
            // "month" parameter is missing.

            var exception = Assert.Throws<InvalidOperationException>(() => urlHelper.CurrentAbsoluteUrl());
            Assert.That(exception.Message.Contains("cannot be generated"));
            Assert.That(exception.Message.Contains("current route"));
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void CurrentAbsoluteUrl_EmptyRoute_ReturnsSiteRootUrl()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("SiteRoot", "");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/"));
        }

        [Test]
        public void CurrentAbsoluteUrl__RouteWithoutParameters_NoNewRouteParameters__ReturnsOriginalAbsolutRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "route-without-parameters");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/route-without-parameters"));
        }

        [Test]
        public void CurrentAbsoluteUrl__RouteWithParameters_NoNewRouteParameters__ReturnsOriginalAbsoluteRoute()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/en-US/1999"));
        }

        [Test]
        public void CurrentAbsoluteUrl__RouteWithoutParameters_NewRouteParameters__ParametersAddedToQueryString()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "route-without-parameters");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("http://localhost/route-without-parameters?p1=p1&p2=1&p3=1.23"));
        }

        [Test]
        public void CurrentAbsoluteUrl__RouteWithParameters_NewRouteParametersThatDoNotExist__ParametersAddedToQueryString()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("http://localhost/en-US/1999?p1=p1&p2=1&p3=1.23"));
        }

        [Test]
        public void CurrentAbsoluteUrl__RouteWithParameters_NewRouteParametersThatAlreadyExist__ParametersOverriddenInTheUrl()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "hr-HR"), Is.EqualTo("http://localhost/hr-HR/1999"));

            Assert.That(urlHelper.CurrentAbsoluteUrl(year => 2000), Is.EqualTo("http://localhost/en-US/2000"));

            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "hr-HR", year => 2000), Is.EqualTo("http://localhost/hr-HR/2000"));
        }

        [Test]
        public void CurrentAbsoluteUrl__RouteWithParameters_NewRouteParametersThatAlreadyExistAndThatDoNotExist__ExistingParametersOverriddenInTheUrl_NonExistingParametersAddedToQueryString()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "hr-HR", year => 2000, p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("http://localhost/hr-HR/2000?p1=p1&p2=1&p3=1.23"));
        }

        [Test]
        public void CurrentAbsoluteUrl__RouteWithParameters_ParameterWithSameNameInQueryString__ParametersOverriddenInTheUrl()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "language", "hr-HR" } }
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/hr-HR/1999"));
        }

        [Test]
        public void CurrentAbsoluteUrl__RouteWithParameters_ParameterWithSameNameInQueryStringAndInNewRouteParameters__ParametersOverriddenInTheUrl_NewRouteParametersWin()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "language", "hr-HR" }, { "year", "2000" } }
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "fr-FR"), Is.EqualTo("http://localhost/fr-FR/2000"));
        }

        [Test]
        public void CurrentAbsoluteUrl_NewRouteParametersAreCaseInsensitive()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "hr-HR"), Is.EqualTo("http://localhost/hr-HR"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(LANGUAGE => "hr-HR"), Is.EqualTo("http://localhost/hr-HR"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(LaNgUaGe => "hr-HR"), Is.EqualTo("http://localhost/hr-HR"));
        }

        [Test]
        public void CurrentAbsoluteUrl_ParametersInTheQueryStringAreCaseInsensitive()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "LANGUAGE", "hr-HR" } }
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/hr-HR/1999"));
        }

        [Test]
        public void CurrentAbsoluteUrl_ReturnsCurrentUrlWithEncodedParameters()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithEncodedParameter", "{parameter}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentAbsoluteUrl(parameter => " &?!.,:"), Is.EqualTo("http://localhost/%20%26%3f!.%2c%3a"));
        }

        [Test]
        public void CurrentAbsoluteUrl_ProtocolDefined_OverridesTheOriginalProtocol()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("SiteRoot", "");

            var httpContextDefinition = new TestHttpContextDefinition
            {
                Protocol = Protocol.Http
            };

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(MvcTestHelper.GetHttpContext(httpContextDefinition), new RouteData(), routeCollection);
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.Scheme, Is.EqualTo("http"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.CurrentAbsoluteUrl(Protocol.Https), Is.EqualTo("https://localhost/"));


            httpContextDefinition = new TestHttpContextDefinition
            {
                Protocol = Protocol.Https
            };

            urlHelper = MvcTestHelper.GetUrlHelper(MvcTestHelper.GetHttpContext(httpContextDefinition), new RouteData(), routeCollection);
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.Scheme, Is.EqualTo("https"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.CurrentAbsoluteUrl(Protocol.Http), Is.EqualTo("http://localhost/"));
        }
        #endregion

        #region ToAbsoluteUrl
        [Test]
        public void ToAbsoluteUrl_UrlHelperIsNull_ThrowsException()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => UrlHelperExtensions.ToAbsoluteUrl(null, "/some/relative/url")).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper"));
        }

        [Test]
        public void ToAbsoluteUrl_UrlHelperRequestContextIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper();
            // It is not possible to set UrlHelper.RequestContext to null and at the same time to have RouteCollection being not null.
            // Therefor this little trick to pass the null check for the urlHelper.RouteCollection.

            urlHelper.GetType().GetProperty("RouteCollection").GetSetMethod(true).Invoke(urlHelper, new object[] { new RouteCollection() });
            Assert.That(urlHelper.RouteCollection, Is.Not.Null);

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.ToAbsoluteUrl("/some/relative/url")).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper.RequestContext"));
        }

        [Test]
        public void ToAbsoluteUrl_RelativeOrAbsoluteUrlIsNull_ThrowsException()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.ToAbsoluteUrl(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("relativeOrAbsoluteUrl"));
        }

        [Test]
        public void ToAbsoluteUrl_RelativeOrAbsoluteUrlIsEmpty_ThrowsException()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.ToAbsoluteUrl(string.Empty));
            Assert.That(exception.ParamName, Is.EqualTo("relativeOrAbsoluteUrl"));
            Assert.That(exception.Message.Contains("empty"));
        }

        [Test]
        public void ToAbsoluteUrl_RelativeOrAbsoluteUrlIsWhiteSpace_ThrowsException()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.ToAbsoluteUrl(" "));
            Assert.That(exception.ParamName, Is.EqualTo("relativeOrAbsoluteUrl"));
            Assert.That(exception.Message.Contains("white space"));
        }

        [Test]
        public void ToAbsoluteUrl_RelativeOrAbsoluteUrlIsNotValidUrl_ThrowsException()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.ToAbsoluteUrl("http://invalid-uri:12ab/"));
            Assert.That(exception.ParamName, Is.EqualTo("relativeOrAbsoluteUrl"));
            Assert.That(exception.Message.Contains("Relative or absolute URL does not represent a valid relative or absolute URL."));
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void ToAbsoluteUrl_IsAbsoluteUrlNotBelongingToTheWebSite_ReturnsCanonicalUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            Assert.That(urlHelper.ToAbsoluteUrl("http://WWW.AbsoluteUrl.COM"), Is.EqualTo("http://www.absoluteurl.com/"));
            Assert.That(urlHelper.ToAbsoluteUrl("http://WWW.AbsoluteUrl.COM/"), Is.EqualTo("http://www.absoluteurl.com/"));
        }

        [Test]
        public void ToAbsoluteUrl__IsAbsoluteUrlNotBelongingToTheWebSite_WithQuery__ReturnsCanonicalUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            Assert.That(urlHelper.ToAbsoluteUrl("http://WWW.AbsoluteUrl.COM?a=1&B=2"), Is.EqualTo("http://www.absoluteurl.com/?a=1&B=2"));
            Assert.That(urlHelper.ToAbsoluteUrl("http://WWW.AbsoluteUrl.COM/?a=1&B=2"), Is.EqualTo("http://www.absoluteurl.com/?a=1&B=2"));
        }

        [Test]
        public void ToAbsoluteUrl__IsAbsoluteUrlNotBelongingToTheWebSite_WithRelativePath__ReturnsCanonicalUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            Assert.That(urlHelper.ToAbsoluteUrl("http://WWW.AbsoluteUrl.COM/Some/Relative/Path"), Is.EqualTo("http://www.absoluteurl.com/Some/Relative/Path"));
        }

        [Test]
        public void ToAbsoluteUrl__IsAbsoluteUrlNotBelongingToTheWebSite_WithRelativePathAndQuery__ReturnsCanonicalUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            Assert.That(urlHelper.ToAbsoluteUrl("http://WWW.AbsoluteUrl.COM/Some/Relative/Path?a=1&B=2"), Is.EqualTo("http://www.absoluteurl.com/Some/Relative/Path?a=1&B=2"));
            Assert.That(urlHelper.ToAbsoluteUrl("http://WWW.AbsoluteUrl.COM/Some/Relative/Path/?a=1&B=2"), Is.EqualTo("http://www.absoluteurl.com/Some/Relative/Path/?a=1&B=2"));
        }

        [Test]
        public void ToAbsoluteUrl_IsAbsoluteUrlBelongingToTheWebSite_ReturnsCanonicalUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString().StartsWith("http://localhost"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.ToAbsoluteUrl("http://localhost"), Is.EqualTo("http://localhost/"));
            Assert.That(urlHelper.ToAbsoluteUrl("http://localhost/"), Is.EqualTo("http://localhost/"));
        }

        [Test]
        public void ToAbsoluteUrl__IsAbsoluteUrlBelongingToTheWebSite_WithQuery__ReturnsCanonicalUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString().StartsWith("http://localhost"));
            // ReSharper restore PossibleNullReferenceException


            Assert.That(urlHelper.ToAbsoluteUrl("http://localhost?a=1&B=2"), Is.EqualTo("http://localhost/?a=1&B=2"));
            Assert.That(urlHelper.ToAbsoluteUrl("http://localhost/?a=1&B=2"), Is.EqualTo("http://localhost/?a=1&B=2"));
        }

        [Test]
        public void ToAbsoluteUrl__IsAbsoluteUrlBelongingToTheWebSite_WithRelativePath__ReturnsCanonicalUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString().StartsWith("http://localhost"));
            // ReSharper restore PossibleNullReferenceException


            Assert.That(urlHelper.ToAbsoluteUrl("http://localhost/Some/Relative/Path"), Is.EqualTo("http://localhost/Some/Relative/Path"));
            Assert.That(urlHelper.ToAbsoluteUrl("HTTP://loCalhosT/Some/Relative/Path"), Is.EqualTo("http://localhost/Some/Relative/Path"));
            Assert.That(urlHelper.ToAbsoluteUrl("http://localhost/Some/Relative/Path/"), Is.EqualTo("http://localhost/Some/Relative/Path/"));
        }

        [Test]
        public void ToAbsoluteUrl__IsAbsoluteUrlBelongingToTheWebSite_WithRelativePathAndQuery__ReturnsCanonicalUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString().StartsWith("http://localhost"));
            // ReSharper restore PossibleNullReferenceException


            Assert.That(urlHelper.ToAbsoluteUrl("http://localhost/Some/Relative/Path?a=1&B=2"), Is.EqualTo("http://localhost/Some/Relative/Path?a=1&B=2"));
            Assert.That(urlHelper.ToAbsoluteUrl("HTTP://loCalhosT/Some/Relative/Path?a=1&B=2"), Is.EqualTo("http://localhost/Some/Relative/Path?a=1&B=2"));
            Assert.That(urlHelper.ToAbsoluteUrl("http://localhost/Some/Relative/Path/?a=1&B=2"), Is.EqualTo("http://localhost/Some/Relative/Path/?a=1&B=2"));
        }

        [Test]
        public void ToAbsoluteUrl_RelativeUrl_ReturnsAbsoluteUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString().StartsWith("http://localhost"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.ToAbsoluteUrl("relative/url"), Is.EqualTo("http://localhost/relative/url"));
        }

        [Test]
        public void ToAbsoluteUrl_RelativeUrlBeginsWithSlashOrBackslash_ReturnsAbsoluteUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString().StartsWith("http://localhost"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.ToAbsoluteUrl("/relative/url"), Is.EqualTo("http://localhost/relative/url"));
            Assert.That(urlHelper.ToAbsoluteUrl("\\relative/url"), Is.EqualTo("http://localhost/relative/url"));
            Assert.That(urlHelper.ToAbsoluteUrl("\\relative\\url"), Is.EqualTo("http://localhost/relative/url"));
        }

        [Test]
        public void ToAbsoluteUrl__RelativeUrl_WithQuery__ReturnsAbsoluteUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString().StartsWith("http://localhost"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.ToAbsoluteUrl("relative/url?a=1&B=2"), Is.EqualTo("http://localhost/relative/url?a=1&B=2"));
        }

        [Test]
        public void ToAbsoluteUrl__RelativeUrlBeginsWithSlashOrBackslash_WithQuery__ReturnsAbsoluteUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString().StartsWith("http://localhost"));
            // ReSharper restore PossibleNullReferenceException


            Assert.That(urlHelper.ToAbsoluteUrl("/relative/url?a=1&B=2"), Is.EqualTo("http://localhost/relative/url?a=1&B=2"));
            Assert.That(urlHelper.ToAbsoluteUrl("\\relative/url?a=1&B=2"), Is.EqualTo("http://localhost/relative/url?a=1&B=2"));
            Assert.That(urlHelper.ToAbsoluteUrl("\\relative\\url?a=1&B=2"), Is.EqualTo("http://localhost/relative/url?a=1&B=2"));
        }

        [Test]
        public void ToAbsoluteUrl_HttpRequestHasNoUrlDefined_ThrowsException()
        {
            var httpRequestDefinition = new TestHttpContextDefinition
            {
                SetRequestUrlToNull = true
            };

            var urlHelper = MvcTestHelper.GetUrlHelper(httpRequestDefinition);

            var exception = Assert.Throws<InvalidOperationException>(() => urlHelper.ToAbsoluteUrl("/some/url"));
            Assert.That(exception.Message.Contains("The HTTP request has no URL defined."));
        }

        [Test]
        public void ToAbsoluteUrl_ReturnsUnescapedUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString().StartsWith("http://localhost"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.ToAbsoluteUrl("/path with spaces, and commas"), Is.EqualTo("http://localhost/path with spaces, and commas"));
            Assert.That(urlHelper.ToAbsoluteUrl("path with spaces, and commas"), Is.EqualTo("http://localhost/path with spaces, and commas"));
        }

        [Test]
        public void ToAbsoluteUrl_RelativeUrlStartsWithSlash_ReturnsAbsoluteUrl()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                RequestPath = "/some/url/"
            };

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", "some/url");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContextDefinition, new RouteData(), routeCollection);

            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString(), Is.EqualTo("http://localhost/some/url/"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.ToAbsoluteUrl("/relative/url"), Is.EqualTo("http://localhost/relative/url"));
            Assert.That(urlHelper.ToAbsoluteUrl("/relative\\url"), Is.EqualTo("http://localhost/relative/url"));
        }

        [Test]
        public void ToAbsoluteUrl_RelativeUrlStartsWithBackslash_ReturnsAbsoluteUrl()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                RequestPath = "/some/url/"
            };

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", "some/url");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContextDefinition, new RouteData(), routeCollection);

            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString(), Is.EqualTo("http://localhost/some/url/"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.ToAbsoluteUrl("\\relative/url"), Is.EqualTo("http://localhost/relative/url"));
            Assert.That(urlHelper.ToAbsoluteUrl("\\relative\\url"), Is.EqualTo("http://localhost/relative/url"));
        }

        [Test]
        public void ToAbsoluteUrl_RelativeUrlStartsWithotSlashOrBackslash_ReturnsAbsoluteUrl()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                RequestPath = "/some/url/"
            };

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", "some/url");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContextDefinition, new RouteData(), routeCollection);

            // ReSharper disable PossibleNullReferenceException
            Assert.That(urlHelper.RequestContext.HttpContext.Request.Url.ToString(), Is.EqualTo("http://localhost/some/url/"));
            // ReSharper restore PossibleNullReferenceException

            Assert.That(urlHelper.ToAbsoluteUrl("relative/url"), Is.EqualTo("http://localhost/relative/url"));
            Assert.That(urlHelper.ToAbsoluteUrl("relative\\url"), Is.EqualTo("http://localhost/relative/url"));
        }
        #endregion

        #region Action
        [Test]
        public void Action_UrlHelperIsNull_ThrowsException()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => UrlHelperExtensions.Action<TestController>(null, c => c.ActionWithoutParameters())).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper"));
        }

        [Test]
        public void Action_ActionExpressionIsNull_ThrowsException()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.Action<TestController>(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("actionExpression"));
        }

        [Test]
        public void Action_ActionExpressionNotValid_ThrowsException()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.Action<TestController>(c => new EmptyResult()));
            Assert.That(exception.ParamName, Is.EqualTo("actionExpression"));
            Assert.That(exception.Message.Contains("Action expression is not a valid action expression."));
            Assert.That(exception.Message.Contains("'new EmptyResult()'"));
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void Action_ValidActionExpression_ReturnsActionUrl()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper(MvcTestHelper.GetDefaultRouteData(), MvcTestHelper.GetDefaultRouteCollection());

            Assert.That(urlHelper.Action<TestController>(c => c.ActionWithoutParameters()), Is.EqualTo("/Test/ActionWithoutParameters"));
        }

        [Test]
        public void Action__ValidActionExpression_RouteValues__ReturnsActionUrlWithQueryString()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper(MvcTestHelper.GetDefaultRouteData(), MvcTestHelper.GetDefaultRouteCollection());

            Assert.That(urlHelper.Action<TestController>(c => c.ActionWithoutParameters(), new { p1 = "firstValue", p2 = 1, p3 = 1.23 }), Is.EqualTo("/Test/ActionWithoutParameters?p1=firstValue&p2=1&p3=1.23"));
        }

        class TestController : Controller
        {
            // ReSharper disable MemberCanBeMadeStatic.Local
            public ActionResult ActionWithoutParameters() { return null; }
            // ReSharper restore MemberCanBeMadeStatic.Local
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
