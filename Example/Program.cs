
using ServidorHttp.StaticResources.Extensions;

var builder = HttpServer.CreateBuilder();

builder.UseStaticResource(opt =>
{
    opt.BasePath = @"C:\Users\0\source\repos\ServidorHttp\Example\www\";
    opt
    .UseDiferentsFoldersForDiferentHosts()
    .AddPathForHost("localhost", "localhost")
    .AddPathForHost("teste.com", "teste");
    ;
});

var server = builder.Build();
await server.Run();
