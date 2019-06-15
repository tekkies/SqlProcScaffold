using System;
using System.Runtime.CompilerServices;
using SprocWrapper;
using SqlProcScaffold.Properties;

namespace SqlProcScaffold
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintBanner();
            var connectionString = args[0];
            var like = args.Length >= 2 ? args[1] : "%";
            var outputFolder = GetOutputFolder(args);
            ProcWrapper.SprocWrapper(connectionString, like, outputFolder);
        }

        private static string GetOutputFolder(string[] args)
        {
            var outputFolder = GetDefaultOutputFolder();
            if (args.Length >= 3)
            {
                outputFolder = args[2];
            }
            return outputFolder;
        }

        private static string GetDefaultOutputFolder()
        {
#if DEBUG
            const string sqlProcScaffoldTestSourceFolder = @"..\..\..\..\SqlProcScaffoldTest\Procs";
            string outputFolder = sqlProcScaffoldTestSourceFolder;
#else
            string outputFolder = String.Empty;
#endif
            return outputFolder;
        }

        private static void PrintBanner()
        {
            Logger.Log(String.Empty);
            Logger.Log(typeof(Program).Namespace);
            Logger.Log(String.Empty);
            Logger.Log(new string('-', 80));
            Logger.Log(SqlProcScaffold.Properties.Resources.LICENSE);
            Logger.Log(new string('-', 80));
            Logger.Log("Source: https://github.com/tekkies/SqlProcScaffold");
            Logger.Log(String.Empty);
        }
    }
}
