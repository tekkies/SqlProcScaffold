//File auto-generated using https://github.com/tekkies/SqlProcScaffold
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SqlProcScaffoldTest.Procs
{
    public class Proc
    {
        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();

        public static DbConnection DefaultConnection { get; set; }

        public DbConnection Connection { get; set; }

        protected void AddParameterIfNotNull(string nameWithoutAt, object value)
        {
            if (value != null)
            {
                AddParameter(nameWithoutAt, value);
            }
        }

        protected void AddParameter(string name, object value)
        {

            if (value == null)
            {
                value = DBNull.Value;
            }
            Parameters.Add($"@{name}", value);
        }

        public DbDataReader ExecuteDataReader()
        {
            var dbCommand = CreateCommand();
            BindParameters(dbCommand);
            return dbCommand.ExecuteReader();
        }

        public void Execute()
        {
            var dbCommand = CreateCommand();
            BindParameters(dbCommand);
            dbCommand.ExecuteNonQuery();
        }

        private void BindParameters(DbCommand dbCommand)
        {
            foreach (var parameter in Parameters)
            {
                dbCommand.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
            }
        }

        protected DbCommand CreateCommand()
        {
            var connection = Connection ?? DefaultConnection;
            var command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            var procedureName = this.GetType().Name;
            command.CommandText = procedureName;
            return command;
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

        public Proc SetConnection(DbConnection sqlConnection)
        {
            Connection = sqlConnection;
            return this;
        }
    }
}