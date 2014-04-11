using System;
using System.Collections;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    internal static class MvcTestHelper
    {
        internal static HtmlHelper GetHtmlHelper()
        {
            var httpContext = GetHttpContext();

            var viewDataDictionary = new ViewDataDictionary();

            var viewContext = new ViewContext
            {
                ViewData = viewDataDictionary,
                HttpContext = httpContext
            };

            var viewDataConteinerMock = new Mock<IViewDataContainer>();
            viewDataConteinerMock.Setup(x => x.ViewData).Returns(viewDataDictionary);

            return new HtmlHelper(viewContext, viewDataConteinerMock.Object);
        }

        internal static HttpContextBase GetHttpContext(string webSiteUrl)
        {
            return GetHttpContext(protocol: null,
                                  webSiteUrl: webSiteUrl,
                                  port: null,
                                  applicationPath: null,
                                  requestPath: null,
                                  httpMethod: null);
        }

        internal static HttpContextBase GetHttpContext()
        {
            return GetHttpContext(protocol: null,
                                  webSiteUrl: null,
                                  port: null,
                                  applicationPath: null,
                                  requestPath: null,
                                  httpMethod: null);
        }

        internal static HttpContextBase GetHttpContext(string protocol, string webSiteUrl, int? port, string applicationPath, string requestPath, string httpMethod)
        {
            Mock<HttpContextBase> mockHttpContext = new Mock<HttpContextBase>();

            if (!string.IsNullOrEmpty(applicationPath))
            {
                mockHttpContext.Setup(o => o.Request.ApplicationPath).Returns(applicationPath);
                mockHttpContext.Setup(o => o.Request.RawUrl).Returns(applicationPath);
            }

            if (!string.IsNullOrEmpty(requestPath))
            {
                mockHttpContext.Setup(o => o.Request.AppRelativeCurrentExecutionFilePath).Returns(requestPath);
            }

            protocol = string.IsNullOrEmpty(protocol) ? "http" : protocol;            
            webSiteUrl = string.IsNullOrEmpty(webSiteUrl) ? "localhost" : webSiteUrl;
            string portAsString = port.HasValue ? ":" + port : string.Empty;

            Uri uri = new Uri(string.Format("{0}://{1}{2}", protocol, webSiteUrl, portAsString));

            mockHttpContext.Setup(o => o.Request.Url).Returns(uri);

            mockHttpContext.Setup(o => o.Request.PathInfo).Returns(string.Empty);
            if (!string.IsNullOrEmpty(httpMethod))
            {
                mockHttpContext.Setup(o => o.Request.HttpMethod).Returns(httpMethod);
            }

            mockHttpContext.Setup(o => o.Session).Returns((HttpSessionStateBase)null);
            mockHttpContext.Setup(o => o.Response.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(r => r);
            mockHttpContext.Setup(o => o.Items).Returns(new Hashtable());
            return mockHttpContext.Object;
        }

        internal static RouteData GetDefaultRouteData()
        {
            RouteData rd = new RouteData();
            rd.Values.Add("controller", "home");
            rd.Values.Add("action", "index");
            return rd;
        }

        internal static UrlHelper GetUrlHelper()
        {
            return GetUrlHelper(GetHttpContext(), new RouteData(), new RouteCollection());
        }

        internal static UrlHelper GetUrlHelper(RouteData routeData, RouteCollection routeCollection)
        {
            return GetUrlHelper(GetHttpContext(), routeData, routeCollection);            
        }

        internal static UrlHelper GetUrlHelper(HttpContextBase httpContext, RouteData routeData, RouteCollection routeCollection)
        {
            return new UrlHelper(new RequestContext(httpContext, routeData), routeCollection);
        }
    }
}
