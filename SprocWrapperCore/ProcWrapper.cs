using System.Collections.Generic;
using System.Data.SqlClient;

namespace SprocWrapper
{
    class ProcWrapper
    {
        public static void SprocWrapper(string connectionString, string like)
        {
            Logger.Log(connectionString);
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var procs = GetProcs(sqlConnection, like);
                foreach (var proc in procs)
                {
                    GetParams(sqlConnection, proc);
                }

                new SprocWrapper.Procs.Dbo.sp_sproc_wrapper_test(sqlConnection, 1, 2, 3).Execute();
            }
        }

        private static void GetParams(SqlConnection sqlConnection, string procName)
        {
            using (var dataReader = new SprocWrapper.Procs.Dbo.sp_procedure_params_rowset(sqlConnection, procName).ExecuteDataReader())
            {
                while (dataReader.Read())
                {
                    for (int columnIndex = 0; columnIndex < dataReader.FieldCount; columnIndex++)
                    {
                        Logger.Log("    {0}={1}", dataReader.GetName(columnIndex), dataReader[columnIndex]);
                    }
                }
            }
        }

        private static List<string> GetProcs(SqlConnection sqlConnection, string like)
        {
            var procs = new List<string>();
            using (var dataReader = new QueryBuilder(sqlConnection,
                    @"SELECT  
                    SS.name AS[schema],
                    SO.name
                        FROM sys.objects SO
                    LEFT JOIN sys.schemas SS ON SO.schema_id = SS.schema_id
                    WHERE SO.type = 'P'
                    AND SO.name like @0",
                    like)
                .ExecuteDataReader())
            {
                while (dataReader.Read())
                {
                    var schema = dataReader.GetString(0);
                    var procName = dataReader.GetString(1);
                    procs.Add(procName);
                    Logger.Log("[{0}].[{1}]", schema, procName);
                }
            }
            return procs;
        }
    }
}