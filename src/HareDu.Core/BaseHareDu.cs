namespace HareDu.Core;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class BaseHareDu
{
    protected readonly HttpClient Client;
    protected readonly IDictionary<string, string> ErrorReasons;

    protected BaseHareDu(HttpClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
        ErrorReasons = new Dictionary<string, string>
        {
            {nameof(MissingMethodException), "Could not properly handle '.' and/or '/' characters in URL."},
            {nameof(HttpRequestException), "Request failed due to network connectivity, DNS failure, server certificate validation, or timeout."},
            {nameof(JsonException), "The JSON is invalid or T is not compatible with the JSON."},
            {nameof(Exception), "Something went bad in BaseBrokerObject.GetAll method."},
            {nameof(TaskCanceledException), "Request failed due to timeout."}
        };
    }

    protected HttpContent GetRequestContent(string request)
    {
        byte[] payloadBytes = Encoding.UTF8.GetBytes(request);
        
        var content = new ByteArrayContent(payloadBytes);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Headers.ContentLength = payloadBytes.Length;

        return content;
    }
}