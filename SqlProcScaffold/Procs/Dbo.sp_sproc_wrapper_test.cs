using System.Data.SqlClient;

namespace SprocWrapper.Procs
{
    public partial class Dbo
    {
        public class sp_sproc_wrapper_test : Proc
        {
            public sp_sproc_wrapper_test(SqlConnection sqlConnection,int intNoDefault, int? intNullDefault, int? intNumericDefault)
            {
                AddParameter(nameof(intNoDefault), intNoDefault);
                AddParameterIfNotNull(nameof(intNullDefault), intNullDefault);
                AddParameterIfNotNull(nameof(intNumericDefault), intNumericDefault);
            }
        }
    }
}