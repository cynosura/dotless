using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dotless.Core.CoffeeScript
{
    public interface ICoffeeEngine
    {
        string TransformToJavaScript(string source, string fileName);
    }
}
