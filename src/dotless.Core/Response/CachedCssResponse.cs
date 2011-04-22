namespace dotless.Core.Response
{
    using System;
    using dotless.Core.Abstractions;

    public class CachedCssResponse : CachedResponseBase
    {
        public override string ContentType {
            get { return "text/css"; }
        }

        public CachedCssResponse(IHttp http)
            : base(http, TimeSpan.FromDays(7)) {
        }
    }
}
