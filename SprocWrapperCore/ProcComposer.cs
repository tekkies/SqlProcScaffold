using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace SprocWrapper
{
    internal class ProcComposer
    {
        private readonly SqlConnection _sqlConnection;
        private readonly ProcIdentifier _procIdentifier;
        private StreamWriter _streamWriter;
        private string _namespace;
        private int _indentationLevel=0;
        private string _indentationPadding = String.Empty;
        private ProcDefinition _procDefinition;

        public ProcComposer(SqlConnection sqlConnection, ProcIdentifier procIdentifier)
        {
            _sqlConnection = sqlConnection;
            _procIdentifier = procIdentifier;
            _namespace = "SprocWrapper.Procs";
        }

        public void Compose()
        {
            _procDefinition = new ProcParser(_sqlConnection).ParseProc(_procIdentifier);
            using (_streamWriter = OpenStreamWriter())
            {
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
                            WriteMethodHeader();
                            OpenParenthesis();
                            {
                                WriteLine("SqlConnection sqlConnection,");
                                WriteParameters();
                            }
                            CloseParenthesis();
                            OpenBrace();
                            {
                                CreateCommand();
                                AssignParameters();
                            }
                            CloseBrace();
                        }
                        CloseBrace();
                    }
                    CloseBrace();
                }
                CloseBrace();
            }
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

        private void CreateCommand()
        {
            WriteLine($"CreateCommand(sqlConnection, nameof({_procIdentifier.Schema}.{_procIdentifier.Name}));");
        }

        private void WriteParameters()
        {
            var parameterLines = new List<string>();
            for(var index = 1; index < _procDefinition.Parameters.Count; index++)
            {
                var parameterDefinition = _procDefinition.Parameters[index];
                parameterLines.Add(GetParameterLine(parameterDefinition));
            }
            WriteLine(String.Join(","+Environment.NewLine+_indentationPadding, parameterLines));
        }

        private string GetParameterLine(ParameterDefinition parameterDefinition)
        {
            return $"{parameterDefinition.CSharpType} {parameterDefinition.NameWithoutAt}";
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
            var fileName = $@"..\..\..\..\SprocWrapperCoreTest\Procs\{_procIdentifier.Schema}.{_procIdentifier.Name}.cs";
            return new StreamWriter(fileName);
        }
    }
}