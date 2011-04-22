
namespace dotless.Core.configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class CoffeeScriptConfiguration : DotlessConfiguration
    {
        public static new readonly CoffeeScriptConfiguration DefaultWeb = 
            new CoffeeScriptConfiguration();
        
        public CoffeeScriptConfiguration() {
            CompilePattern = "";
            CompilerPath = "";
        }
        
        public string CompilerPath { get; set; }
        public string CompilePattern { get; set;}

    }
}
