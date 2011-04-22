
namespace dotless.Core.CoffeeScript
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    public interface ICoffeeEngine {
        string TransformToJavaScript(Stream source, string fileName);
    }
}
