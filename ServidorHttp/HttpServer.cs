using System.Net;
using System.Net.Sockets;

public class HttpServer 
{
    private TcpListener listener { get; }
    public List<IRequestResolver> Resolvers { get; }
    private int Port { get; set; }

    public static IServerBuilder CreateBuilder() => new ServerBuilder();

    internal HttpServer(List<IRequestResolver> resolvers, string Ip, int Port)
    {
        this.Resolvers = resolvers;
        this.Port = Port;
        this.listener = new TcpListener(IPAddress.Parse(Ip), this.Port);
        this.listener.Start();
        Console.WriteLine($"Listen on port : {this.Port}");
        Console.WriteLine($"At adress : http://localhost:{this.Port}");
    } 

    public async Task Run() => await WaitConnection();

    private async Task WaitConnection()
    {
        while (true)
        {
            using var con = this.listener.AcceptSocket();
            await ProcessRequest(con);
        }
    }

    public async Task ProcessRequest(Socket socket)
    {
        Console.WriteLine($"Request received");
        if (socket.Connected)
        {
            try
            {
                Request request = await socket.ReceiveAsync();
                if (request.IsEmpty)
                {
                    Console.WriteLine("Empty request.. discarding.");
                    return;
                }
                Response response = await ResolveResponse(request);
                Console.WriteLine($"Request from host: {request.Headers["Host"]}" +
                  $"\n\tPath : {request.Path}" +
                  $"\n\tMethod : {request.Method.ToString()}" +
                  $"\n\tReponse Code : {response.Headers.StatusCode}" +
                  $"\n\tHas content : {(response.HasContent ? "yes" : "no")}");
                await socket.SendAsync(response);
                socket.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error while trying to precess request, ex :{ex.Message}");
            }
        }
    }

    private async Task<Response> ResolveResponse(Request request)
    {
        foreach(var resolver in Resolvers)
            if (await resolver.CanResolve(request))
                return await resolver.Resolve(request);
        return new Response(StatusCode.NOT_FOUND, "text/html");
    }
    

}