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
        public void CurrentUrl_EmptyRoute_ReturnsEmptyString()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();
            Assert.That(urlHelper.CurrentUrl(), Is.Empty);
        }
        #endregion

        #region CurrentUrl and CurrentAbsoluteUrl() (Common tests)
        // TODO-IG: Key in the current query string is null or empty.
        // TODO-iG: Value in the current query string is null or empty.
        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_UrlHelperIsNull_ThrowsException()
        {
            var parameterName = Assert.Throws<ArgumentNullException>(() => UrlHelperExtensions.CurrentUrl(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper"));

            parameterName = Assert.Throws<ArgumentNullException>(() => UrlHelperExtensions.CurrentAbsoluteUrl(null, Protocol.Http, new Func<object, object>[0])).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_UrlHelperRequestContextIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper();
            // It is not possible to set UrlHelper.RequestContext to null and at the same time to have RouteCollection being not null.
            // Therefor this little trick to pass the null check for the urlHelper.RouteCollection.

            urlHelper.GetType().GetProperty("RouteCollection").GetSetMethod(true).Invoke(urlHelper, new object[] { new RouteCollection() });
            Assert.That(urlHelper.RouteCollection, Is.Not.Null);

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentUrl(new Func<object, object>[0])).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper.RequestContext"));

            parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentAbsoluteUrl(Protocol.Http, new Func<object, object>[0])).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper.RequestContext"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_NewRouteAndQueryStringParametersIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper(new RequestContext(), new RouteCollection());

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentUrl(null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("newRouteAndQueryStringParameters"));

            parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentAbsoluteUrl(Protocol.Http, null)).ParamName;
            Assert.That(parameterName, Is.EqualTo("newRouteAndQueryStringParameters"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithoutParameters_NoNewRouteParameters__ReturnsOriginalUrl()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", "route-without-parameters");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/route-without-parameters"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/route-without-parameters"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithParameters_NoNewRouteParameters__ReturnsOriginalUrl()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/en-US/1999"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/en-US/1999"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithoutParameters_NewRouteParameters__ParametersAddedToQueryString()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", "route-without-parameters");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("/route-without-parameters?p1=p1&p2=1&p3=1.23"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("http://localhost/route-without-parameters?p1=p1&p2=1&p3=1.23"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithParameters_NewRouteParametersThatDoNotExist__ParametersAddedToQueryString()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("/en-US/1999?p1=p1&p2=1&p3=1.23"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("http://localhost/en-US/1999?p1=p1&p2=1&p3=1.23"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithParameters_NewRouteParametersThatAlreadyExist__ParametersOverriddenInTheUrl()
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

            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "hr-HR"), Is.EqualTo("http://localhost/hr-HR/1999"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(year => 2000), Is.EqualTo("http://localhost/en-US/2000"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "hr-HR", year => 2000), Is.EqualTo("http://localhost/hr-HR/2000"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithParameters_NewRouteParametersThatAlreadyExistAndThatDoNotExist__ExistingParametersOverriddenInTheUrl_NonExistingParametersAddedToQueryString()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(language => "hr-HR", year => 2000, p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("/hr-HR/2000?p1=p1&p2=1&p3=1.23"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "hr-HR", year => 2000, p1 => "p1", p2 => 1, p3 => 1.23), Is.EqualTo("http://localhost/hr-HR/2000?p1=p1&p2=1&p3=1.23"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithParameters_ParameterWithSameNameInQueryString__RouteParameterRemainTheSame()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection {{"language", "hr-HR"}},
                RequestPath = "/en-US/1999?language=hr-HR"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/en-US/1999?language=hr-HR"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/en-US/1999?language=hr-HR"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithParameters_ParameterWithSameNameInQueryString__QueryStringParameterRemainInTheQueryString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "language", "hr-HR" } },
                RequestPath = "/en-US/1999/?language=hr-HR"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/en-US/1999?language=hr-HR"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/en-US/1999?language=hr-HR"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithParameters_ParameterWithSameNameInQueryStringAndInNewRouteParameters__ParametersOverriddenOnlyInTheQueryString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "language", "hr-HR" }, { "year", "2000" } },
                RequestPath = "/en-US/1999/?language=hr-HR&year=2000"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            // The order of the parameters in the query string is not important.
            Assert.That(urlHelper.CurrentUrl(language => "fr-FR").StartsWith("/en-US/1999?"));
            Assert.That(urlHelper.CurrentUrl(language => "fr-FR").Contains("language=fr-FR"));
            Assert.That(urlHelper.CurrentUrl(language => "fr-FR").Contains("year=2000"));
            Assert.That(urlHelper.CurrentUrl(language => "fr-FR").Contains("&"));
            Assert.That(!urlHelper.CurrentUrl(language => "fr-FR").EndsWith("&"));

        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_RouteParametesrAreCaseInsensitive()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(language => "hr-HR"), Is.EqualTo("/hr-HR"));
            Assert.That(urlHelper.CurrentUrl(LANGUAGE => "hr-HR"), Is.EqualTo("/hr-HR"));
            Assert.That(urlHelper.CurrentUrl(LaNgUaGe => "hr-HR"), Is.EqualTo("/hr-HR"));

            Assert.That(urlHelper.CurrentAbsoluteUrl(language => "hr-HR"), Is.EqualTo("http://localhost/hr-HR"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(LANGUAGE => "hr-HR"), Is.EqualTo("http://localhost/hr-HR"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(LaNgUaGe => "hr-HR"), Is.EqualTo("http://localhost/hr-HR"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_QueryStringParametersAreCaseInsensitive()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "parameter", "value" } },
                RequestPath = "/en-US/1999/?parameter=value"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(parameter => "newValue"), Is.EqualTo("/en-US/1999?parameter=newValue"));
            Assert.That(urlHelper.CurrentUrl(PARAMETER => "newValue"), Is.EqualTo("/en-US/1999?PARAMETER=newValue"));
            Assert.That(urlHelper.CurrentUrl(PARAMETER => "newValue"), Is.EqualTo("/en-US/1999?PARAMETER=newValue"));

            Assert.That(urlHelper.CurrentAbsoluteUrl(parameter => "newValue"), Is.EqualTo("http://localhost/en-US/1999?parameter=newValue"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(PARAMETER => "newValue"), Is.EqualTo("http://localhost/en-US/1999?PARAMETER=newValue"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(pArAmEtEr => "newValue"), Is.EqualTo("http://localhost/en-US/1999?pArAmEtEr=newValue"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_ReturnsUrlWithEncodedParameters()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithEncodedParameter", "{parameter}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(parameter => " &?!.,:"), Is.EqualTo("/%20%26%3f!.%2c%3a"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(parameter => " &?!.,:"), Is.EqualTo("http://localhost/%20%26%3f!.%2c%3a"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_QueryStringWithDuplicateParameterEntries_ExistingQueryStringIsPreserved()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "p1", "1" }, { "p1", "2" }, { "p1", "3" } },
                RequestPath = "/?p1=1&p1=2&p1=3"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            RouteData routeData = new RouteData();

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/?p1=1&p1=2&p1=3"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/?p1=1&p1=2&p1=3"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_QueryStringWithoutDuplicateParameterEntries_ExistingQueryStringIsPreserved()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {                
                QueryString = new NameValueCollection { { "p1", "1" }, { "p2", "2.0" }, { "p3", "text" } },
                RequestPath = "/?p1=1&p2=2.0&p3=text"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            RouteData routeData = new RouteData();

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/?p1=1&p2=2.0&p3=text"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/?p1=1&p2=2.0&p3=text"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_RouteParameterSetToNull_ThrowsException()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{routeParameter}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(routeParameter => null));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to null."));
            Assert.That(exception.Message.Contains("'routeParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(routeParameter => null));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to null."));
            Assert.That(exception.Message.Contains("'routeParameter'"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_RouteParameterSetToEmptyString_ThrowsException()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{routeParameter}");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(new RouteData(), routeCollection);

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(routeParameter => string.Empty));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to empty string."));
            Assert.That(exception.Message.Contains("'routeParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(routeParameter => string.Empty));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to empty string."));
            Assert.That(exception.Message.Contains("'routeParameter'"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_ExistingQueryStringParameterSetToNull_ThrowsException()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "queryStringParameter", "1" } },
                RequestPath = "/?urlParameter=1"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(queryStringParameter => null));
            Console.WriteLine(exception.Message);
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to null."));
            Assert.That(exception.Message.Contains("'queryStringParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(queryStringParameter => null));
            Console.WriteLine(exception.Message);
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to null."));
            Assert.That(exception.Message.Contains("'queryStringParameter'"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_ExistingQueryStringParameterSetToEmptyString_ThrowsException()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "queryStringParameter", "1" } },
                RequestPath = "/?urlParameter=1"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(queryStringParameter => string.Empty));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to empty string."));
            Assert.That(exception.Message.Contains("'queryStringParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(queryStringParameter => string.Empty));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to empty string."));
            Assert.That(exception.Message.Contains("'queryStringParameter'"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_NewQueryStringParameterSetToNull_ThrowsException()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection()
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(newQueryStringParameter => null));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to null."));
            Assert.That(exception.Message.Contains("'newQueryStringParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(newQueryStringParameter => null));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to null."));
            Assert.That(exception.Message.Contains("'newQueryStringParameter'"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_NewQueryStringParameterSetToEmpty_ThrowsException()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection()
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(newQueryStringParameter => string.Empty));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to empty string."));
            Assert.That(exception.Message.Contains("'newQueryStringParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(newQueryStringParameter => string.Empty));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("A route or query string parameter cannot be set to empty string."));
            Assert.That(exception.Message.Contains("'newQueryStringParameter'"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_RouteParameterSetToUrlParameterOptional_RouteParameterRemovedFromUrl()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}/{month}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", 2000);
            routeData.Values.Add("month", UrlParameter.Optional);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/en-US/2000/"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/en-US/2000/"));

            // The routes constructed below does not make any sense.
            // Still, it must be possible to put all parameters to optional and get nonsense.
            // We also have to test if nonsense URLs are properly generated :-)
            routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", UrlParameter.Optional);
            routeData.Values.Add("month", UrlParameter.Optional);

            urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("/en-US//"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost/en-US//"));

            routeData = new RouteData();
            routeData.Values.Add("language", UrlParameter.Optional);
            routeData.Values.Add("year", UrlParameter.Optional);
            routeData.Values.Add("month", UrlParameter.Optional);

            urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(), Is.EqualTo("///"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(), Is.EqualTo("http://localhost///"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_ExistingQueryStringParameterSetToUrlParameterOptional_ParameterRemovedFromQueryString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "p1", "1" }, { "p2", "2" }, { "p3", "3" } },
                RequestPath = "/?p1=1&p2=2&p3=3"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(p1 => UrlParameter.Optional), Is.EqualTo("/?p2=2&p3=3"));
            Assert.That(urlHelper.CurrentUrl(p2 => UrlParameter.Optional), Is.EqualTo("/?p1=1&p3=3"));
            Assert.That(urlHelper.CurrentUrl(p3 => UrlParameter.Optional), Is.EqualTo("/?p1=1&p2=2"));
            Assert.That(urlHelper.CurrentUrl(p1 => UrlParameter.Optional, p2 => UrlParameter.Optional), Is.EqualTo("/?p3=3"));
            Assert.That(urlHelper.CurrentUrl(p1 => UrlParameter.Optional, p3 => UrlParameter.Optional), Is.EqualTo("/?p2=2"));
            Assert.That(urlHelper.CurrentUrl(p2 => UrlParameter.Optional, p3 => UrlParameter.Optional), Is.EqualTo("/?p1=1"));
            Assert.That(urlHelper.CurrentUrl(p1 => UrlParameter.Optional, p2 => UrlParameter.Optional, p3 => UrlParameter.Optional), Is.EqualTo("/"));

            Assert.That(urlHelper.CurrentAbsoluteUrl(p1 => UrlParameter.Optional), Is.EqualTo("http://localhost/?p2=2&p3=3"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p2 => UrlParameter.Optional), Is.EqualTo("http://localhost/?p1=1&p3=3"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p3 => UrlParameter.Optional), Is.EqualTo("http://localhost/?p1=1&p2=2"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p1 => UrlParameter.Optional, p2 => UrlParameter.Optional), Is.EqualTo("http://localhost/?p3=3"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p1 => UrlParameter.Optional, p3 => UrlParameter.Optional), Is.EqualTo("http://localhost/?p2=2"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p2 => UrlParameter.Optional, p3 => UrlParameter.Optional), Is.EqualTo("http://localhost/?p1=1"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p1 => UrlParameter.Optional, p2 => UrlParameter.Optional, p3 => UrlParameter.Optional), Is.EqualTo("http://localhost/"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithoutParameters_NewQueryStringParameterSetToUrlParameterOptional__ParameterWithoutValueAddedToQueryString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection()
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(newParameter => UrlParameter.Optional), Is.EqualTo("/"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(newParameter => UrlParameter.Optional), Is.EqualTo("http://localhost/"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl__RouteWithParameters_NewQueryStringParameterSetToUrlParameterOptional__ParameterWithoutValueAddedToQueryString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection()
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", "1999");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(newParameter => UrlParameter.Optional), Is.EqualTo("/en-US/1999"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(newParameter => UrlParameter.Optional), Is.EqualTo("http://localhost/en-US/1999"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_ExistingNonEnumerableQueryStringParameterSetToEnumerableParameter_ExistingParameterReplacedWithEnumerableParameter()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "existing", "0" } },
                RequestPath = "/?existing=0"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(existing => new[] { 1, 2, 3 }), Is.EqualTo("/?existing=1&existing=2&existing=3"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(existing => new[] { 1, 2, 3 }), Is.EqualTo("http://localhost/?existing=1&existing=2&existing=3"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_ExistingEnumerableQueryStringParameterSetToEnumerableParameter_AllExistingEnumerableValuesReplacedWithNewEnumerableValues()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "existing", "0" }, { "existing", "1" }, { "existing", "2" } },
                RequestPath = "/?existing=0&existing=1&existing=2"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(existing => new[] { 3, 4, 5 }), Is.EqualTo("/?existing=3&existing=4&existing=5"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(existing => new[] { 3, 4, 5 }), Is.EqualTo("http://localhost/?existing=3&existing=4&existing=5"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_ExistingEnumerableQueryStringParameterSetToNonEnumerableParameter_AllExistingEnumerableValuesReplacedNewParameter()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "existing", "0" }, { "existing", "1" }, { "existing", "2" } },
                RequestPath = "/?existing=0&existing=1&existing=2"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(existing => 0), Is.EqualTo("/?existing=0"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(existing => 0), Is.EqualTo("http://localhost/?existing=0"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_ExistingEnumerableQueryStringParameterValuesOverlapWithNewEnumerableParameterValues_OverlappedValuesRemainInQueryString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "existing", "0" }, { "existing", "1" }, { "existing", "2" } },
                RequestPath = "/?existing=0&existing=1&existing=2"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(existing => new[] { 1, 2, 3, 4 }), Is.EqualTo("/?existing=1&existing=2&existing=3&existing=4"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(existing => new[] { 1, 2, 3, 4 }), Is.EqualTo("http://localhost/?existing=1&existing=2&existing=3&existing=4"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_EnumerableQueryStringParameterValuesContainUrlParameterOptional_OptionalParameterIsIgnored()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection()
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(p => new[] { UrlParameter.Optional }), Is.EqualTo("/"));
            Assert.That(urlHelper.CurrentUrl(p => new object[] { 1, UrlParameter.Optional }), Is.EqualTo("/?p=1"));
            Assert.That(urlHelper.CurrentUrl(p => new object[] { UrlParameter.Optional, 1 }), Is.EqualTo("/?p=1"));
            Assert.That(urlHelper.CurrentUrl(p => new object[] { 1, UrlParameter.Optional, 2 }), Is.EqualTo("/?p=1&p=2"));

            Assert.That(urlHelper.CurrentAbsoluteUrl(p => new[] { UrlParameter.Optional }), Is.EqualTo("http://localhost/"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p => new object[] { 1, UrlParameter.Optional }), Is.EqualTo("http://localhost/?p=1"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p => new object[] { UrlParameter.Optional, 1 }), Is.EqualTo("http://localhost/?p=1"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p => new object[] { 1, UrlParameter.Optional, 2 }), Is.EqualTo("http://localhost/?p=1&p=2"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_EnumerableQueryStringParameterValuesContainNull_ThrowsException()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection()
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(enumerableParameter => new object[] { null }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be null."));
            Assert.That(exception.Message.Contains("0"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(enumerableParameter => new object[] { null, 1 }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be null."));
            Assert.That(exception.Message.Contains("0"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(enumerableParameter => new object[] { 1, null }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be null."));
            Assert.That(exception.Message.Contains("1"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));


            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(enumerableParameter => new object[] { null }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be null."));
            Assert.That(exception.Message.Contains("0"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(enumerableParameter => new object[] { null, 1 }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be null."));
            Assert.That(exception.Message.Contains("0"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(enumerableParameter => new object[] { 1, null }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be null."));
            Assert.That(exception.Message.Contains("1"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_EnumerableQueryStringParameterValuesContainEmptyString_ThrowsException()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection()
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(enumerableParameter => new object[] { string.Empty }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be empty string."));
            Assert.That(exception.Message.Contains("0"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(enumerableParameter => new object[] { string.Empty, 1 }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be empty string."));
            Assert.That(exception.Message.Contains("0"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(enumerableParameter => new object[] { 1, string.Empty }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be empty string."));
            Assert.That(exception.Message.Contains("1"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));


            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(enumerableParameter => new object[] { string.Empty }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be empty string."));
            Assert.That(exception.Message.Contains("0"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(enumerableParameter => new object[] { string.Empty, 1 }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be empty string."));
            Assert.That(exception.Message.Contains("0"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));

            exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentAbsoluteUrl(enumerableParameter => new object[] { 1, string.Empty }));
            Console.WriteLine(exception.Message);
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("The item on the zero-index"));
            Assert.That(exception.Message.Contains("An enumerable item cannot be empty string."));
            Assert.That(exception.Message.Contains("1"));
            Assert.That(exception.Message.Contains("'enumerableParameter'"));
        }

        /// <summary>
        /// Enumerable parameters are always added to the query string.
        /// </summary>
        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_EnumerableParameterDoesNotExistInQueryString_ParameterWithSameNameExistsInRouteParameters_EnumerableParameterAddedToQueryString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection(),
                RequestPath = "/routeEnumerableParameter"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{enumerableParameter}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("enumerableParameter", "routeEnumerableParameter");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(enumerableParameter => new[] { 1, 2 }), Is.EqualTo("/routeEnumerableParameter?enumerableParameter=1&enumerableParameter=2"));
            Assert.That(urlHelper.CurrentAbsoluteUrl(enumerableParameter => new[] { 1, 2 }), Is.EqualTo("http://localhost/routeEnumerableParameter?enumerableParameter=1&enumerableParameter=2"));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_NewRouteAndQueryStringParameterSpecifiedMoreThanOnce_ThrowsException()
        {
            var urlHelper = MvcTestHelper.GetUrlHelper();

            var exception = Assert.Throws<ArgumentException>(() => urlHelper.CurrentUrl(firstRepeating => 0, firstRepeating => 0, nonRepeating => 0, secondRepeating => 0, secondRepeating => 0));
            Assert.That(exception.ParamName, Is.EqualTo("newRouteAndQueryStringParameters"));
            Assert.That(exception.Message.Contains("Each new route and query string parameter can be specified only once."));
            Assert.That(exception.Message.Contains("firstRepeating"));
            Assert.That(exception.Message.Contains("secondRepeating"), Is.Not.True);
            Assert.That(exception.Message.Contains("nonRepeating"), Is.Not.True);
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_NewQueryStringParameterConvertedToString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { {"p", "0" } },
                RequestPath = "/?p=0"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(p => new TestClassThatOverridesToString()), Is.EqualTo("/?p=" + TestClassThatOverridesToString.ToStringText));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p => new TestClassThatOverridesToString()), Is.EqualTo("http://localhost/?p=" + TestClassThatOverridesToString.ToStringText));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_NewEnumerableQueryStringParameterItemsConvertedToString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection { { "p", "0" } },
                RequestPath = "/?p=0"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithoutParameters", string.Empty);

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, new RouteData(), routeCollection);

            Assert.That(urlHelper.CurrentUrl(p => new[] { new TestClassThatOverridesToString(), new TestClassThatOverridesToString() }), Is.EqualTo(string.Format("/?p={0}&p={0}", TestClassThatOverridesToString.ToStringText)));
            Assert.That(urlHelper.CurrentAbsoluteUrl(p => new[] { new TestClassThatOverridesToString(), new TestClassThatOverridesToString() }), Is.EqualTo(string.Format("http://localhost/?p={0}&p={0}", TestClassThatOverridesToString.ToStringText)));
        }

        [Test]
        public void CurrentUrlAndCurrentAbsoluteUrl_NewRouteParameterConvertedToString()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                QueryString = new NameValueCollection(),
                RequestPath = "/parameterValue"
            };

            var httpContext = MvcTestHelper.GetHttpContext(httpContextDefinition);

            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{parameter}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("parameter", "parameterValue");

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(httpContext, routeData, routeCollection);

            Assert.That(urlHelper.CurrentUrl(parameter => new TestClassThatOverridesToString()), Is.EqualTo("/" + TestClassThatOverridesToString.ToStringText));
            Assert.That(urlHelper.CurrentAbsoluteUrl(parameter => new TestClassThatOverridesToString()), Is.EqualTo("http://localhost/" + TestClassThatOverridesToString.ToStringText));
        }

        private class TestClassThatOverridesToString
        {
            internal const string ToStringText = "ToStringText";

            public override string ToString()
            {
                return ToStringText;
            }
        }
        #endregion

        #region CurrentAbsoluteUrl
        [Test]
        public void CurrentAbsoluteUrl_UrlHelperRouteCollectionIsNull_ThrowsException()
        {
            var urlHelper = new UrlHelper();

            var parameterName = Assert.Throws<ArgumentNullException>(() => urlHelper.CurrentAbsoluteUrl(Protocol.Http, new Func<object, object>[0])).ParamName;
            Assert.That(parameterName, Is.EqualTo("urlHelper.RouteCollection"));
        }

        [Test]
        public void CurrentAbsoluteUrl_RouteWithParametersRouteValuesNotAllDefined_ThrowsException()
        {
            RouteCollection routeCollection = new RouteCollection();
            routeCollection.MapRoute("RouteWithParameters", "{language}/{year}/{month}");

            RouteData routeData = new RouteData();
            routeData.Values.Add("language", "en-US");
            routeData.Values.Add("year", 2000);
            // The "month" parameter is missing.

            UrlHelper urlHelper = MvcTestHelper.GetUrlHelper(routeData, routeCollection);

            var exception = Assert.Throws<InvalidOperationException>(() => urlHelper.CurrentAbsoluteUrl());
            Assert.That(exception.Message.Contains("cannot be generated"));
            Assert.That(exception.Message.Contains("current route"));
            Assert.That(exception.Message.Contains("language"));
            Assert.That(exception.Message.Contains("en-US"));
            Assert.That(exception.Message.Contains("year"));
            Assert.That(exception.Message.Contains("2000"));
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
