namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core;

internal class BaseHareDu
{
    protected readonly HttpClient Client;
    protected readonly IDictionary<string, Error> InternalErrors;

    protected BaseHareDu(HttpClient client)
    {
        Client = client ?? throw new ArgumentNullException(nameof(client));
        InternalErrors = new Dictionary<string, Error>
        {
            {nameof(MissingMethodException), new() {Reason = "Could not properly handle '.' and/or '/' characters in URL."}},
            {nameof(HttpRequestException), new() {Reason = "Request failed due to network connectivity, DNS failure, server certificate validation, or timeout."}},
            {nameof(JsonException), new() {Reason = "The JSON is invalid or T is not compatible with the JSON."}},
            {nameof(Exception), new() {Reason = "Something went bad in BaseBrokerObject.GetAll method."}},
            {nameof(TaskCanceledException), new() {Reason = "Request failed due to timeout."}}
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