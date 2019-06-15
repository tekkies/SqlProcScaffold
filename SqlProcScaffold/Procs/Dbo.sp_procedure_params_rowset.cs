using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace SprocWrapper.Procs
{
    public partial class Dbo
    {
        public class sp_procedure_params_rowset : Proc
        {
            public sp_procedure_params_rowset(SqlConnection sqlConnection, 
                string procedure_name,
                int? group_number=null,
                string procedure_schema=null,
                string parameter_name=null
            )
            {
                CreateCommand(sqlConnection, nameof(Dbo.sp_procedure_params_rowset));
                AddParameter(nameof(procedure_name), procedure_name);
                AddParameterIfNotNull(nameof(group_number), group_number);
                AddParameterIfNotNull(nameof(procedure_schema), procedure_schema);
                AddParameterIfNotNull(nameof(parameter_name), parameter_name);
            }
        }
    }
}