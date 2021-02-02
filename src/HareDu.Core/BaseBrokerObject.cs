namespace HareDu.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Extensions;
    using Serialization;

    public class BaseBrokerObject
    {
        readonly HttpClient _client;

        protected BaseBrokerObject(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        protected async Task<ResultList<T>> GetAll<T>(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                if (url.Contains("/%2f"))
                    HandleDotsAndSlashes();

                var response = await _client.GetAsync(url, cancellationToken).ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                    return new FaultedResultList<T>{Errors = new List<Error> { GetError(response.StatusCode) }, DebugInfo = new (){URL = url, Request = null}};
                
                var data = await response.ToObject<List<T>>(Deserializer.Options);
                
                return new SuccessfulResultList<T>{Data = data.GetDataOrEmpty(), DebugInfo = new (){URL = url, Request = null}};
            }
            catch (MissingMethodException)
            {
                return new FaultedResultList<T>{Errors = new List<Error>{ new (){Reason = "Could not properly handle '.' and/or '/' characters in URL."}}};
            }
        }

        protected async Task<Result<T>> Get<T>(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                if (url.Contains("/%2f"))
                    HandleDotsAndSlashes();

                var response = await _client.GetAsync(url, cancellationToken).ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                    return new FaultedResult<T>{Errors = new List<Error> { GetError(response.StatusCode) }, DebugInfo = new (){URL = url, Request = null}};

                var data = await response.ToObject<T>(Deserializer.Options);
                
                return new SuccessfulResult<T>{Data = data, DebugInfo = new (){URL = url, Request = null}};
            }
            catch (MissingMethodException)
            {
                return new FaultedResult<T>{Errors = new List<Error>{ new (){Reason = "Could not properly handle '.' and/or '/' characters in URL."}}};
            }
        }

        protected async Task<Result> Delete(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                if (url.Contains("/%2f"))
                    HandleDotsAndSlashes();

                var response = await _client.DeleteAsync(url, cancellationToken).ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                    return new FaultedResult{Errors = new List<Error> { GetError(response.StatusCode) }, DebugInfo = new (){URL = url, Request = null}};

                return new SuccessfulResult{DebugInfo = new (){URL = url, Request = null}};
            }
            catch (MissingMethodException)
            {
                return new FaultedResult{Errors = new List<Error>{ new () {Reason = "Could not properly handle '.' and/or '/' characters in URL."}}};
            }
        }

        protected async Task<Result> Put<TValue>(string url, TValue value, CancellationToken cancellationToken = default)
        {
            try
            {
                if (url.Contains("/%2f"))
                    HandleDotsAndSlashes();

                string request = value.ToJsonString();
                byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                var content = new ByteArrayContent(requestBytes);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await _client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                    return new FaultedResult{Errors = new List<Error> { GetError(response.StatusCode) }, DebugInfo = new () {URL = url, Request = request}};

                return new SuccessfulResult{DebugInfo = new () {URL = url, Request = request}};
            }
            catch (MissingMethodException)
            {
                return new FaultedResult{Errors = new List<Error>{ new (){Reason = "Could not properly handle '.' and/or '/' characters in URL."}}};
            }
        }

        protected async Task<Result> Put(string url, string request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (url.Contains("/%2f"))
                    HandleDotsAndSlashes();

                byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                var content = new ByteArrayContent(requestBytes);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await _client.PutAsync(url, content, cancellationToken).ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                    return new FaultedResult{Errors = new List<Error> { GetError(response.StatusCode) }, DebugInfo = new (){URL = url, Request = request}};

                return new SuccessfulResult{DebugInfo = new (){URL = url, Request = request}};
            }
            catch (MissingMethodException)
            {
                return new FaultedResult{Errors = new List<Error>{ new (){Reason = "Could not properly handle '.' and/or '/' characters in URL."}}};
            }
        }

        protected async Task<Result<T>> Post<T, TValue>(string url, TValue value, CancellationToken cancellationToken = default)
        {
            try
            {
                if (url.Contains("/%2f"))
                    HandleDotsAndSlashes();

                string request = value.ToJsonString();
                byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                var content = new ByteArrayContent(requestBytes);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await _client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                    return new FaultedResult<T>{Errors = new List<Error> { GetError(response.StatusCode) }, DebugInfo = new (){URL = url, Request = request}};

                var data = await response.ToObject<T>(Deserializer.Options);

                return new SuccessfulResult<T>{Data = data.GetDataOrDefault(), DebugInfo = new(){URL = url, Request = request}};
            }
            catch (MissingMethodException)
            {
                return new FaultedResult<T>{Errors = new List<Error>{ new (){Reason = "Could not properly handle '.' and/or '/' characters in URL."}}};
            }
        }

        protected async Task<ResultList<T>> PostList<T, TValue>(string url, TValue value, CancellationToken cancellationToken = default)
        {
            try
            {
                if (url.Contains("/%2f"))
                    HandleDotsAndSlashes();

                string request = value.ToJsonString();
                byte[] requestBytes = Encoding.UTF8.GetBytes(request);
                var content = new ByteArrayContent(requestBytes);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await _client.PostAsync(url, content, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                    return new FaultedResultList<T> {Errors = new List<Error> {GetError(response.StatusCode)}, DebugInfo = new () {URL = url, Request = request}};

                var data = await response.ToObject<List<T>>(Deserializer.Options);

                return new SuccessfulResultList<T> {Data = data.GetDataOrEmpty(), DebugInfo = new () {URL = url, Request = request}};
            }
            catch (MissingMethodException)
            {
                return new FaultedResultList<T> {Errors = new List<Error> {new() {Reason = "Could not properly handle '.' and/or '/' characters in URL."}}};
            }
        }

        protected async Task<Result> PostEmpty(string url, CancellationToken cancellationToken = default)
        {
            try
            {
                if (url.Contains("/%2f"))
                    HandleDotsAndSlashes();

                var response = await _client.PostAsync(url, null, cancellationToken).ConfigureAwait(false);
                
                if (!response.IsSuccessStatusCode)
                    return new FaultedResult{Errors = new List<Error> { GetError(response.StatusCode) }, DebugInfo = new () {URL = url, Request = null}};

                return new SuccessfulResult {DebugInfo = new() {URL = url, Request = null}};
            }
            catch (MissingMethodException)
            {
                return new FaultedResult{Errors = new List<Error>{ new (){Reason = "Could not properly handle '.' and/or '/' characters in URL."}}};
            }
        }

        void HandleDotsAndSlashes()
        {
            var getSyntaxMethod = typeof(UriParser).GetMethod("GetSyntax", BindingFlags.Static | BindingFlags.NonPublic);
            if (getSyntaxMethod == null)
                throw new MissingMethodException("UriParser", "GetSyntax");

            var uriParser = getSyntaxMethod.Invoke(null, new object[] {"http"});

            var setUpdatableFlagsMethod = uriParser
                .GetType()
                .GetMethod("SetUpdatableFlags", BindingFlags.Instance | BindingFlags.NonPublic);
            
            if (setUpdatableFlagsMethod == null)
                throw new MissingMethodException("UriParser", "SetUpdatableFlags");

            setUpdatableFlagsMethod.Invoke(uriParser, new object[] {0});
        }

        Error GetError(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    return new () {Reason = "RabbitMQ server did not recognize the request due to malformed syntax."};
                
                case HttpStatusCode.Forbidden:
                    return new () {Reason = "RabbitMQ server rejected the request."};
                
                case HttpStatusCode.InternalServerError:
                    return new () {Reason = "Internal error happened on RabbitMQ server."};
                
                case HttpStatusCode.RequestTimeout:
                    return new (){Reason = "No response from the RabbitMQ server within the specified window of time."};
                
                case HttpStatusCode.ServiceUnavailable:
                    return new (){Reason = "RabbitMQ server temporarily not able to handle request"};
                
                case HttpStatusCode.Unauthorized:
                    return new (){Reason = "Unauthorized access to RabbitMQ server resource."};
                
                default:
                    return null;
            }
        }
    }
}