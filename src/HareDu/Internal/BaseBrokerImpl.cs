namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Core.HTTP;
using Extensions;
using Serialization;

internal class BaseBrokerImpl :
    BaseHareDu
{
    protected BaseBrokerImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    protected async Task<Results<T>> GetAllRequest<T>(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Results<T>(url, [GetError(response.StatusCode)], response: rawResponse)
                : Successful.Results(url, rawResponse.ToObject<List<T>>().GetDataOrDefault(), response: rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result<T>> GetRequest<T>(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Result<T>(url, [GetError(response.StatusCode)], rawResponse)
                : Successful.Result(url, rawResponse.ToObject<T>(), null, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> GetRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Result(url, [GetError(response.StatusCode)], request: rawResponse)
                : Successful.Result(url, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> DeleteRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.DeleteAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Result(url, [GetError(response.StatusCode)], response: rawResponse)
                : Successful.Result(url, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
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
            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Result(url, [GetError(response.StatusCode)], requestContent, rawResponse)
                : Successful.Result(url, rawResponse, requestContent);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
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
            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Result(url, [GetError(response.StatusCode)], request, rawResponse)
                : Successful.Result(url, rawResponse, request);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
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
            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Result<T>(url, [GetError(response.StatusCode)], requestContent, rawResponse)
                : Successful.Result(url, rawResponse.ToObject<T>().GetDataOrDefault(), requestContent, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Result<T>(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
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
            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Result(url, [GetError(response.StatusCode)], requestContent, rawResponse)
                : Successful.Result(url, rawResponse, requestContent);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Results<T>> PostListRequest<T, TRequest>(string url, TRequest request, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            string requestContent = request.ToJsonString(Deserializer.Options);
            var content = GetRequestContent(requestContent);
            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Results<T>(url, [GetError(response.StatusCode)], requestContent, rawResponse)
                : Successful.Results(url, rawResponse.ToObject<List<T>>().GetDataOrEmpty(), requestContent, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Results<T>(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> PostEmptyRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient(HttpConst.BrokerClient.Value);
            var response = await client.PostAsync(url, null, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? Panic.Result(url, [GetError(response.StatusCode)], response: rawResponse)
                : Successful.Result(url, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return Faulted.Result(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
        }
    }
}