
using System.Diagnostics.CodeAnalysis;
namespace ServidorHttp.Contracts;



public interface IRequestResolver
{
    public Task<Response> Resolve(Request request);
    public Task<bool> CanResolve(Request request);
}


