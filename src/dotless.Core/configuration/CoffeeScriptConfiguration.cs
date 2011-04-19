
namespace dotless.Core.configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public class CoffeeScriptConfiguration : DotlessConfiguration
    {
        public static new readonly CoffeeScriptConfiguration DefaultWeb = new CoffeeScriptConfiguration();
        
        public CoffeeScriptConfiguration() {
            CompilePattern = "{0} {1}";
        }
        
        public FileInfo Compiler { get; set; }
        public FileInfo CoffeeScriptSrc { get; set; }
        public string CompilePattern { get; set;}

        public bool IsValid() {
            return Compiler != null && Compiler.Exists &&
                   CoffeeScriptSrc != null && CoffeeScriptSrc.Exists &&
                   !String.IsNullOrEmpty(CompilePattern);
        }
    }
}
