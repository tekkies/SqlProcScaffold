using System;
using System.Data;

namespace SprocWrapper
{
    internal class SqlToCSharpTypeMapper
    {
        public static string MapSqlToCSharp(string sqlType, bool hasDefault)
        {
            string cSharpType;
            if (hasDefault)
            {
                cSharpType = GetNullableCSharpType(sqlType);
            }
            else
            {
                cSharpType = GetNotNullableCSharpType(sqlType);
            }

            return cSharpType;
        }


        private static string GetNullableCSharpType(string sqlType)
        {
            string cSharpType;
            switch (sqlType.ToLower())
            {
                case "bit":
                    cSharpType = "bool?";
                    break;
                case "int":
                    cSharpType = "int?";
                    break;
                case "datetime":
                    cSharpType = "DateTime";
                    break;
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    cSharpType = "string";
                    break;
                default:
                    throw new NotImplementedException($"Unable to map SQL data type \"{sqlType}\" to C# data type");
            }
            return cSharpType;
        }
        private static string GetNotNullableCSharpType(string sqlType)
        {
            string cSharpType;
            switch (sqlType.ToLower())
            {
                case "bit":
                    cSharpType = "bool";
                    break;
                case "int":
                    cSharpType = "int";
                    break;
                case "datetime":
                    cSharpType = "DateTime";
                    break;
                case "char":
                case "nchar":
                case "varchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    cSharpType = "string";
                    break;
                default:
                    throw new NotImplementedException($"Unable to map SQL data type \"{sqlType}\" to C# data type");
            }
            return cSharpType;
        }
    }
}