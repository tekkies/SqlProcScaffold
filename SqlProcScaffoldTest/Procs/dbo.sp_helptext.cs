using System.Data.SqlClient;

namespace SqlProcScaffoldTest.Procs
{
    public partial class dbo
    {
        public class sp_helptext : Proc
        {
            public sp_helptext
            (
                SqlConnection sqlConnection,
                string objname,
                string columnname
            )
            {
                CreateCommand(sqlConnection, nameof(dbo.sp_helptext));
                AddParameterIfNotNull(nameof(objname), objname);
                AddParameterIfNotNull(nameof(columnname), columnname);
            }
            public sp_helptext
            (
                string objname,
                string columnname
            )
            {
                CreateCommand(DefaultConnection, nameof(dbo.sp_helptext));
                AddParameterIfNotNull(nameof(objname), objname);
                AddParameterIfNotNull(nameof(columnname), columnname);
            }
        }
    }
}
