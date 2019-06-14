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

        public ProcComposer(SqlConnection sqlConnection, ProcIdentifier procIdentifier)
        {
            _sqlConnection = sqlConnection;
            _procIdentifier = procIdentifier;
            _namespace = "Procs.dbo";
        }

        public void Compose()
        {
            _streamWriter = OpenStreamWriter();
            using (_streamWriter)
            {
                using (var dataReader = new Procs.Dbo.sp_procedure_params_rowset(_sqlConnection, _procIdentifier.Name, procedure_schema: _procIdentifier.Schema).ExecuteDataReader())
                {
                    while (dataReader.Read())
                    {
                        for (int columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
                        {
                            var line = String.Format("    {0}={1}", dataReader.GetName(columnIndex), dataReader[columnIndex]);
                            Write(line);
                        }
                    }
                }
            }
        }

        private void Write(string line)
        {
            Logger.Log(line);
            _streamWriter.WriteLine(line);
        }

        private StreamWriter OpenStreamWriter()
        {
            var fileName = $@"..\..\..\..\SprocWrapperCoreTest\Procs\{_procIdentifier.Schema}.{_procIdentifier.Name}.cs";
            return new StreamWriter(fileName);
        }
    }
}