using ServidorHttp.Models.Headers;
using System.Text;

namespace ServidorHttp.Models;
public class Request
{
    public Method Method { get; init; }
    public string Path { get; init; }
    public string HttpVersion { get; init; }
    //
    public readonly bool IsEmpty = false;
    public RequestHeaders Headers { get; }
    public static Request Empty => new Request(true);
    private Request(bool IsEmpty)
    {
        this.IsEmpty = IsEmpty;
        Headers = new RequestHeaders(new List<string>());
        Method = Method.UNSET;
        Path = "";
        HttpVersion = "";
    }
    public Request(byte[] RequestData, int count)
    {
        var strVersion = Encoding.UTF8.GetString(RequestData, 0, count);
        var values = strVersion.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
        var methodPathAndHttpVersions = values[0].Split(' ');
        this.Method = Enum.Parse<Method>(methodPathAndHttpVersions[0]);
        this.Path = methodPathAndHttpVersions[1];
        this.HttpVersion = methodPathAndHttpVersions[2];
        Headers = new RequestHeaders(values.Skip(1));
    }
}
