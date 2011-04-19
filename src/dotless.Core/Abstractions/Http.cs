namespace dotless.Core.Abstractions
{
    using System.Web;

    public class Http : IHttp
    {
        public HttpContextBase Context {
            get { return new HttpContextWrapper(HttpContext.Current); }
        }
    }
}