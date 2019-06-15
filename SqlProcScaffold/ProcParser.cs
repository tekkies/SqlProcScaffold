using System;
using System.Data.SqlClient;
using System.Text;

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
            ReadBasicParameterDefinition(procIdentifier, procDefinition);
            ParseParameterDefaults(procDefinition);
            return procDefinition;
        }

        private void ParseParameterDefaults(ProcDefinition procDefinition)
        {
            //sys.sp_procedure_params_rowset does not accurately reflect parameter defaults
            var script = GetProcedureScript(procDefinition);
        }

        private string GetProcedureScript(ProcDefinition procDefinition)
        {
            var nameWithSchema = $"{procDefinition.Identifier.Schema}.{procDefinition.Identifier.Name}";
            var script = new StringBuilder();
            using (var dataReader = new SprocWrapper.Procs.sys.sp_helptext(_sqlConnection, nameWithSchema).ExecuteDataReader())
            {
                while (dataReader.Read())
                {
                    script.Append(dataReader.GetString(0));
                }

                Logger.Log(script.ToString());
            }
            return script.ToString();
        }

        private void ReadBasicParameterDefinition(ProcIdentifier procIdentifier, ProcDefinition procDefinition)
        {
            using (var dataReader = new Procs.Dbo.sp_procedure_params_rowset(_sqlConnection, procIdentifier.Name, procedure_schema: procIdentifier.Schema).ExecuteDataReader())
            {
                while (dataReader.Read())
                {
                    var name = dataReader["PARAMETER_NAME"].ToString();
                    var sqlType = dataReader["TYPE_NAME"].ToString();
                    procDefinition.Parameters.Add(new ParameterDefinition(name, sqlType));
                }
            }
        }
    }
}