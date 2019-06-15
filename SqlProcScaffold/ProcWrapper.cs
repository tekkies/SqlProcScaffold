using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mime;
using SqlProcScaffold.Properties;

namespace SprocWrapper
{
    class ProcWrapper
    {
        private static string _outputFolder;

        public static void SprocWrapper(string connectionString, string like, string outputFolder)
        {
            _outputFolder = outputFolder;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                WriteBaseClass();
                var procs = GetProcs(sqlConnection, like);
                CheckForNoProcs(procs);
                foreach (var proc in procs)
                {
                    var procComposer = new ProcComposer(sqlConnection, proc, _outputFolder);
                    procComposer.Compose();
                }
            }
        }

        private static void CheckForNoProcs(List<ProcIdentifier> procs)
        {
            if (procs.Count == 0)
            {
                Logger.Log("ERROR: No stored procedures found");
                System.Environment.Exit(1);
            }
        }

        private static void WriteBaseClass()
        {
            var className = typeof(SprocWrapper.Procs.Proc).Name;
            var file = Path.Join(_outputFolder, $"{className}.cs");
            using (var streamWriter = new StreamWriter(file))
            {
                streamWriter.Write(SqlProcScaffold.Properties.Resources.Proc);
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
                    AND SS.name+'.'+SO.name like @0",
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
    }
}