namespace ServidorHttp.Extensions;

using ServidorHttp.Models;
using ServidorHttp.Models.Headers;
using System.Net.Sockets;
using System.Text;

public static class SocketExtensions
{
    public static async Task<int> SendAsync(this Socket sock,Response response, SocketFlags socketFlag = SocketFlags.None)
    {
        int TotalSended = await sock.SendAsync(response.Headers,socketFlag);
        if (response.HasContent)
        {
            var content = response.Data;
            TotalSended += await sock.SendAsync(content,socketFlag);
        }
        return TotalSended;
    }
    public static async Task<int> SendAsync(this Socket sock, ResponseHeaders headers, SocketFlags socketFlag = SocketFlags.None)
    {
        var arr = headers.ToByteArray();
        return await sock.SendAsync(arr, socketFlag);
    }
    public static async Task<Request> ReceiveAsync(this Socket sock,SocketFlags flags = SocketFlags.None,int BufferSize = 1024)
    {
        var buff = new byte[BufferSize];
        var received = await sock.ReceiveAsync(buff, flags);
        if(received > 0)
        {
            return new Request(buff, received);
        }
        else
        {
            return Request.Empty;
        }
      
    }

}