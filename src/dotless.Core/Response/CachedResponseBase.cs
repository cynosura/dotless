namespace dotless.Core.Response
{
    using System;
    using System.Web;
    using Abstractions;

    public abstract class CachedResponseBase : ResponseBase
    {
        public readonly TimeSpan CacheAge;

        protected CachedResponseBase(IHttp http, TimeSpan cacheAge) : base(http) {
            CacheAge = cacheAge;
        }

        public override void WriteResponse(string content) {
            var response = Http.Context.Response;
            var request = Http.Context.Request;

            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetMaxAge(CacheAge);

            response.Cache.SetExpires(DateTime.UtcNow.Add(CacheAge));
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
}