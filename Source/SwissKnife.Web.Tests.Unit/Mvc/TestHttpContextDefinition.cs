using System;
using SwissKnife.Web.Mvc;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    internal class TestHttpContextDefinition
    {
        internal Protocol Protocol { get; set; }
        internal string WebSiteUrl { get; set; }
        internal int? Port { get; set; }
        internal string ApplicationPath { get; set; }
        internal string RequestPath { get; set; }
        internal HttpMethod? HttpMethod { get; set; }

        internal bool HasApplicationPath { get { return !string.IsNullOrEmpty(ApplicationPath); } }
        internal bool HasRequestPath { get { return !string.IsNullOrEmpty(RequestPath); } }

        internal Uri RequestUrl { get { return new Uri(string.Format("{0}://{1}{2}", Protocol.ToString().ToLowerInvariant(), WebSiteUrl, Port.HasValue ? ":" + Port : string.Empty)); } }

        internal TestHttpContextDefinition()
        {
            Protocol = Protocol.Http;            
            WebSiteUrl = "localhost";
            Port = null;
            ApplicationPath = string.Empty;
            RequestPath = string.Empty;
            HttpMethod = Mvc.HttpMethod.Get;
        }
    }
}
