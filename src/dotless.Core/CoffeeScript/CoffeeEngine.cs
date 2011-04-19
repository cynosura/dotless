using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotless.Core.CoffeeScript
{
    class CoffeeEngine : ICoffeeEngine
    {
        public string TransformToJavaScript(string source, string fileName) {
            return source;
        }
    }
}
