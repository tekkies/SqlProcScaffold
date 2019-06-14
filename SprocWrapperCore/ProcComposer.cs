using System;
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

        public ProcComposer(SqlConnection sqlConnection, ProcIdentifier procIdentifier)
        {
            _sqlConnection = sqlConnection;
            _procIdentifier = procIdentifier;
            _namespace = "SprocWrapper.Procs";
        }

        public void Compose()
        {
            var procDefinition = new ProcParser(_sqlConnection).ParseProc(_procIdentifier);
            using (_streamWriter = OpenStreamWriter())
            {
                WriteUsings();
                WriteNamespace();
                OpenBrace();
                {
                    WriteSchemaClassDefinition();
                    OpenBrace();
                    {
                        WriteProcClassDefinition();
                        OpenBrace();
                        {

                        }
                        CloseBrace();
                    }
                    CloseBrace();
                }
                CloseBrace();
            }
        }

        private void WriteProcClassDefinition()
        {
            WriteLine($"public class {_procIdentifier.Name} : Proc");
        }

        private void WriteSchemaClassDefinition()
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