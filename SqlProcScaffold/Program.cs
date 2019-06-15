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
            var commandLine = CommandLineParser.Parse(args);
            if (commandLine != null)
            {
                ProcWrapper.SprocWrapper(commandLine.ConnectionString,
                    commandLine.Filter, 
                    commandLine.OutputFolder);
            }
        }
    }
}
