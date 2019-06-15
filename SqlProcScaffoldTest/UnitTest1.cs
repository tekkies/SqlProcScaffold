using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqlProcScaffoldTest.Procs;

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
            SqlProcScaffoldTest.Procs.dbo.sp_sproc_wrapper_test.DefaultConnection = null;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _sqlConnection.Dispose();
            _sqlConnection = null;
            SqlProcScaffoldTest.Procs.dbo.sp_sproc_wrapper_test.DefaultConnection = null;
        }

        [TestMethod]
        public void TestWithExplicitConnection()
        {
            using (var dataReader = new SqlProcScaffoldTest.Procs.dbo.sp_sproc_wrapper_test(
                _sqlConnection, 
                1, 
                "varcharNoDefault", 
                2,
                3,
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
            SqlProcScaffoldTest.Procs.Proc.DefaultConnection = _sqlConnection;
            using (var dataReader = new SqlProcScaffoldTest.Procs.dbo.sp_sproc_wrapper_test(
                1,
                "varcharNoDefault",
                2,
                3,
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
            SqlProcScaffoldTest.Procs.Proc.DefaultConnection = _sqlConnection;
            using (var dataReader = new SqlProcScaffoldTest.Procs.dbo.sp_sproc_wrapper_test(
                intNoDefault: 1,
                intNullDefault: null,
                intNumericDefault: null,
                varcharNoDefault: "varcharNoDefault",
                varcharNullDefault: null,
                varcharValueDefault: null)
                .ExecuteDataReader())
            {
                dataReader.Read();
                Assert.AreEqual(1, dataReader.GetInt32(0));
            }
        }
        
        [TestMethod]
        public void TestWithSparseValues()
        {
            SqlProcScaffoldTest.Procs.Proc.DefaultConnection = _sqlConnection;
            using (var dataReader = new SqlProcScaffoldTest.Procs.dbo.sp_sproc_wrapper_test(
                    1,
                    varcharNoDefault: "varcharNoDefault")
                .ExecuteDataReader())
            {
                dataReader.Read();
                Assert.AreEqual(1, dataReader.GetInt32(0));
            }
        }


        [TestMethod]
        public void ScreenshotSandpit()
        {
            
            //#region Before
            //SqlProcScaffoldTest.Procs.Proc.DefaultConnection = _sqlConnection;
            //var intNoDefault = 0;
            //string varcharNoDefault = string.Empty;
            //int? intNullDefault;
            //#endregion
            //var sqlCommand = _sqlConnection.CreateCommand();
            //sqlCommand.CommandText = "dbo.sp_make_pizza";
            //sqlCommand.CommandType = CommandType.StoredProcedure;
            //sqlCommand.Parameters.Add(new SqlParameter("@name", "Hawaiian"));
            //sqlCommand.Parameters.Add(new SqlParameter("@baseType", "stone baked"));
            //var hawaiian = sqlCommand.ExecuteReader();

            //#region After
            //hawaiian.Dispose();
            //#endregion
            
            ////new dbo.sp_make_pizza();
            
            //var pepperoniPlus = new dbo.sp_make_pizza(
            //        "Pepperoni",
            //        "deep pan",
            //        "cheese filled",
            //        anchovies: null)
            //    .ExecuteDataReader();
            
            //var pepperoni = new dbo.sp_make_pizza(
            //    "Pepperoni")
            //    .ExecuteDataReader();


            #region End
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            #endregion
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
