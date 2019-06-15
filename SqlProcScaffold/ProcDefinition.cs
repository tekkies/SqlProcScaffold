using System.Collections.Generic;

namespace SprocWrapper
{
    internal class ProcDefinition
    {
        public ProcIdentifier Identifier { get; }
        public List<ParameterDefinition> Parameters { get; set; }

        public ProcDefinition(ProcIdentifier procIdentifier)
        {
            Identifier = procIdentifier;
            Parameters = new List<ParameterDefinition>();
        }

    }
}