using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SprocWrapper
{
    class ProcWrapper
    {
        private static string _outputFolder= @"..\..\..\..\SqlProcScaffoldTest";

        public static void SprocWrapper(string connectionString, string like)
        {
            Logger.Log(connectionString);
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                var procs = GetProcs(sqlConnection, like);
                foreach (var proc in procs)
                {
                    var procComposer = new ProcComposer(sqlConnection, proc, _outputFolder);
                    procComposer.Compose();
                }
            }
        }

        private static List<ProcIdentifier> GetProcs(SqlConnection sqlConnection, string like)
        {
            var procs = new List<ProcIdentifier>();
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
                    var procIdentifier = new ProcIdentifier(procName, schema);
                    procs.Add(procIdentifier);
                    Logger.Log(procIdentifier.ToString());
                }
            }
            return procs;
        }

        private static void NewMethod(ProcIdentifier procIdentifier)
        {
            throw new System.NotImplementedException();
        }
    }
}