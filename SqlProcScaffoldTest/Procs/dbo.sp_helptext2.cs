using System.Data.SqlClient;

namespace SprocWrapper.Procs
{
    public partial class dbo
    {
        public class sp_helptext2 : Proc
        {
            public sp_helptext2
            (
                SqlConnection sqlConnection,
                string objname,
                string columnname
            )
            {
                CreateCommand(sqlConnection, nameof(dbo.sp_helptext2));
                AddParameterIfNotNull(nameof(objname), objname);
                AddParameterIfNotNull(nameof(columnname), columnname);
            }
            public sp_helptext2
            (
                string objname,
                string columnname
            )
            {
                CreateCommand(DefaultConnection, nameof(dbo.sp_helptext2));
                AddParameterIfNotNull(nameof(objname), objname);
                AddParameterIfNotNull(nameof(columnname), columnname);
            }
        }
    }
}
