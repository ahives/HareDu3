namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Serialization;

internal class BaseBrokerImpl :
    BaseHareDu
{
    protected BaseBrokerImpl(HttpClient client)
        : base(client)
    {
    }

    protected async Task<Results<T>> GetAllRequest<T>(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Responses.Failed<T>(url, [GetError(response.StatusCode)], response: rawResponse)
                : Responses.Succeeded(url, rawResponse.ToObject<List<T>>().GetDataOrDefault(), response: rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result<T>> GetRequest<T>(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed<T>(url, [GetError(response.StatusCode)], rawResponse)
                : Response.Succeeded(url, rawResponse.ToObject<T>(), null, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> GetRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(url, [GetError(response.StatusCode)], request: rawResponse)
                : Response.Succeeded(url, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> DeleteRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.DeleteAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(url, [GetError(response.StatusCode)], response: rawResponse)
                : Response.Succeeded(url, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> PutRequest<TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            string requestContent = request.ToJsonString(Deserializer.Options);
            var content = GetRequestContent(requestContent);
            var response = await Client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(url, [GetError(response.StatusCode)], requestContent, rawResponse)
                : Response.Succeeded(url, rawResponse, requestContent);
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> PutRequest(string url, string request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var content = GetRequestContent(request);
            var response = await Client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(url, [GetError(response.StatusCode)], request, rawResponse)
                : Response.Succeeded(url, rawResponse, request);
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result<T>> PostRequest<T, TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            string requestContent = request.ToJsonString(Deserializer.Options);
            var content = GetRequestContent(requestContent);
            var response = await Client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed<T>(url, [GetError(response.StatusCode)], requestContent, rawResponse)
                : Response.Succeeded(url, rawResponse.ToObject<T>().GetDataOrDefault(), requestContent, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(MissingMethodException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Response.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> PostRequest<TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            string requestContent = request.ToJsonString(Deserializer.Options);
            var content = GetRequestContent(requestContent);
            var response = await Client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(url, [GetError(response.StatusCode)], requestContent, rawResponse)
                : Response.Succeeded(url, rawResponse, requestContent);
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(MissingMethodException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Results<T>> PostListRequest<T, TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            string requestContent = request.ToJsonString(Deserializer.Options);
            var content = GetRequestContent(requestContent);
            var response = await Client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Responses.Failed<T>(url, [GetError(response.StatusCode)], requestContent, rawResponse)
                : Responses.Succeeded(url, rawResponse.ToObject<List<T>>().GetDataOrEmpty(), requestContent, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Responses.Faulted<T>(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> PostEmptyRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            var response = await Client.PostAsync(url, null, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Response.Failed(url, [GetError(response.StatusCode)], response: rawResponse)
                : Response.Succeeded(url, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(MissingMethodException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Response.Faulted(url, e.Message, e.StackTrace, InternalErrors[nameof(Exception)], rawResponse);
        }
    }
}