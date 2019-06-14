using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SprocWrapperCoreTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var sqlConnection = OpenDatabase();
            using (sqlConnection )
            {
                using (var dataReader = new SprocWrapper.Procs.Dbo.sp_sproc_wrapper_test(sqlConnection, 1, 2, 3).ExecuteDataReader())
                {
                    dataReader.Read();
                    Assert.AreEqual(1, dataReader.GetInt32(0));
                }
            }
        }

        private static SqlConnection OpenDatabase()
        {
            string connectionString = Environment.GetEnvironmentVariable("databaseConnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }
    }
}
