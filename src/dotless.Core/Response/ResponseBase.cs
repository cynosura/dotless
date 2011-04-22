namespace dotless.Core.Response
{
    using System;
    using System.Web;
    using dotless.Core.Abstractions;

    public abstract class ResponseBase : IResponse
    {
        public readonly IHttp Http;

        public abstract string ContentType { get; }

        protected ResponseBase(IHttp http) {
            Http = http;
        }

        public virtual void WriteResponse(string content) {
            var response = Http.Context.Response;
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.ContentType = ContentType;
            response.Write(content);
            response.End();
        }
    }
}
