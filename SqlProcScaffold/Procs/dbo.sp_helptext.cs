using System.Data.SqlClient;

namespace SprocWrapper.Procs
{
    public partial class sys
    {
        public class sp_helptext : Proc
        {
            public sp_helptext
            (
                SqlConnection sqlConnection,
                string objname,
                string columnname = null
            )
            {
                CreateCommand(sqlConnection, nameof(sys.sp_helptext));
                AddParameterIfNotNull(nameof(objname), objname);
                AddParameterIfNotNull(nameof(columnname), columnname);
            }
            public sp_helptext
            (
                string objname,
                string columnname = null
            )
            {
                CreateCommand(DefaultConnection, nameof(sys.sp_helptext));
                AddParameterIfNotNull(nameof(objname), objname);
                AddParameterIfNotNull(nameof(columnname), columnname);
            }
        }
    }
}
