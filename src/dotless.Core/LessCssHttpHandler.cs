namespace dotless.Core
{
    using System.Web;
    using configuration;
    using Microsoft.Practices.ServiceLocation;

    public class LessCssHttpHandler : IHttpHandler
    {
        public IServiceLocator Container { get; set; }
        public DotlessConfiguration Config { get; set; }

        public LessCssHttpHandler()
        {
            Config = new WebConfigConfigurationLoader().GetConfiguration();
            Container = new ContainerFactory().GetLessContainer(Config);
        }

        public void ProcessRequest(HttpContext context)
        {
            var handler = Container.GetInstance<LessHandlerImpl>();
            
            handler.Execute();
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}