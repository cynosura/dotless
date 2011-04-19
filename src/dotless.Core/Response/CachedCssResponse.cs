namespace dotless.Core.Response
{
    using System;
    using System.Web;
    using Abstractions;

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
    
    public abstract class CachedResponseBase : ResponseBase
    {
        public readonly int CacheAgeMinutes;

        protected CachedResponseBase(IHttp http, int cacheAge) : base(http) {
            CacheAgeMinutes = cacheAge;
        }

        public override void WriteResponse(string content) {
            var response = Http.Context.Response;
            var request = Http.Context.Request;

            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetMaxAge(new TimeSpan(0, CacheAgeMinutes, 0));

            response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(CacheAgeMinutes));
            response.Cache.SetETagFromFileDependencies();

            response.ContentType = ContentType;
            response.Write(content);

            /*
                        if (request.Headers.Get("If-None-Match") == response.Headers.Get("ETag"))
                        {
                            response.StatusCode = 304;
                            response.StatusDescription = "Not Modified";

                            // Explicitly set the Content-Length header so client
                            // keeps the connection open for other requests.
                            response.AddHeader("Content-Length", "0");
                        }
                        else
                        {
                            response.ContentType = "text/css";
                            response.Write(css);
                        }
            */
            response.End();
        }
    }

    public class CachedCssResponse : CachedResponseBase
    {
        public override string ContentType {
            get { return "text/css"; }
        }

        public CachedCssResponse(IHttp http)
            : base(http, 10080 /* 7 days */) {
        }
    }

    public class CachedJavascriptResponse : CachedResponseBase
    {
        public override string ContentType {
            get { return "text/javascript"; }
        }

        public CachedJavascriptResponse(IHttp http)
            : base(http, 10080 /* 7 days */) {
        }
    }
}