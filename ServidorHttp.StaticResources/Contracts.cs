using System.Diagnostics.CodeAnalysis;

namespace ServidorHttp.StaticResources
{
    public interface StaticResourcePathParser
    {
        public bool TryGetFileInfo(RequestHeaders headers, string RelativePath, [NotNullWhen(true)] out FileInfo? fileInfo);
    }

}
