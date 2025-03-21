﻿namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Serialization;

public class BaseBrokerImpl
{
    readonly IHttpClientFactory _clientFactory;
    readonly IDictionary<string, Error> _errors;

    protected BaseBrokerImpl(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        _errors = new Dictionary<string, Error>
        {
            {nameof(MissingMethodException), new() {Reason = "Could not properly handle '.' and/or '/' characters in URL."}},
            {nameof(HttpRequestException), new() {Reason = "Request failed due to network connectivity, DNS failure, server certificate validation, or timeout."}},
            {nameof(JsonException), new() {Reason = "The JSON is invalid or T is not compatible with the JSON."}},
            {nameof(Exception), new() {Reason = "Something went bad in BaseBrokerObject.GetAll method."}},
            {nameof(TaskCanceledException), new() {Reason = "Request failed due to timeout."}}
        };
    }

    protected async Task<Results<T>> GetAllRequest<T>(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = _clientFactory.CreateClient("broker");
            var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResults<T> {DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error> {GetError(response.StatusCode)}}};

            var data = rawResponse.ToObject<List<T>>();

            return new SuccessfulResults<T> {Data = data.GetDataOrEmpty(), DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResults<T> {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResults<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResults<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResults<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResults<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    protected async Task<Result<T>> GetRequest<T>(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = _clientFactory.CreateClient("broker");
            var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResult<T> {DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error> { GetError(response.StatusCode) }}};

            var data = rawResponse.ToObject<T>();
                
            return new SuccessfulResult<T> {Data = data, DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    protected async Task<Result> GetRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = _clientFactory.CreateClient("broker");
            var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResult{DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error> { GetError(response.StatusCode) }}};
                
            return new SuccessfulResult{DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    protected async Task<Result> DeleteRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = _clientFactory.CreateClient("broker");
            var response = await client.DeleteAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error> { GetError(response.StatusCode) }}};

            return new SuccessfulResult {DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    protected async Task<Result> PutRequest<TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            string requestContent = request.ToJsonString(Deserializer.Options);
            var content = GetRequestContent(requestContent);
            using var client = _clientFactory.CreateClient("broker");
            var response = await client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResult {DebugInfo = new() {URL = url, Request = requestContent, Response = rawResponse, Errors = new List<Error> { GetError(response.StatusCode) }}};

            return new SuccessfulResult {DebugInfo = new() {URL = url, Request = requestContent, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    protected async Task<Result> PutRequest(string url, string request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            var content = GetRequestContent(request);
            using var client = _clientFactory.CreateClient("broker");
            var response = await client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResult {DebugInfo = new() {URL = url, Request = request, Response = rawResponse, Errors = new List<Error> { GetError(response.StatusCode) }}};

            return new SuccessfulResult {DebugInfo = new() {URL = url, Request = request, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    protected async Task<Result<T>> PostRequest<T, TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            string requestContent = request.ToJsonString(Deserializer.Options);
            var content = GetRequestContent(requestContent);
            using var client = _clientFactory.CreateClient("broker");
            var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResult<T> {DebugInfo = new() {URL = url, Request = requestContent, Response = rawResponse, Errors = new List<Error> { GetError(response.StatusCode) }}};

            var data = rawResponse.ToObject<T>();

            return new SuccessfulResult<T> {Data = data.GetDataOrDefault(), DebugInfo = new() {URL = url, Request = requestContent, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResult<T> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    protected async Task<Result> PostRequest<TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            string requestContent = request.ToJsonString(Deserializer.Options);
            var content = GetRequestContent(requestContent);
            using var client = _clientFactory.CreateClient("broker");
            var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResult {DebugInfo = new() {URL = url, Request = requestContent, Response = rawResponse, Errors = new List<Error> { GetError(response.StatusCode) }}};

            return new SuccessfulResult {DebugInfo = new() {URL = url, Request = requestContent, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    protected async Task<Result<IReadOnlyList<T>>> PostListRequest<T, TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            string requestContent = request.ToJsonString(Deserializer.Options);
            var content = GetRequestContent(requestContent);
            using var client = _clientFactory.CreateClient("broker");
            var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResult<IReadOnlyList<T>> {DebugInfo = new() {URL = url, Request = requestContent, Response = rawResponse, Errors = new List<Error> {GetError(response.StatusCode)}}};

            var data = rawResponse.ToObject<List<T>>();

            return new SuccessfulResult<IReadOnlyList<T>> {Data = data.GetDataOrEmpty(), DebugInfo = new() {URL = url, Request = requestContent, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResult<IReadOnlyList<T>> {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResult<IReadOnlyList<T>> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResult<IReadOnlyList<T>> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResult<IReadOnlyList<T>> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResult<IReadOnlyList<T>> {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    protected async Task<Result> PostEmptyRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = _clientFactory.CreateClient("broker");
            var response = await client.PostAsync(url, null, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error> { GetError(response.StatusCode) }}};

            return new SuccessfulResult {DebugInfo = new() {URL = url, Response = rawResponse, Errors = new List<Error>()}};
        }
        catch (MissingMethodException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(MissingMethodException)]}}};
        }
        catch (HttpRequestException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(HttpRequestException)]}}};
        }
        catch (JsonException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(JsonException)]}}};
        }
        catch (TaskCanceledException e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(TaskCanceledException)]}}};
        }
        catch (Exception e)
        {
            return new FaultedResult {DebugInfo = new() {URL = url, Response = rawResponse, Exception = e.Message, StackTrace = e.StackTrace, Errors = new List<Error> {_errors[nameof(Exception)]}}};
        }
    }

    HttpContent GetRequestContent(string request)
    {
        byte[] requestBytes = Encoding.UTF8.GetBytes(request);
        var content = new ByteArrayContent(requestBytes);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return content;
    }

    void HandleDotsAndSlashes()
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

    Error GetError(HttpStatusCode statusCode)
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