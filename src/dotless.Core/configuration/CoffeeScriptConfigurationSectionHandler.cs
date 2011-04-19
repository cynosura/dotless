using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml;

namespace dotless.Core.configuration
{
    class CoffeeConfigurationSectionHandler : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section) {
            var configuration = CoffeeScriptConfiguration.DefaultWeb;

            try {
                var interpreter = new XmlConfigurationInterpreter();
                //configuration = interpreter.Process(section);
            } catch (Exception) {
                //TODO: Log the errormessage to somewhere
            }

            return configuration;
        }
    }
}
