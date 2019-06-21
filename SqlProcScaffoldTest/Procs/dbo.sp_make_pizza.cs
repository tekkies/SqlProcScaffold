//File auto-generated using https://github.com/tekkies/SqlProcScaffold v0.9.3
using System.Data.SqlClient;
using System;
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
                AddParameterIfNotNull(nameof(name), name);
                AddParameterIfNotNull(nameof(baseType), baseType);
                AddParameterIfNotNull(nameof(crust), crust);
                AddParameterIfNotNull(nameof(anchovies), anchovies);
            }
        }
    }
}
