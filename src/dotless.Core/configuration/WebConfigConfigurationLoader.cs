namespace dotless.Core.configuration
{
    using System.Configuration;

    public class WebConfigConfigurationLoader
    {
        DotlessConfiguration mDotConfigCache;
        public DotlessConfiguration GetConfiguration()
        {
            if (mDotConfigCache == null) {
                mDotConfigCache = (DotlessConfiguration)ConfigurationManager.GetSection("dotless");

                if (mDotConfigCache == null)
                    mDotConfigCache =  DotlessConfiguration.DefaultWeb;
            }

            return mDotConfigCache;
        }

        CoffeeScriptConfiguration mCoffeeConfiguration;
        public CoffeeScriptConfiguration GetCoffeeConfiguration() {
            if (mCoffeeConfiguration == null) {
                mCoffeeConfiguration = (CoffeeScriptConfiguration)ConfigurationManager.GetSection("coffee");

                if (mCoffeeConfiguration == null)
                    mCoffeeConfiguration = CoffeeScriptConfiguration.DefaultWeb;
            }

            return mCoffeeConfiguration;
        }
    }
}