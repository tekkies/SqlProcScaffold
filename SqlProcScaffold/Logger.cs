using System;

namespace SprocWrapper
{
    internal class Logger
    {
        public static void Log(string format, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(format, args);
            Console.WriteLine(format, args);
        }
    }
}