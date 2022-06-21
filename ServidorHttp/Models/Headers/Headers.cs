using System.Diagnostics.CodeAnalysis;

namespace ServidorHttp.Models.Headers;
public abstract class Headers
{ 
    protected Dictionary<string, string> _values { get; init; } =new();
    public string this[string key] => _values[key];
    public virtual bool TryGet(string key, [NotNullWhen(true)]out string? value)
    {
        if(!this._values.TryGetValue(key, out value))
            return false;
        if (value == null)
            return false;
        return true;
    }

}
