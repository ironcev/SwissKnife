using System;
using System.Collections.Specialized;
using SwissKnife.Web.Mvc;

namespace SwissKnife.Web.Tests.Unit.Mvc
{
    internal class TestHttpContextDefinition
    {
        /// <summary>
        /// Overrides the protocol defined by <see cref="Protocol"/>.
        /// Use this property to define an arbitrary protocol different than protocols available in the <see cref="SwissKnife.Web.Mvc.Protocol"/>.
        /// </summary>
        internal string ArbitraryProtocol { get; set; }
        internal Protocol Protocol { get; set; }
        internal string WebSiteUrl { get; set; }
        internal int? Port { get; set; }
        internal string ApplicationPath { get; set; }
        internal string RequestPath { get; set; }
        internal HttpMethod? HttpMethod { get; set; }
        internal NameValueCollection QueryString { get; set; }

        /// <summary>
        /// If true, the <see cref="RequestUrl"/> will always return null.
        /// </summary>
        internal bool SetRequestUrlToNull { get; set; }

        internal bool HasApplicationPath { get { return !string.IsNullOrEmpty(ApplicationPath); } }
        internal bool HasRequestPath { get { return !string.IsNullOrEmpty(RequestPath); } }

        internal Uri RequestUrl
        {
            get
            {
                return SetRequestUrlToNull ? null :                    
                                             new Uri(string.Format("{0}://{1}{2}",
                                                                    string.IsNullOrEmpty(ArbitraryProtocol) ? Protocol.ToString().ToLowerInvariant() : ArbitraryProtocol,
                                                                    WebSiteUrl,
                                                                    Port.HasValue ? ":" + Port : string.Empty));
            }
        }

        internal TestHttpContextDefinition()
        {
            SetRequestUrlToNull = false;
            ArbitraryProtocol = null;
            Protocol = Protocol.Http;            
            WebSiteUrl = "localhost";
            Port = null;
            ApplicationPath = string.Empty;
            RequestPath = string.Empty;
            HttpMethod = Mvc.HttpMethod.Get;
            QueryString = new NameValueCollection();
        }
    }
}
