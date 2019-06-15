﻿using System;
using System.ComponentModel;
using System.Linq;
using Ookii.CommandLine;
using SprocWrapper;

namespace SqlProcScaffold
{
    [Description(ProgramDescription)]
    class CommandLineParser
    {
        private const string ProgramDescription = @"SqlProcScaffold - Generate C# wrappers for SQL Server stored procedures.

    https://github.com/tekkies/SqlProcScaffold";

        public static CommandLineParser Request { get; private set; }

        [CommandLineArgument(Position = 0, IsRequired = true), Description(@"Connection string to the SqlServer")]
        public string ConnectionString { get; set; }

        [CommandLineArgument(Position = 1, IsRequired = true), Description(@"Namespace for the generated code.")]
        public string NameSpace { get; set; }

        [CommandLineArgument(Position = 2, DefaultValue = "%"), Description(
             @"Filter procedures by name. Wildcard is %. e.g.
dbo.sp_get%
")]
        public String Filter { get; set; }

        [CommandLineArgument(
     Position = 3,
#if DEBUG
            DefaultValue = @"..\..\..\..\SqlProcScaffoldTest\Procs"),
#else
            DefaultValue = String.Empty()),
#endif
          Description(
     @"Filter procedures by name. Wildcard is %. e.g.
        dbo.sp_get%
    ")]
        public String OutputFolder { get; set; }

    
        [CommandLineArgument, Description("Addorn code with [NotNull] attributes.  This helps ReSharper warn you that a parameter is required.")]
        public bool UseNotNullAttribute { get; set; }


        [CommandLineArgument, Description("Show license and acknowledgements.")]
        public bool ShowLicense { get; set; }


        [CommandLineArgument, Alias("v"), Description("Print verbose information.")]
        public bool Verbose { get; set; }

        [CommandLineArgument, Alias("?"), Description("Displays this help message.")]
        public bool Help { get; set; }

        public static CommandLineParser Parse(string[] args)
        {
            Request = null;
            Ookii.CommandLine.CommandLineParser parser = new Ookii.CommandLine.CommandLineParser(typeof(CommandLineParser));
            parser.ArgumentParsed += CommandLineParser_ArgumentParsed;
            try
            {
                try
                {
                    Request = (CommandLineParser)parser.Parse(args);
                }
                finally
                {
                    MaybeShowLicense(args);
                }
            }
            catch (CommandLineArgumentException ex)
            {
                using (LineWrappingTextWriter writer = LineWrappingTextWriter.ForConsoleError())
                {
                    Logger.Log(Logger.Level.Error, ex.Message);
                }
            }
            if (Request == null)
            {
                WriteUsageOptions options = new WriteUsageOptions() {IncludeDefaultValueInDescription = true, IncludeAliasInDescription = true};
                parser.WriteUsageToConsole(options);
                Logger.Log(Logger.Level.Info,"Example:");
                Logger.Log(Logger.Level.Info, "    SqlProcScaffold.exe \"Server=my...\" MyNameSpace dbo.sp% C:\\src\\MyProj");
            }
            return Request;
        }

        private static void MaybeShowLicense(string[] args)
        {
            var parsedLicenseRequest = Request != null && Request.ShowLicense;
            var unparsedLicenseRequest = Request == null && args.Any(o => o.Contains("ShowLicense", StringComparison.OrdinalIgnoreCase));
            if (parsedLicenseRequest || unparsedLicenseRequest)
            {
                PrintLicense();
            }
        }

        private static void CommandLineParser_ArgumentParsed(object sender, ArgumentParsedEventArgs e)
        {
            if (e.Argument.ArgumentName == "Help")
                e.Cancel = true;
        }

        private static void PrintLicense()
        {
            Logger.Log(Logger.Level.Info, String.Empty);
            Logger.Log(Logger.Level.Info, ProgramDescription);
            Logger.Log(Logger.Level.Info, String.Empty);
            Logger.Log(Logger.Level.Info, new string('-', 80));
            Logger.Log(Logger.Level.Info, String.Empty);
            Logger.Log(Logger.Level.Info, Properties.Resources.LICENSE);
            Logger.Log(Logger.Level.Info, new string('-', 80));
            Logger.Log(Logger.Level.Info, @"
Acknowledgments:
    Ookii.CommandLine by Sven Groot (Ookii.org)
");
            Logger.Log(Logger.Level.Info, new string('-', 80));
        }
    }
}
