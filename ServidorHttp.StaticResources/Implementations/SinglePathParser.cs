namespace ServidorHttp.StaticResources.Implementations;
public class SinglePathParser : StaticResourcePathParser
{
    public SinglePathParser(string BasePath)
    {
        this.BasePath = BasePath;
    }
    public string BasePath { get; }

    public bool TryGetFileInfo(RequestHeaders headers, string RelativePath, out FileInfo fileInfo)
    {
        RelativePath = RelativePath == "/" ? "\\index.html" : RelativePath.Replace('/', '\\');
        fileInfo = new FileInfo(BasePath + RelativePath);
        return fileInfo.Exists;
    }
}




