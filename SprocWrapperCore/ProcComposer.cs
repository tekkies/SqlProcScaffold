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
            using (_streamWriter = OpenStreamWriter())
            {
                WriteUsings();
                WriteNamespace();
                OpenBrace();
                GetParams();
                CloseBrace();
            }
        }

        private void CloseBrace()
        {
            SetIndentation(-1);
            WriteLine("}");
        }

        private void GetParams()
        {
            using (var dataReader = new Procs.Dbo.sp_procedure_params_rowset(_sqlConnection, _procIdentifier.Name, procedure_schema: _procIdentifier.Schema).ExecuteDataReader())
            {
                while (dataReader.Read())
                {
                    for (int columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
                    {
                        var line = String.Format("{0}={1}", dataReader.GetName(columnIndex), dataReader[columnIndex]);
                        WriteLine(line);
                    }
                }
            }
        }

        private void OpenBrace()
        {
            WriteLine("{");
            SetIndentation(_indentationLevel + 1);
        }

        private void SetIndentation(int increment)
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