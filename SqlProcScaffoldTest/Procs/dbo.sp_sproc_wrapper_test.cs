//File auto-generated using https://github.com/tekkies/SqlProcScaffold
using System.Data.SqlClient;
using System;
using JetBrains.Annotations;

namespace SqlProcScaffoldTest.Procs
{
    public partial class dbo
    {
        public class sp_sproc_wrapper_test : Proc
        {
            public sp_sproc_wrapper_test
            (
                [NotNull] int intNoDefault,
                [NotNull] string varcharNoDefault,
                int? intNullDefault = null,
                int? intNumericDefault = null,
                string varcharNullDefault = null,
                string varcharValueDefault = null
            )
            {
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
