using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SprocWrapper
{
    internal class QueryBuilder
    {
        private SqlCommand _command;
        private readonly SqlConnection _sqlConnection;
        private string _sql;

        public QueryBuilder(SqlConnection sqlConnection, string sql, params object[] args)
        {
            _command = sqlConnection.CreateCommand();
            _sqlConnection = sqlConnection;
            BindParameters(args);
            _sql = sql;
        }

        private void BindParameters(object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                AddParameter("@" + i, args[i]);
            }
        }

        private void AddParameter(string name, object value)
        {
            _command.Parameters.Add(name, value);
        }

        public DbDataReader ExecuteDataReader()
        {
            _command.CommandText = _sql;
            return _command.ExecuteReader();
        }
    }
}