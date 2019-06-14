using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SprocWrapper.Procs
{
    public class Proc
    {
        protected SqlCommand _command;

        protected void AddParameterIfNotNull(string name, object value)
        {
            if (value != null)
            {
                AddParameter(name, value);
            }
        }

        protected void AddParameter(string name, object value)
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
            _command.ExecuteNonQuery();
        }

        protected void CreateCommand(SqlConnection sqlConnection, string procedureName)
        {
            _command = sqlConnection.CreateCommand();
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = procedureName;
        }
    }
}