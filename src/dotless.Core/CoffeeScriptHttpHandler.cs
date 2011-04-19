
namespace dotless.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Microsoft.Practices.ServiceLocation;
    using dotless.Core.configuration;
    using dotless.Core.CoffeeScript;

    class CoffeeScriptHttpHandler : IHttpHandler
    {
        IServiceLocator mContainer;
        CoffeeScriptConfiguration mConfig;

        public CoffeeScriptHttpHandler() {
            mConfig = new WebConfigConfigurationLoader().GetCoffeeConfiguration();
            mContainer = new ContainerFactory().GetCoffeeContainer(mConfig);
        }

        public void ProcessRequest(HttpContext context) {
            //var a = mContainer.GetInstance<LessHandlerImpl>();
            var handler = mContainer.GetInstance<CoffeeHandlerImpl>();

            handler.Execute();
        }

        public bool IsReusable {
            get { return true; }
        }
    }
}
