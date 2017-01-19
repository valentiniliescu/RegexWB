using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RegexTest
{
    /// <summary>
    ///     do a replace with a MatchEvaluator
    /// </summary>
    public class ReplaceMatchEvaluator
    {
        private static int _serial;
        private readonly string _matchEvaluatorString;

        public ReplaceMatchEvaluator(string matchEvaluatorString)
        {
            _matchEvaluatorString = matchEvaluatorString;
        }

        public MatchEvaluator MatchEvaluator { get; private set; }

        public string CreateAndLoadClass()
        {
            _serial++;
            var filename =
                string.Format("{0}match{1}.cs", Path.GetTempPath(), _serial);

            var writer = File.CreateText(filename);

            var className = string.Format("MatchEvaluator{0}", _serial);
            writer.WriteLine("using System;");
            writer.WriteLine("using System.Text.RegularExpressions;");
            writer.WriteLine("class {0} {{", className);
            writer.WriteLine(_matchEvaluatorString);
            writer.WriteLine("}");
            writer.Close();

            var version = Environment.Version;

            var runtimePath =
                string.Format(@"c:\windows\microsoft.net\framework\v{0}.{1}.{2}",
                    version.Major, version.Minor, version.Build);

            var batchFilename =
                Path.GetTempPath() + "BuildMatch.bat";
            writer = File.CreateText(batchFilename);
            writer.WriteLine("set path=%path%;{0}", runtimePath);
            writer.WriteLine("csc /nologo /t:library {0}", filename);
            writer.Close();

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = batchFilename;
            startInfo.WorkingDirectory = Path.GetTempPath();
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            var process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                var output = process.StandardOutput.ReadToEnd();
                var lines = output.Split('\n');

                output = "";
                for (var j = 4; j < lines.Length; j++)
                    output += lines[j];

                return output;
            }

            try
            {
                var assembly =
                    Assembly.LoadFrom(filename.Replace(".cs", ".dll"));

                var type = assembly.GetType(className);
                var methodInfo = type.GetMethod("Evaluator");
                MatchEvaluator =
                    (MatchEvaluator) Delegate.CreateDelegate(typeof(MatchEvaluator), methodInfo);
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return null;
        }
    }
}