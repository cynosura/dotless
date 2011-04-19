namespace dotless.Core
{
    using System.Web;
    using configuration;
    using Microsoft.Practices.ServiceLocation;

    public class LessCssHttpHandler : IHttpHandler
    {
        DotlessConfiguration Config;
        LessHandlerImpl mHandlerImpl;

        public LessCssHttpHandler() {
            Config = new WebConfigConfigurationLoader().GetConfiguration();
            mHandlerImpl = new ContainerFactory().GetLessContainer(Config)
                .GetInstance<LessHandlerImpl>();
        }

        public void ProcessRequest(HttpContext context) {
            mHandlerImpl.Execute();
        }

        public bool IsReusable {
            get { return true; }
        }
    }
}