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

        public ProcDefinition ParseProc(ProcIdentifier procIdentifier)
        {
            var procDefinition = new ProcDefinition(procIdentifier);
            using (var dataReader = new Procs.Dbo.sp_procedure_params_rowset(_sqlConnection, procIdentifier.Name, procedure_schema: procIdentifier.Schema).ExecuteDataReader())
            {
                while (dataReader.Read())
                {
                    var name = dataReader["PARAMETER_NAME"].ToString();
                    procDefinition.Parameters.Add(new ParameterDefinition(name));
                }
            }

            return procDefinition;
        }
    }
}