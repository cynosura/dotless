
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
        CoffeeScriptConfiguration mConfig;
        CoffeeHandlerImpl mHandlerImpl;

        public CoffeeScriptHttpHandler() {
            mConfig = new WebConfigConfigurationLoader().GetCoffeeConfiguration();
            mHandlerImpl = new ContainerFactory().GetCoffeeContainer(mConfig)
                .GetInstance<CoffeeHandlerImpl>();
        }

        public void ProcessRequest(HttpContext context) {
            mHandlerImpl.Execute();
        }

        public bool IsReusable {
            get { return true; }
        }
    }
}
