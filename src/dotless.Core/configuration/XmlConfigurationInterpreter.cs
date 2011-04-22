namespace dotless.Core.configuration
{
    using System;
    using System.Xml;
    using Loggers;
    using System.IO;

    public class CoffeeConfigurationInterpreter : XmlConfigurationInterpreter<CoffeeScriptConfiguration>
    {
        public override CoffeeScriptConfiguration Process(XmlNode section) {
            var @default = CoffeeScriptConfiguration.DefaultWeb;
            var result = new CoffeeScriptConfiguration();

            result.MinifyOutput =
                GetBoolValue(section, "minify") ?? @default.MinifyOutput;

            result.CacheEnabled =
                GetBoolValue(section, "cache") ?? @default.CacheEnabled;

            result.Optimization =
                GetIntValue(section, "optimization") ?? @default.Optimization;

            result.CompilePattern = 
                GetStringValue(section, "compilerPattern") ?? @default.CompilePattern;

            result.CompilerPath =
                GetStringValue(section, "compilerPath") ?? @default.CompilerPath;

            var logLevel = GetStringValue(section, "log") ?? "default";
            switch (logLevel.ToLowerInvariant()) {
                case "info":
                    result.LogLevel = LogLevel.Info;
                    break;
                case "debug":
                    result.LogLevel = LogLevel.Debug;
                    break;
                case "warn":
                    result.LogLevel = LogLevel.Warn;
                    break;
                case "error":
                    result.LogLevel = LogLevel.Error;
                    break;
                case "default":
                    break;
                default:
                    break;
            }

            var source = GetTypeValue(section, "source");
            
            if (source != null)
                result.LessSource = source;

            result.Logger = GetTypeValue(section, "logger");

            return result;
        }
    }
    
    public class LessConfigurationInterpreter : XmlConfigurationInterpreter<DotlessConfiguration>
    {
        public override DotlessConfiguration Process(XmlNode section) {
            var dotlessConfiguration = DotlessConfiguration.DefaultWeb;

            dotlessConfiguration.MinifyOutput =
                GetBoolValue(section, "minify") ?? dotlessConfiguration.MinifyOutput;

            dotlessConfiguration.CacheEnabled =
                GetBoolValue(section, "cache") ?? dotlessConfiguration.CacheEnabled;

            dotlessConfiguration.Optimization =
                GetIntValue(section, "optimization") ?? dotlessConfiguration.Optimization;

            var logLevel = GetStringValue(section, "log") ?? "default";
            switch (logLevel.ToLowerInvariant()) {
                case "info":
                    dotlessConfiguration.LogLevel = LogLevel.Info;
                    break;
                case "debug":
                    dotlessConfiguration.LogLevel = LogLevel.Debug;
                    break;
                case "warn":
                    dotlessConfiguration.LogLevel = LogLevel.Warn;
                    break;
                case "error":
                    dotlessConfiguration.LogLevel = LogLevel.Error;
                    break;
                case "default":
                    break;
                default:
                    break;
            }

            var source = GetTypeValue(section, "source");
            if (source != null)
                dotlessConfiguration.LessSource = source;

            dotlessConfiguration.Logger = GetTypeValue(section, "logger");

            return dotlessConfiguration;
        }
    }
    
    public abstract class XmlConfigurationInterpreter<T>
    {
        public abstract T Process(XmlNode section);

        protected static string GetStringValue(XmlNode section, string property) {
            var attribute = section.Attributes[property];

            if (attribute == null)
                return null;

            return attribute.Value;
        }

        protected static int? GetIntValue(XmlNode section, string property) {
            var attribute = section.Attributes[property];

            if (attribute == null)
                return null;

            int result;
            if (int.TryParse(attribute.Value, out result))
                return result;

            return null;
        }

        protected static bool? GetBoolValue(XmlNode section, string property) {
            var attribute = section.Attributes[property];

            if (attribute == null)
                return null;

            bool result;
            if (bool.TryParse(attribute.Value, out result))
                return result;

            return null;
        }

        protected static Type GetTypeValue(XmlNode section, string property) {
            var attribute = section.Attributes[property];

            if (attribute == null)
                return null;

            var value = attribute.Value;

            if (!string.IsNullOrEmpty(value))
                return Type.GetType(value);

            return null;
        }
    }
}