using System;
using SqlProcScaffold;

namespace SprocWrapper
{
    internal class Logger
    {
        public static void Log(Level level, string format, params object[] args)
        {
            if (level <= Level.Info || CommandLineParser.Request.Verbose)
            {
                Console.WriteLine(format, args);
            }
            System.Diagnostics.Debug.WriteLine(format, args);
        }

        internal enum Level
        {
            Error,
            Info,
            Verbose
        }
    }
}