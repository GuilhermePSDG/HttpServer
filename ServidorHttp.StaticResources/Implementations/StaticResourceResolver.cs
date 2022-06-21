
using System.Text.Json;
namespace ServidorHttp.StaticResources.Implementations;
public class StaticResourceResolver : IRequestResolver
{
    public record SuportedType(string Extension, string Type);

    private const string TextDefaultType = "text/html;charset=utf 8";
    public string SuportedTypesJsonPath = "SuportedTypes.json";
    public StaticResourcePathParser PathParser { get; }
    public Dictionary<string, string> SuportedTypes { get; }

    public sbyte Priority { get; set; } = sbyte.MinValue;

    public StaticResourceResolver(StaticResourcePathParser pathParser)
    {
        PathParser = pathParser;
        var suportedTypeJson = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, SuportedTypesJsonPath));
        var suportedTypes = JsonSerializer.Deserialize<List<SuportedType>>(suportedTypeJson) ?? throw new NullReferenceException("No suported type provide");
        SuportedTypes = suportedTypes.ToDictionary(x => x.Extension, x => x.Type);
    }

    public async Task<Response> Resolve(Request request)
    {
        if (!PathParser.TryGetFileInfo(request.Headers, request.Path, out var FileInfo) || !FileInfo.Exists)
            return new Response(StatusCode.NOT_FOUND, TextDefaultType);
        if (!SuportedTypes.TryGetValue(FileInfo.Extension, out var contentType))
            return new Response(StatusCode.UNSUPORTED_TYPE, TextDefaultType);
        var data = await File.ReadAllBytesAsync(FileInfo.FullName);
        return new Response(data, StatusCode.OK, contentType);
    }

    public Task<bool> CanResolve(Request request) => Task.FromResult(true);

}




