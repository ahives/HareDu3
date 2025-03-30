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

    protected Result GetFaultedExceptionResult(string url, string message, string stackTrace, Error error, string response = null) =>
        new FaultedResult {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    protected Result<T> GetFaultedExceptionResult<T>(string url, string response, string message, string stackTrace, Error error) =>
        new FaultedResult<T> {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    protected Results<T> GetFaultedExceptionResults<T>(string url, string response, string message, string stackTrace, Error error) =>
        new FaultedResults<T> {DebugInfo = new() {URL = url, Response = response, Exception = message, StackTrace = stackTrace, Errors = new List<Error> {error}}};

    protected Result<T> GetSuccessfulResult<T>(string url, string request, string response, T data) =>
        new SuccessfulResult<T> {Data = data, DebugInfo = new() {URL = url, Request = request, Response = response, Errors = new List<Error>()}};

    protected Results<T> GetSuccessfulResults<T>(string url, IReadOnlyList<T> data, string response, string request = null) =>
        new SuccessfulResults<T> {Data = data, DebugInfo = new() {URL = url, Request = request, Response = response, Errors = new List<Error>()}};

    protected Result GetSuccessfulResult(string url, string response, string request = null) =>
        new SuccessfulResult {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = new List<Error>()}};

    protected Result GetFaultedResult(string url, List<Error> errors, string response, string request = null) =>
        new FaultedResult {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    protected Result<T> GetFaultedResult<T>(string url, string response, List<Error> errors) =>
        new FaultedResult<T> {DebugInfo = new() {URL = url, Response = response, Errors = errors}};

    protected Result<T> GetFaultedResult<T>(string url, string request, string response, List<Error> errors) =>
        new FaultedResult<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

    protected Results<T> GetFaultedResults<T>(string url, List<Error> errors, string response, string request = null) =>
        new FaultedResults<T> {DebugInfo = new() {URL = url, Request = request, Response = response, Errors = errors}};

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