using System;
using System.Data.SqlClient;

namespace SprocWrapper
{
    internal class ProcParser
    {
        private readonly SqlConnection _sqlConnection;

        public ProcParser(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public void ParseProc(ProcIdentifier procIdentifier)
        {
            using (var dataReader = new Procs.Dbo.sp_procedure_params_rowset(_sqlConnection, procIdentifier.Name, procedure_schema: procIdentifier.Schema).ExecuteDataReader())
            {
                while (dataReader.Read())
                {
                    for (int columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
                    {
                        var line = String.Format("{0}={1}", dataReader.GetName(columnIndex), dataReader[columnIndex]);
                    }
                }
            }
        }
    }
}