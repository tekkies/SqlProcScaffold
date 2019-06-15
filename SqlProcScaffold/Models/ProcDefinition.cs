using System;
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

        public void SortParametersRequriedFirst()
        {
            Parameters.Sort(new ParameterDefinitionComparer());
        }

        private class ParameterDefinitionComparer : IComparer<ParameterDefinition>
        {
            public int Compare(ParameterDefinition x, ParameterDefinition y)
            {
                return RequiredParamsFirstTryingToMaintainOriginalOrder(x, y);
            }

            private static int RequiredParamsFirstTryingToMaintainOriginalOrder(ParameterDefinition x, ParameterDefinition y)
            {
                var comparison = x.HasDefault.CompareTo(y.HasDefault);
                if (comparison == 0)
                {
                    comparison = x.OriginalOrder.CompareTo(y.OriginalOrder);
                }
                return comparison;
            }
        }
    }
}