using System;
using System.Reflection;

namespace SqlProcScaffold
{
    internal class VersionInfo
    {
        public static string VersionString => Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        public static string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name;
    }
}