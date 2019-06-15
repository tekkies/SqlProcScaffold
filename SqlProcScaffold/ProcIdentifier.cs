namespace SprocWrapper
{
    internal class ProcIdentifier
    {
        public ProcIdentifier(string name, string schema)
        {
            Name = name;
            Schema = schema;
        }

        public string Name { get; private set; }
        public string Schema { get; private set; }
        public override string ToString()
        {
            return $"[{Schema}].[{Name}]";
        }
    }
}