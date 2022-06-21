namespace ServidorHttp.Models.Headers;

using ServidorHttp.Models.Enums;
using System.Text;

public class ResponseHeaders : Headers
{
    public StatusCode StatusCode { get; private set; }
    public ResponseHeaders(StatusCode StatusCode,string ContentType,int ContentLenght)
    {
        this.StatusCode = StatusCode;
        this._values = new();
        this.Add("Server", ServerConfiguration.SERVER_NAME);
        this.Add("Content-Type", ContentType);
        this.Add("Content-Lenght", ContentLenght);
    }
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"{ServerConfiguration.HTTP_VERSION} {(int)StatusCode}");
        foreach (var keyValue in this._values)
        {
            builder.AppendLine($"{keyValue.Key} : {keyValue.Value}");
        }
        builder.Append(Environment.NewLine);
        return builder.ToString();
    }
    public byte[] ToByteArray() => Encoding.UTF8.GetBytes(ToString());
    public void Add(string key,string value)
    {
        _values.Add(key, value);
    }
    public void Add(string key, object value)
    {
        _values.Add(key, value.ToString() ?? throw new NullReferenceException(nameof(value)));
    }
}
