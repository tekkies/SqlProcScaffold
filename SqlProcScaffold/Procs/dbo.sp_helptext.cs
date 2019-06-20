using System.Data.SqlClient;

namespace SprocWrapper.Procs
{
    public partial class sys
    {
        public class sp_helptext : Proc
        {
            public sp_helptext
            (
                string objname,
                string columnname = null
            )
            {
                AddParameterIfNotNull(nameof(objname), objname);
                AddParameterIfNotNull(nameof(columnname), columnname);
            }
        }
    }
}
