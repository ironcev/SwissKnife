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
            return GetHttpContext(new TestHttpContextDefinition { WebSiteUrl = webSiteUrl });
        }

        internal static HttpContextBase GetHttpContext()
        {
            return GetHttpContext(new TestHttpContextDefinition());
        }

        internal static HttpContextBase GetHttpContext(TestHttpContextDefinition testHttpContextDefinition)
        {
            Mock<HttpContextBase> mockHttpContext = new Mock<HttpContextBase>();

            if (testHttpContextDefinition.HasApplicationPath)
            {
                mockHttpContext.Setup(o => o.Request.ApplicationPath).Returns(testHttpContextDefinition.ApplicationPath);
                mockHttpContext.Setup(o => o.Request.RawUrl).Returns(testHttpContextDefinition.ApplicationPath);
            }

            if (testHttpContextDefinition.HasRequestPath)
            {
                mockHttpContext.Setup(o => o.Request.AppRelativeCurrentExecutionFilePath).Returns(testHttpContextDefinition.RequestPath);
            }


            mockHttpContext.Setup(o => o.Request.Url).Returns(testHttpContextDefinition.RequestUrl);

            mockHttpContext.Setup(o => o.Request.PathInfo).Returns(string.Empty);
            if (testHttpContextDefinition.HttpMethod.HasValue)
            {
                mockHttpContext.Setup(o => o.Request.HttpMethod).Returns(testHttpContextDefinition.HttpMethod.Value.ToString().ToUpperInvariant());
            }

            mockHttpContext.Setup(o => o.Session).Returns((HttpSessionStateBase)null);
            mockHttpContext.Setup(o => o.Response.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(r => r);
            mockHttpContext.Setup(o => o.Items).Returns(new Hashtable());
            return mockHttpContext.Object;
        }

        /// <summary>
        /// Gets the route data for the default "{controller}/{action}" route. That default route is "home/index".
        /// </summary>
        internal static RouteData GetDefaultRouteData()
        {
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "home");
            routeData.Values.Add("action", "index");
            return routeData;
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
