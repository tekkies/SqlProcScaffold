using System.Data;

namespace SprocWrapper
{
    internal class SqlToCSharpTypeMapper
    {
        public static string MapSqlToCSharp(string sqlType)
        {
            var cSharpType = sqlType;
            switch (sqlType)
            {
                case "char":
                case "varchar":
                case "text":
                    cSharpType = "string";
                    break;
            }
            return cSharpType;
        }
    }
}