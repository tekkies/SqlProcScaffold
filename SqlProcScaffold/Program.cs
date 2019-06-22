using System.Diagnostics;
using System.Runtime.CompilerServices;
using Ookii.CommandLine;
using SprocWrapper;
using SqlProcScaffold.Properties;

namespace SqlProcScaffold
{
    class Program
    {
        static void Main(string[] args)
        {
            LogVersion();
            var request = CommandLineParser.Parse(args);
            if (request != null)
            {
                Executive.SprocWrapper();
                Logger.Log(Logger.Level.Info,"Done");
            }
        }

        private static void LogVersion()
        {
            Logger.Log(Logger.Level.Info, $"{VersionInfo.AssemblyName} v{VersionInfo.VersionString}");
        }
    }
}
