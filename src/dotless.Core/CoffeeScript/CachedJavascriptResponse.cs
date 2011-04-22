namespace dotless.Core.CoffeeScript
{
    using System;
    using dotless.Core.Response;
    using dotless.Core.Abstractions;

    public class CachedJavascriptResponse : CachedResponseBase
    {
        public override string ContentType {
            get { return "text/javascript"; }
        }

        public CachedJavascriptResponse(IHttp http)
            : base(http, TimeSpan.FromDays(7)) {
        }
    }
}
