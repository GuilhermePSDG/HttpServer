
using System.Diagnostics.CodeAnalysis;
namespace ServidorHttp.StaticResources.Implementations;
public class ScopedPathParser : StaticResourcePathParser
{
    public ScopedPathParser(string BasePath)
    {
        this.BasePath = BasePath;
        hosts = new();
    }
    private string BasePath { get; }
    private Dictionary<string, string> hosts { get; }
    public void AddScopedHost(string HostName, string PhysicalSubPath) => hosts.Add(HostName, PhysicalSubPath);
    public bool TryGetFileInfo(RequestHeaders headers, string RelativePath, [NotNullWhen(true)] out FileInfo? fileInfo)
    {
        if (!headers.TryGet("Host", out var host))
        {
            fileInfo = null;
            return false;
        }
        if (!hosts.TryGetValue(host, out var hostPath))
        {
            fileInfo = null;
            return false;
        }
        RelativePath = RelativePath == "/" ? "\\index.html" : RelativePath.Replace('/', '\\');
        var fullPath = BasePath + hostPath + RelativePath;
        fileInfo = new FileInfo(fullPath);
        return fileInfo.Exists;
    }
}




