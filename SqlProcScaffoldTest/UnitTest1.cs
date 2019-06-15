using System;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SprocWrapperCoreTest
{
    [TestClass]
    public class UnitTest1
    {
        private static SqlConnection _sqlConnection;

        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            _sqlConnection = OpenDatabase();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            SprocWrapper.Procs.dbo.sp_sproc_wrapper_test.DefaultConnection = null;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _sqlConnection.Dispose();
            _sqlConnection = null;
            SprocWrapper.Procs.dbo.sp_sproc_wrapper_test.DefaultConnection = null;
        }

        [TestMethod]
        public void TestWithExplicitConnection()
        {
            using (var dataReader = new SprocWrapper.Procs.dbo.sp_sproc_wrapper_test(
                _sqlConnection, 
                1, 
                2, 
                3,
                "varcharNoDefault",
                "varcharNullDefault",
                "varcharValueDefault").ExecuteDataReader())
            {
                dataReader.Read();
                Assert.AreEqual(1, dataReader.GetInt32(0));
            }
        }

        [TestMethod]
        public void TestWithDefaultConnection()
        {
            SprocWrapper.Procs.Proc.DefaultConnection = _sqlConnection;
            using (var dataReader = new SprocWrapper.Procs.dbo.sp_sproc_wrapper_test(
                1,
                2,
                3,
                "varcharNoDefault",
                "varcharNullDefault",
                "varcharValueDefault")
                .ExecuteDataReader())
            {
                dataReader.Read();
                Assert.AreEqual(1, dataReader.GetInt32(0));
            }
        }

        [TestMethod]
        public void TestWithNullValues()
        {
            SprocWrapper.Procs.Proc.DefaultConnection = _sqlConnection;
            using (var dataReader = new SprocWrapper.Procs.dbo.sp_sproc_wrapper_test(
                intNoDefault: 1,
                intNullDefault: null,
                intNumericDefault: null,
                varcharNoDefault: null, //ToDo: Get a compile time error here?
                varcharNullDefault: null,
                varcharValueDefault: null)
                .ExecuteDataReader())
            {
                dataReader.Read();
                Assert.AreEqual(1, dataReader.GetInt32(0));
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
