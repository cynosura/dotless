
namespace dotless.Core.CoffeeScript
{
    using System;
    using System.IO;

    public interface ICoffeeEngine {
        string TransformToJavaScript(Stream source, DateTime lastModified, string fileName);
    }
}
