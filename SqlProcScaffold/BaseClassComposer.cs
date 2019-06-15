using System.IO;
using System.Security.Cryptography.X509Certificates;
using SqlProcScaffold;

static internal class BaseClassComposer
{
    public const string AutoGenComment = "//File auto-generated using https://github.com/tekkies/SqlProcScaffold";

    public static void WriteBaseClass()
    {
        var className = typeof(SprocWrapper.Procs.Proc).Name;
        var file = Path.Join(CommandLineParser.Request.OutputFolder, $"{className}.cs");
        using (var streamWriter = new StreamWriter(file))
        {
            var templateCode = SqlProcScaffold.Properties.Resources.Proc;
            var renderedCode = templateCode.Replace("namespace SprocWrapper.Procs", $"namespace {CommandLineParser.Request.NameSpace}");
            streamWriter.WriteLine(AutoGenComment);
            streamWriter.Write(renderedCode);
        }
    }
}