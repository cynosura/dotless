
namespace dotless.Core.CoffeeScript
{
    using System;
    using dotless.Core.Abstractions;
    using dotless.Core.Response;
    using dotless.Core.Input;
    using System.IO;

    class CoffeeHandlerImpl : HandlerBase
    {
        readonly ICoffeeEngine mEngine;
        readonly IPathResolver mResolver;

        public CoffeeHandlerImpl(IHttp http, IResponse response,
                ICoffeeEngine engine, IPathResolver resolver) {
            Http = http;
            Response = response;
            
            mEngine = engine;
            mResolver = resolver;
        }

        public override void Execute() {
            var localPath = Http.Context.Request.Url.LocalPath;
            string path = mResolver.GetFullPath(localPath);

            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) {
                Response.WriteResponse(mEngine.TransformToJavaScript(fs, File.GetLastWriteTimeUtc(path), localPath));
            }
        }
    }
}
