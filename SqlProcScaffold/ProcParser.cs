using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using SqlProcScaffold;

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
            procDefinition.SortParametersRequriedFirst();
            Log(procDefinition);
            return procDefinition;
        }

        private void Log(ProcDefinition procDefinition)
        {
            Logger.Log(Logger.Level.Verbose, $"    {procDefinition.Identifier}");
            var procDefinitionParameters = procDefinition.Parameters;
            for (var index = 1; index < procDefinitionParameters.Count; index++)
            {
                {
                    var procDefinitionParameter = procDefinitionParameters[index];
                    Logger.Log(Logger.Level.Verbose, $"        {procDefinitionParameter}");
                }
            }
        }

        private void ParseParameterDefaults(ProcDefinition procDefinition)
        {
            //sys.sp_procedure_params_rowset does not accurately reflect parameter defaults.
            //We have to parse it from the text
            var script = GetProcedureScript(procDefinition);
            var parameterDefinitions = ParseParameterDefinitions(script);
            for (var parameterIndex = 1; parameterIndex < procDefinition.Parameters.Count; parameterIndex++)
            {
                UpdateParameterDefinitionWithHasDefault(procDefinition, parameterIndex, parameterDefinitions);
            }
        }

        private static void UpdateParameterDefinitionWithHasDefault(ProcDefinition procDefinition, int parameterIndex, List<string> parameterDefinitions)
        {
            var nameWithoutAt = procDefinition.Parameters[parameterIndex].NameWithoutAt;
            var paramDefinition = parameterDefinitions[parameterIndex - 1];
            //TODO: Parse the parameter value for more reliability (it might be useful) https://dotnetfiddle.net/fHMeFk
            var unreliableParameterHasDefault = 
                paramDefinition.Contains(nameWithoutAt, StringComparison.OrdinalIgnoreCase) 
                && paramDefinition.Contains("=", StringComparison.OrdinalIgnoreCase);
            procDefinition.Parameters[parameterIndex].HasDefault = unreliableParameterHasDefault;
        }

        private static List<string> ParseParameterDefinitions(string script)
        {
            var regex = new Regex(@"CREATE\s+PROCEDURE\s+.*\s+(@.*\s+)*\s+AS");
            var match = regex.Match(script);
            var parameterCaptures = match.Groups[1].Captures;
            var parameterDefinitions = new List<string>();
            foreach (var parameterCapture in parameterCaptures)
            {
                parameterDefinitions.Add(parameterCapture.ToString());
            }

            return parameterDefinitions;
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
                    var originalOrder = dataReader.GetInt16(dataReader.GetOrdinal("ORDINAL_POSITION"))-1;
                    procDefinition.Parameters.Add(new ParameterDefinition(name, sqlType, originalOrder));
                }
            }
        }
    }
}