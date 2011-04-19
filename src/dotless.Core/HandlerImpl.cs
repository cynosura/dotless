namespace dotless.Core
{
    using Abstractions;
    using Input;
    using Response;

    public abstract class HandlerBase
    {
        public IHttp Http { get; protected set; }
        public IResponse Response { get; protected set; }
        public IFileReader FileReader { get; protected set; }

        public abstract void Execute();
    }
    
    public class LessHandlerImpl : HandlerBase
    {
        public ILessEngine Engine { get; private set; }

        public LessHandlerImpl(IHttp http, IResponse response, ILessEngine engine, IFileReader fileReader)
        {
            Http = http;
            Response = response;
            Engine = engine;
            FileReader = fileReader;
        }

        public override void Execute()
        {
            var localPath = Http.Context.Request.Url.LocalPath;
            var source = FileReader.GetFileContents(localPath);

            Response.WriteResponse(Engine.TransformToCss(source, localPath));
        }
    }
}