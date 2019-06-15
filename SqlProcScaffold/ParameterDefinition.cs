namespace SprocWrapper
{
    internal class ParameterDefinition
    {
        public string NameWithAt { get; }
        public string SqlType { get; }
        public string NameWithoutAt { get; }

        public string CSharpType => GetCSharpType();
        public bool HasDefault { get; set; }

        public ParameterDefinition(string nameWithAt, string sqlType)
        {
            NameWithAt = nameWithAt;
            SqlType = sqlType;
            NameWithoutAt = nameWithAt.Substring(1);
        }

        private string GetCSharpType()
        {
            return SqlToCSharpTypeMapper.MapSqlToCSharp(SqlType);
        }

    }
}