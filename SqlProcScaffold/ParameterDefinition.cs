namespace SprocWrapper
{
    internal class ParameterDefinition
    {
        public string NameWithAt { get; }
        public string SqlType { get; }
        public int OriginalOrder { get; }
        public string NameWithoutAt { get; }

        public bool HasDefault { get; set; }

        public ParameterDefinition(string nameWithAt, string sqlType, int originalOrder)
        {
            NameWithAt = nameWithAt;
            SqlType = sqlType;
            OriginalOrder = originalOrder;
            NameWithoutAt = nameWithAt.Substring(1);
        }

        public string GetCSharpType(bool hasDefault)
        {
            return SqlToCSharpTypeMapper.MapSqlToCSharp(SqlType, hasDefault);
        }

    }
}