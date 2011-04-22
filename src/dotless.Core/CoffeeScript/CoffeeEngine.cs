
namespace dotless.Core.CoffeeScript
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using dotless.Core.Abstractions;
    using System.Runtime.CompilerServices;

    class CoffeeEngine : ICoffeeEngine
    {
        readonly string mCompilerPath;
        readonly string mCompilerArguments;

        ProcessStartInfo mStartInfo;

        public CoffeeEngine(IHttp pathResolver, string compilerPath, string compilerArguments) {
            mCompilerArguments = compilerArguments;
            mCompilerPath = compilerPath;
        }

        public string TransformToJavaScript(Stream source, string fileName) {
            string result = String.Empty;
            
            if (mStartInfo == null) {
                mStartInfo = new ProcessStartInfo(mCompilerPath) {
                    Arguments              = mCompilerArguments,
                    WorkingDirectory       = Directory.GetParent(mCompilerPath).FullName,
                    CreateNoWindow         = true,
                    ErrorDialog            = false,
                    LoadUserProfile        = false,
                    RedirectStandardInput  = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError  = true,
                    UseShellExecute        = false
                };
            }

            try {
                source.Seek(0, SeekOrigin.Begin);

                using (var p = new Process() {
                    StartInfo = mStartInfo,
                }) {
                    p.Start();
                    p.PriorityClass = ProcessPriorityClass.BelowNormal;

                    source.CopyTo(p.StandardInput.BaseStream);

                    if (p.WaitForExit(20000)) {
                        if (p.ExitCode == 0) {
                            result = p.StandardOutput.ReadToEnd();
                        } else {
                            result = p.StandardError.ReadToEnd();
                        }
                    } else
                        result = String.Format("Error compiling {0}: time-out", fileName);
                }
            } catch (Exception ex) {
                result = String.Format("Error parsing {0}: {1}", fileName, ex.ToString());
            }

            return result;
        }
    }
}
