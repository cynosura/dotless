namespace dotless.Core.Response
{
    using System.Web;
    using Abstractions;

    public class CssResponse : ResponseBase
    {
        public override string ContentType {
            get { return "text/css"; }
        }
        
        public CssResponse(IHttp http) : base(http)
        {
        }
    }

    public class JavascriptResponse : ResponseBase
    {
        public override string ContentType {
            get { return "text/javascript"; }
        }

        public JavascriptResponse(IHttp http)
            : base(http) {
        }
    }
}