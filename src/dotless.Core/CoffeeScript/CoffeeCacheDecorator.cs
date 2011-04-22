using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotless.Core.Loggers;
using dotless.Core.Cache;
using System.IO;

namespace dotless.Core.CoffeeScript
{
    class CoffeeCacheDecorator : ICoffeeEngine
    {
        public readonly ICoffeeEngine Underlying;
        
        readonly ICache Cache;
        readonly ILogger Logger;

        public CoffeeCacheDecorator(ICoffeeEngine underlying, ICache cache)
            : this(underlying, cache, NullLogger.Instance) { }

        public CoffeeCacheDecorator(ICoffeeEngine underlying, ICache cache, ILogger logger) {
            Underlying = underlying;
            Cache = cache;
            Logger = logger;
        }

        public string TransformToJavaScript(Stream source, DateTime lastModified, string resourcePath) {
            //Compute Cache Key

            var timestamp = lastModified.ToBinary();
            var cacheKey = timestamp.ToString() + resourcePath;
            
            if (!Cache.Exists(cacheKey)) {
                Logger.Debug(String.Format("Inserting cache entry for {0}", cacheKey));

                var css = Underlying.TransformToJavaScript(source, lastModified, resourcePath);
                var dependancies = new[] { resourcePath };

                Cache.Insert(cacheKey, dependancies, css);

                return css;
            }
            Logger.Debug(String.Format("Retrieving cache entry {0}", cacheKey));
            return Cache.Retrieve(cacheKey);
        }
    }
}
