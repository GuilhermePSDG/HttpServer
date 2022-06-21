using ServidorHttp.Models.Enums;
using ServidorHttp.Models.Headers;

namespace ServidorHttp.Models;
public class Response 
{
    public readonly ResponseHeaders Headers;
    public bool HasContent { get; init; }
    public byte[] Data => HasContent ? _Data : throw new Exception("No content availabe");
    
    private byte[] _Data { get; }



    public Response(byte[] Data,StatusCode code,string ContentType)
    {
        this.HasContent = true;
        this.Headers = new ResponseHeaders(code, ContentType, Data.Length);
        this._Data = Data;
    }
    public Response(StatusCode code, string ContentType)
    {
        this.HasContent = false;
        this.Headers = new ResponseHeaders(code, ContentType, 0);
        this._Data = new byte[0];
    }
}
