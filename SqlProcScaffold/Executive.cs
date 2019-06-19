using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using SprocWrapper;

namespace SqlProcScaffold
{
    class Executive
    {
        private static string _outputFolder;

        public static void SprocWrapper()
        {
            _outputFolder = CommandLineParser.Request.OutputFolder;
            using (var sqlConnection = new SqlConnection(CommandLineParser.Request.ConnectionString))
            {
                Logger.Log(Logger.Level.Info, "Connecting to database...");
                sqlConnection.Open();
                ProcComposer.WriteBaseClass();
                var procs = GetProcs(sqlConnection, CommandLineParser.Request.Filter);
                CheckForNoProcs(procs);
                Logger.Log(Logger.Level.Info, "Parsing parameters and writing output");
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
                Logger.Log(Logger.Level.Error, "ERROR: No stored procedures found");
                System.Environment.Exit(1);
            }
        }

        private static List<ProcIdentifier> GetProcs(SqlConnection sqlConnection, string like)
        {
            Logger.Log(Logger.Level.Info, "Finding procedures");
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
                    Logger.Log(Logger.Level.Verbose, $"    {procIdentifier}");
                }
            }
            return procs;
        }
    }
}