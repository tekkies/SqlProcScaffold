//File auto-generated using https://github.com/tekkies/SqlProcScaffold
using System.Data.SqlClient;
using JetBrains.Annotations;

namespace SqlProcScaffoldTest.Procs
{
    public partial class dbo
    {
        public class sp_make_pizza : Proc
        {
            public sp_make_pizza
            (
                [NotNull] string name,
                string baseType = null,
                string crust = null,
                bool? anchovies = null
            )
            {
                CreateCommand(DefaultConnection, nameof(dbo.sp_make_pizza));
                AddParameterIfNotNull(nameof(name), name);
                AddParameterIfNotNull(nameof(baseType), baseType);
                AddParameterIfNotNull(nameof(crust), crust);
                AddParameterIfNotNull(nameof(anchovies), anchovies);
            }
            public sp_make_pizza
            (
                SqlConnection sqlConnection,
                [NotNull] string name,
                string baseType = null,
                string crust = null,
                bool? anchovies = null
            )
            {
                CreateCommand(sqlConnection, nameof(dbo.sp_make_pizza));
                AddParameterIfNotNull(nameof(name), name);
                AddParameterIfNotNull(nameof(baseType), baseType);
                AddParameterIfNotNull(nameof(crust), crust);
                AddParameterIfNotNull(nameof(anchovies), anchovies);
            }
        }
    }
}
