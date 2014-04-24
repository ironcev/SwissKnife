using System;
using System.Web;
using System.Linq;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Mvc
{
    internal static class ProtocolHelper
    {
        internal static Protocol GetProtocolFromHttpRequest(HttpRequestBase httpRequest)
        {
            System.Diagnostics.Debug.Assert(httpRequest != null);

            Operation.IsValid(httpRequest.Url != null, "The HTTP request has no URL defined.");

            // TODO-IG: Use StringConvert.ToEnum() once when it is implemented.
            Protocol result;
            // We checked that the httpRequest.Url is not null.
            // ReSharper disable PossibleNullReferenceException
            if (!Enum.TryParse(httpRequest.Url.Scheme, true, out result))            
                throw new InvalidOperationException(string.Format("The HTTP request has a URL scheme that do not correspond to any of the web protocols defined in the '{1}' enumeration.{0}" +
                                                                  "The URL scheme was: '{2}'.{0}" +
                                                                  "The supported web protocols are: {3}.",
                                                                  Environment.NewLine,
                                                                  typeof(Protocol),
                                                                  httpRequest.Url.Scheme,
                                                                  Enum.GetNames(typeof(Protocol)).Select(protocol => protocol.ToLowerInvariant()).Aggregate((output, current) => output + ", " + current)));
            // ReSharper restore PossibleNullReferenceException

            return result;
        }
    }
}