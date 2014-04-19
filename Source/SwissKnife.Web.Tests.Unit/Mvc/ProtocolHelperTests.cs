using System;
using NUnit.Framework;
using SwissKnife.Web.Mvc;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    // ReSharper disable InconsistentNaming
    [TestFixture]
    public class ProtocolHelperTests
    {
        [Test]
        public void GetProtocolFromHttpRequest_Http_ReturnsHttp()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                Protocol = Protocol.Http
            };

            Assert.That(ProtocolHelper.GetProtocolFromHttpRequest(MvcTestHelper.GetHttpContext(httpContextDefinition).Request), Is.EqualTo(Protocol.Http));
        }

        [Test]
        public void GetProtocolFromHttpRequest_HttpCaseSensitive_ReturnsHttp()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                ArbitraryProtocol = "HtTp"
            };

            Assert.That(ProtocolHelper.GetProtocolFromHttpRequest(MvcTestHelper.GetHttpContext(httpContextDefinition).Request), Is.EqualTo(Protocol.Http));
        }

        [Test]
        public void GetProtocolFromHttpRequest_Https_ReturnsHttps()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                Protocol = Protocol.Https
            };

            Assert.That(ProtocolHelper.GetProtocolFromHttpRequest(MvcTestHelper.GetHttpContext(httpContextDefinition).Request), Is.EqualTo(Protocol.Https));
        }

        [Test]
        public void GetProtocolFromHttpRequest_HttpsCaseSensitive_ReturnsHttps()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                ArbitraryProtocol = "HtTpS"
            };

            Assert.That(ProtocolHelper.GetProtocolFromHttpRequest(MvcTestHelper.GetHttpContext(httpContextDefinition).Request), Is.EqualTo(Protocol.Https));
        }

        [Test]
        public void GetProtocolFromHttpRequest_ArbitraryProtocol_ThrowsException()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                ArbitraryProtocol = "ftp"
            };

            var exception = Assert.Throws<InvalidOperationException>(() => ProtocolHelper.GetProtocolFromHttpRequest(MvcTestHelper.GetHttpContext(httpContextDefinition).Request));
            Assert.That(exception.Message.Contains("The HTTP request has a URL scheme that do not correspond to any of the web protocols"));
            Console.WriteLine(exception.Message);
        }

        [Test]
        public void GetProtocolFromHttpRequest_RequestUrlIsNull_ThrowsException()
        {
            var httpContextDefinition = new TestHttpContextDefinition
            {
                SetRequestUrlToNull = true
            };

            var exception = Assert.Throws<InvalidOperationException>(() => ProtocolHelper.GetProtocolFromHttpRequest(MvcTestHelper.GetHttpContext(httpContextDefinition).Request));
            Assert.That(exception.Message.Contains("The HTTP request has no URL defined."));
            Console.WriteLine(exception.Message);
        }
    }
    // ReSharper restore InconsistentNaming
}
