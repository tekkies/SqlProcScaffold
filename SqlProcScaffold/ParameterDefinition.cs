namespace SprocWrapper
{
    internal class ParameterDefinition
    {
        public string NameWithAt { get; }
        public string SqlType { get; }
        public string NameWithoutAt { get; }

        public bool HasDefault { get; set; }

        public ParameterDefinition(string nameWithAt, string sqlType)
        {
            NameWithAt = nameWithAt;
            SqlType = sqlType;
            NameWithoutAt = nameWithAt.Substring(1);
        }

        public string GetCSharpType(bool hasDefault)
        {
            return SqlToCSharpTypeMapper.MapSqlToCSharp(SqlType, hasDefault);
        }

    }
}