namespace dotless.Core.configuration
{
    using System.Configuration;

    public class WebConfigConfigurationLoader
    {
        public DotlessConfiguration GetConfiguration()
        {
            var webconfig = (DotlessConfiguration)ConfigurationManager.GetSection("dotless");
            
            if (webconfig == null)
                return DotlessConfiguration.DefaultWeb;

            webconfig.Web = true;

            return webconfig;
        }

        public CoffeeScriptConfiguration GetCoffeeConfiguration() {
            var webconfig = (CoffeeScriptConfiguration)ConfigurationManager.GetSection("coffee");

            if (webconfig == null)
                return CoffeeScriptConfiguration.DefaultWeb;

            webconfig.Web = true;

            return webconfig;
        }
    }
}