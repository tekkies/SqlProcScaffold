using System.Data.SqlClient;

namespace SprocWrapper.Procs
{
    public partial class dbo
    {
        public class sp_sproc_wrapper_test : Proc
        {
            public sp_sproc_wrapper_test
            (
                SqlConnection sqlConnection,
                int intNoDefault,
                string varcharNoDefault,
                int? intNullDefault = null,
                int? intNumericDefault = null,
                string varcharNullDefault = null,
                string varcharValueDefault = null
            )
            {
                CreateCommand(sqlConnection, nameof(dbo.sp_sproc_wrapper_test));
                AddParameterIfNotNull(nameof(intNoDefault), intNoDefault);
                AddParameterIfNotNull(nameof(varcharNoDefault), varcharNoDefault);
                AddParameterIfNotNull(nameof(intNullDefault), intNullDefault);
                AddParameterIfNotNull(nameof(intNumericDefault), intNumericDefault);
                AddParameterIfNotNull(nameof(varcharNullDefault), varcharNullDefault);
                AddParameterIfNotNull(nameof(varcharValueDefault), varcharValueDefault);
            }
            public sp_sproc_wrapper_test
            (
                int intNoDefault,
                string varcharNoDefault,
                int? intNullDefault = null,
                int? intNumericDefault = null,
                string varcharNullDefault = null,
                string varcharValueDefault = null
            )
            {
                CreateCommand(DefaultConnection, nameof(dbo.sp_sproc_wrapper_test));
                AddParameterIfNotNull(nameof(intNoDefault), intNoDefault);
                AddParameterIfNotNull(nameof(varcharNoDefault), varcharNoDefault);
                AddParameterIfNotNull(nameof(intNullDefault), intNullDefault);
                AddParameterIfNotNull(nameof(intNumericDefault), intNumericDefault);
                AddParameterIfNotNull(nameof(varcharNullDefault), varcharNullDefault);
                AddParameterIfNotNull(nameof(varcharValueDefault), varcharValueDefault);
            }
        }
    }
}
