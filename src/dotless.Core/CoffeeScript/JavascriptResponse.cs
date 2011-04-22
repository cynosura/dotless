namespace dotless.Core.CoffeeScript
{
    using dotless.Core.Response;
    using dotless.Core.Abstractions;

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
