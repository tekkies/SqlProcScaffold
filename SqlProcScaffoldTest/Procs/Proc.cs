//File auto-generated using https://github.com/tekkies/SqlProcScaffold
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SqlProcScaffoldTest.Procs
{
    public class Proc
    {
        protected SqlCommand _command;

        public static SqlConnection DefaultConnection { get; set; }

        protected void AddParameterIfNotNull(string nameWithoutAt, object value)
        {
            if (value != null)
            {
                AddParameter(nameWithoutAt, value);
            }
        }

        protected void AddParameter(string name, object value)
        {
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

        public object ExecuteScalar()
        {
            using (var dataReader = ExecuteDataReader())
            {
                dataReader.Read();
                var scalar = dataReader[0];
                System.Diagnostics.Debug.Assert(dataReader.Read() == false);
                return scalar;
            }
        }
    }
}