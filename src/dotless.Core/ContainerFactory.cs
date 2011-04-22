namespace dotless.Core
{
    using Abstractions;
    using Cache;
    using configuration;
    using dotless.Core.CoffeeScript;
    using Input;
    using Loggers;
    using Microsoft.Practices.ServiceLocation;
    using Pandora;
    using Pandora.Fluent;
    using Parameters;
    using Response;
    using Stylizers;
    using System.IO;

    public class ContainerFactory
    {
        public IServiceLocator GetLessContainer(DotlessConfiguration configuration) {
            var Container = new PandoraContainer();

            Container.Register(pandora => RegisterServices(pandora, configuration));

            return new CommonServiceLocatorAdapter(Container);
        }

        public IServiceLocator GetCoffeeContainer(CoffeeScriptConfiguration configuration) {
            var Container = new PandoraContainer();

            Container.Register(pandora => RegisterServices(pandora, configuration));

            return new CommonServiceLocatorAdapter(Container);
        }

        private void OverrideServices(FluentRegistration pandora, DotlessConfiguration configuration) {
            if (configuration.Logger != null)
                pandora.Service<ILogger>().Implementor(configuration.Logger);
        }

        #region Coffee setup
        private void RegisterServices(FluentRegistration pandora, CoffeeScriptConfiguration configuration) {
            OverrideServices(pandora, configuration);

            RegisterWebServices(pandora, configuration);

            RegisterCoreServices(pandora, configuration);
        }

        private void RegisterWebServices(FluentRegistration pandora, CoffeeScriptConfiguration configuration) {
            pandora.Service<IHttp>()
                .Implementor<Http>().Lifestyle.Transient();

            pandora.Service<CoffeeHandlerImpl>()
                .Implementor<CoffeeHandlerImpl>().Lifestyle.Transient();

            pandora.Service<IParameterSource>()
                .Implementor<QueryStringParameterSource>().Lifestyle.Transient();

            if (configuration.CacheEnabled)
                pandora.Service<IResponse>()
                    .Implementor<CachedJavascriptResponse>().Lifestyle.Transient();
            else
                pandora.Service<IResponse>()
                    .Implementor<JavascriptResponse>().Lifestyle.Transient();

            pandora.Service<ICache>()
                .Implementor<HttpCache>().Lifestyle.Transient();

            pandora.Service<ILogger>()
                .Implementor<AspResponseLogger>()
                    .Parameters("level").Set("error-level")
                .Lifestyle.Transient();

            pandora.Service<IPathResolver>()
                .Implementor<AspServerPathResolver>().Lifestyle.Transient();
        }

        private void RegisterCoreServices(FluentRegistration pandora, CoffeeScriptConfiguration configuration) {
            pandora.Service<string>("compiler-path")
                .Instance(configuration.CompilerPath);

            pandora.Service<LogLevel>("error-level")
                .Instance(configuration.LogLevel);

            pandora.Service<string>("compiler-args")
                .Instance(configuration.CompilePattern);

            if (configuration.CacheEnabled)
                pandora.Service<ICoffeeEngine>()
                    .Implementor<CoffeeCacheDecorator>().Lifestyle.Transient();

            pandora.Service<ICoffeeEngine>()
                .Implementor<CoffeeEngine>()
                    .Parameters("compilerPath").Set("compiler-path")
                    .Parameters("compilerArguments").Set("compiler-args")
                .Lifestyle.Transient();

            //pandora.Service<bool>("minify-output").Instance(configuration.MinifyOutput);

            pandora.Service<IFileReader>()
                .Implementor(configuration.LessSource);
        }
        #endregion

        #region Less setup
        private void RegisterServices(FluentRegistration pandora, DotlessConfiguration configuration) {
            OverrideServices(pandora, configuration);

            if (configuration.Web)
                RegisterWebServices(pandora, configuration);
            else
                RegisterLocalServices(pandora);

            RegisterCoreServices(pandora, configuration);
        }

        private void RegisterWebServices(FluentRegistration pandora, DotlessConfiguration configuration) {
            pandora.Service<IHttp>()
                .Implementor<Http>().Lifestyle.Transient();

            pandora.Service<LessHandlerImpl>()
                .Implementor<LessHandlerImpl>().Lifestyle.Transient();

            pandora.Service<IParameterSource>()
                .Implementor<QueryStringParameterSource>().Lifestyle.Transient();

            if (configuration.CacheEnabled)
                pandora.Service<IResponse>()
                    .Implementor<CachedCssResponse>().Lifestyle.Transient();
            else
                pandora.Service<IResponse>()
                    .Implementor<CssResponse>().Lifestyle.Transient();

            pandora.Service<ICache>()
                .Implementor<HttpCache>().Lifestyle.Transient();

            pandora.Service<ILogger>()
                .Implementor<AspResponseLogger>().Parameters("level").Set("error-level").Lifestyle.Transient();

            pandora.Service<IPathResolver>()
                .Implementor<AspServerPathResolver>().Lifestyle.Transient();
        }

        private void RegisterLocalServices(FluentRegistration pandora) {
            pandora.Service<ICache>()
                .Implementor<InMemoryCache>();

            pandora.Service<IParameterSource>()
                .Implementor<ConsoleArgumentParameterSource>();

            pandora.Service<ILogger>()
                .Implementor<ConsoleLogger>().Parameters("level").Set("error-level");

            pandora.Service<IPathResolver>()
                .Implementor<RelativePathResolver>();
        }

        private void RegisterCoreServices(FluentRegistration pandora, DotlessConfiguration configuration) {
            pandora.Service<LogLevel>("error-level")
                .Instance(configuration.LogLevel);

            pandora.Service<int>("default-optimization")
                .Instance(configuration.Optimization);

            pandora.Service<bool>("minify-output")
                .Instance(configuration.MinifyOutput);

            pandora.Service<IStylizer>()
                .Implementor<PlainStylizer>();

            pandora.Service<Parser.Parser>()
                .Implementor<Parser.Parser>()
                    .Parameters("optimization").Set("default-optimization")
                .Lifestyle.Transient();

            pandora.Service<ILessEngine>()
                .Implementor<ParameterDecorator>().Lifestyle.Transient();

            if (configuration.CacheEnabled)
                pandora.Service<ILessEngine>()
                    .Implementor<CacheDecorator>().Lifestyle.Transient();

            pandora.Service<ILessEngine>()
                .Implementor<LessEngine>()
                    .Parameters("compress").Set("minify-output")
                .Lifestyle.Transient();

            pandora.Service<IFileReader>()
                .Implementor(configuration.LessSource);
        }
        #endregion
    }
}