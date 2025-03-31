namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core;

internal class BaseHareDu
{
    protected readonly IHttpClientFactory ClientFactory;
    protected readonly IDictionary<string, Error> Errors;

    protected BaseHareDu(IHttpClientFactory clientFactory)
    {
        ClientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        Errors = new Dictionary<string, Error>
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

    protected void HandleDotsAndSlashes()
    {
        var method = typeof(UriParser).GetMethod("GetSyntax", BindingFlags.Static | BindingFlags.NonPublic);
        if (method is null)
            throw new MissingMethodException("UriParser", "GetSyntax");

        var uriParser = method.Invoke(null, new object[] {"http"});

        var setUpdatableFlagsMethod = uriParser
            .GetType()
            .GetMethod("SetUpdatableFlags", BindingFlags.Instance | BindingFlags.NonPublic);
            
        if (setUpdatableFlagsMethod is null)
            throw new MissingMethodException("UriParser", "SetUpdatableFlags");

        setUpdatableFlagsMethod.Invoke(uriParser, new object[] {0});
    }

    protected Error GetError(HttpStatusCode statusCode)
    {
        switch (statusCode)
        {
            case HttpStatusCode.BadRequest:
                return new() {Reason = "RabbitMQ server did not recognize the request due to malformed syntax."};
                
            case HttpStatusCode.Forbidden:
                return new() {Reason = "RabbitMQ server rejected the request."};
                
            case HttpStatusCode.NotAcceptable:
                return new() {Reason = "RabbitMQ server rejected the request because the method is not acceptable."};

            case HttpStatusCode.MethodNotAllowed:
                return new() {Reason = "RabbitMQ server rejected the request because the method is not allowed."};
                
            case HttpStatusCode.InternalServerError:
                return new() {Reason = "Internal error happened on RabbitMQ server."};
                
            case HttpStatusCode.RequestTimeout:
                return new() {Reason = "No response from the RabbitMQ server within the specified window of time."};
                
            case HttpStatusCode.ServiceUnavailable:
                return new() {Reason = "RabbitMQ server temporarily not able to handle request"};
                
            case HttpStatusCode.Unauthorized:
                return new() {Reason = "Unauthorized access to RabbitMQ server resource."};
                
            default:
                return null;
        }
    }
}