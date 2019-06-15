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
            ProcWrapper.SprocWrapper(connectionString, like);
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
