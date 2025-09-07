namespace HareDu.Core;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Extensions;
using Serialization;

public class BaseHareDuImpl
{
    protected readonly HttpClient Client;
    protected readonly IDictionary<string, string> ErrorReasons;
    protected readonly IHareDuDeserializer Deserializer;

    public BaseHareDuImpl(HttpClient client, IHareDuDeserializer deserializer)
        // : base(client)
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
        Deserializer = deserializer;
    }

    protected HttpContent GetRequestContent(string request)
    {
        byte[] payloadBytes = Encoding.UTF8.GetBytes(request);

        var content = new ByteArrayContent(payloadBytes);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        content.Headers.ContentLength = payloadBytes.Length;

        return content;
    }

    protected async Task<Results<T>> GetAllRequest<T>(string url, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Responses.Failed<T>(Debug.Info(url, Errors.Create(e => {e.Add(response.StatusCode, type);}), response: rawResponse))
                : Responses.Succeeded(Deserializer.ToObject<List<T>>(rawResponse).GetDataOrDefault(), Debug.Info(url, [], response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(MissingMethodException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message:e.Message, stackTrace: e.StackTrace, response:rawResponse));
        }
        catch (JsonException e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => { err.Add(ErrorReasons[nameof(TaskCanceledException)]); }), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }

    protected async Task<Result<T>> GetRequest<T>(string url, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed<T>(Debug.Info(url, Errors.Create(err => {err.Add(response.StatusCode, type);}), response: rawResponse))
                : Response.Succeeded(Deserializer.ToObject<T>(rawResponse), Debug.Info(url, [], response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(MissingMethodException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (JsonException e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message:e.Message, stackTrace:e.StackTrace, response:rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(TaskCanceledException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }

    protected async Task<Result> GetRequest(string url, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(Debug.Info(url, Errors.Create(err => {err.Add(response.StatusCode, type);}), response: rawResponse))
                : Response.Succeeded(Debug.Info(url, [], response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(MissingMethodException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message:e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (JsonException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(TaskCanceledException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }

    protected async Task<Result> DeleteRequest(string url, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.DeleteAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(Debug.Info(url, Errors.Create(err => {err.Add(response.StatusCode, type);}), response: rawResponse))
                : Response.Succeeded(Debug.Info(url, [], response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(MissingMethodException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (JsonException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(TaskCanceledException)]);}), message: e.Message, stackTrace:e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }

    protected async Task<Result> PutRequest<TRequest>(string url, TRequest request, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            string requestContent = Deserializer.ToJsonString(request);
            var content = GetRequestContent(requestContent);
            var response = await Client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(Debug.Info(url, Errors.Create(err => {err.Add(response.StatusCode, type);}), requestContent, rawResponse))
                : Response.Succeeded(Debug.Info(url, [], request: requestContent, response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(MissingMethodException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (JsonException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(TaskCanceledException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }

    protected async Task<Result> PutRequest(string url, string request, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var content = GetRequestContent(request);
            var response = await Client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(Debug.Info(url, Errors.Create(err => {err.Add(response.StatusCode, type);}), request: request, response: rawResponse))
                : Response.Succeeded(Debug.Info(url, [], request: request, response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(MissingMethodException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (JsonException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(TaskCanceledException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }

    protected async Task<Result<T>> PostRequest<T, TRequest>(string url, TRequest request, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            string requestContent = Deserializer.ToJsonString(request);
            var content = GetRequestContent(requestContent);
            var response = await Client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed<T>(Debug.Info(url, Errors.Create(err => {err.Add(response.StatusCode, type);}), request: requestContent, response: rawResponse))
                : Response.Succeeded(Deserializer.ToObject<T>(rawResponse).GetDataOrDefault(), Debug.Info(url, [], request: requestContent, response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(MissingMethodException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (JsonException e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(TaskCanceledException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Response.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }

    protected async Task<Result> PostRequest<TRequest>(string url, TRequest request, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            string requestContent = Deserializer.ToJsonString(request);
            var content = GetRequestContent(requestContent);
            var response = await Client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(Debug.Info(url, Errors.Create(err => {err.Add(response.StatusCode, type);}), request: requestContent, response: rawResponse))
                : Response.Succeeded(Debug.Info(url, [], request: requestContent, response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(MissingMethodException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (JsonException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(TaskCanceledException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }

    protected async Task<Results<T>> PostListRequest<T, TRequest>(string url, TRequest request, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            string requestContent = Deserializer.ToJsonString(request);
            var content = GetRequestContent(requestContent);
            var response = await Client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Responses.Failed<T>(Debug.Info(url, Errors.Create(err => {err.Add(response.StatusCode, type);}), request: requestContent, response: rawResponse))
                : Responses.Succeeded(Deserializer.ToObject<List<T>>(rawResponse).GetDataOrDefault(), Debug.Info(url, [], response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (JsonException e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message:e.Message, stackTrace:e.StackTrace, response: rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(TaskCanceledException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Responses.Faulted<T>(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }

    protected async Task<Result> PostEmptyRequest(string url, RequestType type, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.PostAsync(url, null, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(Debug.Info(url, Errors.Create(e => {e.Add(response.StatusCode, type);}), response: rawResponse))
                : Response.Succeeded(Debug.Info(url, [], response: rawResponse));
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(MissingMethodException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(HttpRequestException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (JsonException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(JsonException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(TaskCanceledException)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
        catch (Exception e)
        {
            return Response.Faulted(Debug.Info(url,
                Errors.Create(err => {err.Add(ErrorReasons[nameof(Exception)]);}), message: e.Message, stackTrace: e.StackTrace, response: rawResponse));
        }
    }
}