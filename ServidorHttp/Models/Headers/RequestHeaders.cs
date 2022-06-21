namespace ServidorHttp.Models.Headers;
using ServidorHttp.Models.Enums;

public class RequestHeaders : Headers
{
    public RequestHeaders(IEnumerable<string> values)
    {
        this._values = values
            .Select(x => x.Split(':', StringSplitOptions.TrimEntries))
            .ToDictionary(key => key[0], value => value[1]);
    }

}
