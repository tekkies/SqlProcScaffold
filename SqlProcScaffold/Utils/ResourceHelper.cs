using System.IO;
using System.Reflection;

namespace SqlProcScaffold.Utils
{
    internal class ResourceHelper
    {
        public static string GetResourceAsString(string name)
        {
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream(name))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}