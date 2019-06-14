using System;
using SprocWrapper;

namespace SprocWrapperCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = args[0];
            var like = args[1];
            ProcWrapper.SprocWrapper(connectionString, like);
        }
    }
}
