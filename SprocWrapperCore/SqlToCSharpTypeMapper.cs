using System;
using System.Data;

namespace SprocWrapper
{
    internal class SqlToCSharpTypeMapper
    {
        public static string MapSqlToCSharp(string sqlType)
        {
            string cSharpType;
            switch (sqlType)
            {
                case "char":
                case "varchar":
                case "text":
                    cSharpType = "string";
                    break;
                case "int":
                    cSharpType = "int";
                    break;
                default:
                    throw new NotImplementedException($"Unable to map SQL data type {sqlType} to C# data type");
            }
            return cSharpType;
        }
    }
}