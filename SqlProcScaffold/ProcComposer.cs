using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using SqlProcScaffold;

namespace SprocWrapper
{
    internal class ProcComposer
    {
        private readonly SqlConnection _sqlConnection;
        private readonly ProcIdentifier _procIdentifier;
        private StreamWriter _streamWriter;
        private string _namespace;
        private int _indentationLevel;
        private string _indentationPadding = String.Empty;
        private ProcDefinition _procDefinition;
        private string _outputFolder;

        public ProcComposer(SqlConnection sqlConnection, ProcIdentifier procIdentifier, string outputFolder)
        {
            _sqlConnection = sqlConnection;
            _procIdentifier = procIdentifier;
            _namespace = CommandLineParser.Request.NameSpace;
            _outputFolder = outputFolder;
        }

        public void Compose()
        {
            _procDefinition = new ProcParser(_sqlConnection).ParseProc(_procIdentifier);
            using (_streamWriter = OpenStreamWriter())
            {
                WriteAutoGenMessage();
                WriteUsings();
                WriteNamespace();
                OpenBrace();
                {
                    WriteSchemaClassHeader();
                    OpenBrace();
                    {
                        WriteProcClassHeader();
                        OpenBrace();
                        {
                            WriteMethod(false);
                            WriteMethod(true);
                        }
                        CloseBrace();
                    }
                    CloseBrace();
                }
                CloseBrace();
            }
        }

        private void WriteAutoGenMessage()
        {
            WriteLine(AutoGenComment);
        }

        private void WriteMethod(bool includeConnectionParameter)
        {
            WriteMethodHeader();
            OpenParenthesis();
            {
                WriteMethodParameters(includeConnectionParameter);
            }
            CloseParenthesis();
            OpenBrace();
            {
                CreateCommand(includeConnectionParameter);
                AssignParameters();
            }
            CloseBrace();
        }

        private void AssignParameters()
        {
            var parameterDefinitions = _procDefinition.Parameters;
            for(var i=1;i<parameterDefinitions.Count;i++)
            {
                var parameterDefinition = parameterDefinitions[i];
                AssignParameter(parameterDefinition);
            }
        }

        private void AssignParameter(ParameterDefinition parameterDefinition)
        {
            WriteLine($"AddParameterIfNotNull(nameof({parameterDefinition.NameWithoutAt}), {parameterDefinition.NameWithoutAt});");
        }

        private void CreateCommand(bool includeConnectionParameter)
        {
            var connection = includeConnectionParameter ? "sqlConnection" : "DefaultConnection";
            WriteLine($"CreateCommand({connection}, nameof({_procIdentifier.Schema}.{_procIdentifier.Name}));");
        }

        private void WriteMethodParameters(bool explicitConnection)
        {
            var parameterLines = new List<string>();
            if (explicitConnection)
            {
                parameterLines.Add("SqlConnection sqlConnection");
            }
            for (var index = 1; index < _procDefinition.Parameters.Count; index++)
            {
                var parameterDefinition = _procDefinition.Parameters[index];
                parameterLines.Add(GetParameterLine(parameterDefinition));
            }
            WriteLine(String.Join(","+Environment.NewLine+_indentationPadding, parameterLines));
        }

        private string GetParameterLine(ParameterDefinition parameterDefinition)
        {
            var parameterLine = $"{parameterDefinition.GetCSharpType(parameterDefinition.HasDefault)} {parameterDefinition.NameWithoutAt}";
            if (parameterDefinition.HasDefault)
            {
                parameterLine += " = null";
            }
            else
            {
                if (CommandLineParser.Request.UseNotNullAttribute)
                {
                    parameterLine = "[NotNull] " + parameterLine;
                }
            }
            return parameterLine;
        }

        private void CloseParenthesis()
        {
            IncrementIndentation(-1);
            WriteLine(")");
        }

        private void OpenParenthesis()
        {
            WriteLine("(");
            IncrementIndentation(1);

        }

        private void WriteMethodHeader()
        {
            WriteLine($"public {_procIdentifier.Name}");
        }

        private void WriteProcClassHeader()
        {
            WriteLine($"public class {_procIdentifier.Name} : Proc");
        }

        private void WriteSchemaClassHeader()
        {
            WriteLine($"public partial class {_procIdentifier.Schema}");
        }

        private void CloseBrace()
        {
            IncrementIndentation(-1);
            WriteLine("}");
        }

        private void OpenBrace()
        {
            WriteLine("{");
            IncrementIndentation(1);
        }

        private void IncrementIndentation(int increment)
        {
            _indentationLevel += increment;
            _indentationPadding = new String(' ', _indentationLevel*4);
        }

        private void WriteNamespace()
        {
            WriteLine($@"namespace {_namespace}");
        }

        private void WriteUsings()
        {
            WriteLine(@"using System.Data.SqlClient;");
            WriteLine(@"using System;");
            if (CommandLineParser.Request.UseNotNullAttribute)
            {
                WriteLine(@"using JetBrains.Annotations;");
            }

            WriteLine(String.Empty);
        }

        private void WriteLine(string text)
        {
            text = _indentationPadding + text;
            //Logger.Log(text);
            _streamWriter.WriteLine(text);
        }

        private StreamWriter OpenStreamWriter()
        {
            var fileName = Path.Join(_outputFolder,$@"{_procIdentifier.Schema}.{_procIdentifier.Name}.cs");
            return new StreamWriter(fileName);
        }

        public const string AutoGenComment = "//File auto-generated using https://github.com/tekkies/SqlProcScaffold";

        public static void WriteBaseClass()
        {
            var className = typeof(Procs.Proc).Name;
            var file = Path.Join(CommandLineParser.Request.OutputFolder, $"{className}.cs");
            using (var streamWriter = new StreamWriter(file))
            {
                var templateCode = SqlProcScaffold.Properties.Resources.Proc;
                var renderedCode = templateCode.Replace("namespace SprocWrapper.Procs", $"namespace {CommandLineParser.Request.NameSpace}");
                streamWriter.WriteLine(AutoGenComment);
                streamWriter.Write(renderedCode);
            }
        }
    }
}