using System.Collections;
using System.Web;
using System.Web.Mvc;
using Moq;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    internal static class TestHelper
    {
        internal static HtmlHelper GetHtmlHelper()
        {
            var httpContextMock = new Mock<HttpContextBase>();
            httpContextMock.Setup(x => x.Items).Returns(new Hashtable());

            var viewDataDictionary = new ViewDataDictionary();

            var viewContext = new ViewContext
            {
                ViewData = viewDataDictionary,
                HttpContext = httpContextMock.Object
            };

            var viewDataConteinerMock = new Mock<IViewDataContainer>();
            viewDataConteinerMock.Setup(x => x.ViewData).Returns(viewDataDictionary);

            return new HtmlHelper(viewContext, viewDataConteinerMock.Object);
        }
    }
}
