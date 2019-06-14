using System.Data.SqlClient;

namespace SprocWrapper.Procs
{
    public partial class Dbo
    {
        public class sp_sproc_wrapper_test : Proc
        {
            public sp_sproc_wrapper_test(SqlConnection sqlConnection, int i, int i1, int i2)
            {
                throw new System.NotImplementedException();
            }

            public void Execute()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}