using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SprocWrapper.Procs
{
    public partial class Dbo
    {
        public class sp_procedure_params_rowset : Proc
        {
            protected SqlCommand _command;

            public sp_procedure_params_rowset(SqlConnection sqlConnection, 
                string procedure_name,
                int? group_number=null,
                string procedure_schema=null,
                string parameter_name=null
            )
            {
                _command = sqlConnection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = nameof(sp_procedure_params_rowset);
                AddParameter(nameof(procedure_name), procedure_name);
                AddParameterIfNotNull(nameof(group_number), group_number);
                AddParameterIfNotNull(nameof(procedure_schema), procedure_schema);
                AddParameterIfNotNull(nameof(parameter_name), parameter_name);
            }

            private void AddParameterIfNotNull(string name, object value)
            {
                if (value != null)
                {
                    AddParameter(name, value);
                }
            }

            private void AddParameter(string name, object value)
            {
                Logger.Log("AddParameter @{0} {1}", name, value);
                _command.Parameters.Add(new SqlParameter($"@{name}", value));
            }

            public DbDataReader ExecuteDataReader()
            {
                return _command.ExecuteReader();
            }

            public void Execute()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}