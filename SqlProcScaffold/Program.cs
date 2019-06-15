using System;
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
            var request = CommandLineParser.Parse(args);
            if (request != null)
            {
                Executive.SprocWrapper();
                Logger.Log(Logger.Level.Info,"Done");
            }
        }
    }
}
