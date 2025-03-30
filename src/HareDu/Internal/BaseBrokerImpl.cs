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
    protected BaseBrokerImpl(IHttpClientFactory clientFactory) : base(clientFactory)
    {
    }

    protected async Task<Results<T>> GetAllRequest<T>(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient("broker");
            var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResults<T>(url, [GetError(response.StatusCode)], rawResponse)
                : GetSuccessfulResults(url, rawResponse.ToObject<List<T>>().GetDataOrDefault(), rawResponse);
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResults<T>(url, null, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResults<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)]);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResults<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(JsonException)]);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResults<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)]);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResults<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(Exception)]);
        }
    }

    protected async Task<Result<T>> GetRequest<T>(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient("broker");
            var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResult<T>(url, rawResponse, [GetError(response.StatusCode)])
                : GetSuccessfulResult(url, null, rawResponse, rawResponse.ToObject<T>());
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResult<T>(url,null, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResult<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)]);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResult<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(JsonException)]);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResult<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)]);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResult<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(Exception)]);
        }
    }

    protected async Task<Result> GetRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient("broker");
            var response = await client.GetAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResult(url, [GetError(response.StatusCode)], rawResponse)
                : GetSuccessfulResult(url, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
        }
    }

    protected async Task<Result> DeleteRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient("broker");
            var response = await client.DeleteAsync(url, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResult(url, [GetError(response.StatusCode)], rawResponse)
                : GetSuccessfulResult(url, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
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
            using var client = ClientFactory.CreateClient("broker");
            var response = await client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResult(url, [GetError(response.StatusCode)], rawResponse, requestContent)
                : GetSuccessfulResult(url, rawResponse, requestContent);
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
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
            using var client = ClientFactory.CreateClient("broker");
            var response = await client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResult(url, [GetError(response.StatusCode)], rawResponse, request)
                : GetSuccessfulResult(url, rawResponse, request);
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
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
            using var client = ClientFactory.CreateClient("broker");
            var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResult<T>(url, requestContent, rawResponse, [GetError(response.StatusCode)])
                : GetSuccessfulResult(url, requestContent, rawResponse, rawResponse.ToObject<T>().GetDataOrDefault());
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResult<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)]);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResult<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)]);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResult<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(JsonException)]);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResult<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)]);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResult<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(Exception)]);
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
            using var client = ClientFactory.CreateClient("broker");
            var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResult(url, [GetError(response.StatusCode)], rawResponse, requestContent)
                : GetSuccessfulResult(url, rawResponse, requestContent);
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
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
            using var client = ClientFactory.CreateClient("broker");
            var response = await client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResults<T>(url, [GetError(response.StatusCode)], rawResponse, requestContent)
                : GetSuccessfulResults(url, rawResponse.ToObject<List<T>>().GetDataOrEmpty(), rawResponse, requestContent);
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResults<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)]);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResults<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)]);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResults<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(JsonException)]);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResults<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)]);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResults<T>(url, rawResponse, e.Message, e.StackTrace, Errors[nameof(Exception)]);
        }
    }

    protected async Task<Result> PostEmptyRequest(string url, CancellationToken cancellationToken = default)
    {
        string rawResponse = null;

        try
        {
            if (url.Contains("/%2f"))
                HandleDotsAndSlashes();

            using var client = ClientFactory.CreateClient("broker");
            var response = await client.PostAsync(url, null, cancellationToken).ConfigureAwait(false);

            rawResponse = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return !response.IsSuccessStatusCode
                ? GetFaultedResult(url, [GetError(response.StatusCode)], rawResponse)
                : GetSuccessfulResult(url, rawResponse);
        }
        catch (MissingMethodException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(MissingMethodException)], rawResponse);
        }
        catch (HttpRequestException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(HttpRequestException)], rawResponse);
        }
        catch (JsonException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(JsonException)], rawResponse);
        }
        catch (TaskCanceledException e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(TaskCanceledException)], rawResponse);
        }
        catch (Exception e)
        {
            return GetFaultedExceptionResult(url, e.Message, e.StackTrace, Errors[nameof(Exception)], rawResponse);
        }
    }
}