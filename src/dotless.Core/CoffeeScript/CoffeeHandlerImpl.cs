using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotless.Core.Abstractions;
using dotless.Core.Response;
using dotless.Core.Input;

namespace dotless.Core.CoffeeScript
{
    class CoffeeHandlerImpl : HandlerBase
    {
        readonly ICoffeeEngine Engine;

        public CoffeeHandlerImpl(IHttp http, IResponse response,
                ICoffeeEngine engine, IFileReader fileReader) {
            Http = http;
            Response = response;
            Engine = engine;
            FileReader = fileReader;
        }

        public override void Execute() {
            var localPath = Http.Context.Request.Url.LocalPath;
            var source = FileReader.GetFileContents(localPath);

            Response.WriteResponse(Engine.TransformToJavaScript(source, localPath));
        }
    }
}
