
using System.Diagnostics.CodeAnalysis;
namespace ServidorHttp.Contracts;



public interface IRequestResolver
{
    /// <summary>
    /// Higher is executed first, if has one at same layer, will throw
    /// </summary>
    public sbyte Priority { get;}
    public Task<Response> Resolve(Request request);
    public Task<bool> CanResolve(Request request);
}


