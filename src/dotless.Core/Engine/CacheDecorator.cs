namespace dotless.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using Cache;
    using Loggers;

    public class CacheDecorator : ILessEngine
    {
        public readonly ILessEngine Underlying;
        
        readonly ICache Cache;
        readonly ILogger Logger;

        public CacheDecorator(ILessEngine underlying, ICache cache)
            : this(underlying, cache, NullLogger.Instance) { }

        public CacheDecorator(ILessEngine underlying, ICache cache, ILogger logger) {
            Underlying = underlying;
            Cache = cache;
            Logger = logger;
        }

        public string TransformToCss(string source, string fileName) {
            //Compute Cache Key
            var hash = dotless.Core.Utils.HashUtils.ComputeMD5(source);
            var cacheKey = fileName + hash;
            
            if (!Cache.Exists(cacheKey)) {
                Logger.Debug(String.Format("Inserting cache entry for {0}", cacheKey));

                var css = Underlying.TransformToCss(source, fileName);
                var dependancies = new[] { fileName }.Concat(GetImports());

                Cache.Insert(cacheKey, dependancies, css);

                return css;
            }
            Logger.Debug(String.Format("Retrieving cache entry {0}", cacheKey));
            return Cache.Retrieve(cacheKey);
        }

        

        public IEnumerable<string> GetImports() {
            return Underlying.GetImports();
        }

        public void ResetImports() {
            Underlying.ResetImports();
        }
    }
}