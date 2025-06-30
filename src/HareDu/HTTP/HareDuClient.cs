namespace HareDu.HTTP;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Core;
using Core.Configuration;
using Core.Extensions;
using Core.Security;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Retry;

/// <summary>
/// Represents a client for interacting with the HareDu API.
/// </summary>
public class HareDuClient(HareDuConfig config, IHareDuCredentialBuilder builder) :
    IHareDuClient,
    IDisposable
{
    readonly IDictionary<string, HttpClient> _cache = new Dictionary<string, HttpClient>();

    public HttpClient GetClient(Action<HareDuCredentialProvider> provider)
    {
        Config.IfInvalid(config.Broker);

        var credentials = builder.Build(provider);

        string key = $"{credentials.Username}:{credentials.Password}".GetIdentifier();

        if (_cache.TryGetValue(key, out var clientFromCache))
            return clientFromCache;

        var handler = BuildResilienceHandler(credentials);

        var client = new HttpClient(new HareDuRateLimiter(config, handler));

        client.BaseAddress = new Uri($"{config.Broker.Url}/");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", "HareDu");

        if (config.Broker.Timeout != TimeSpan.Zero)
            client.Timeout = config.Broker.Timeout;

        _cache.Add(key, client);

        return client;
    }

    public void CancelPendingRequests()
    {
        if (_cache is null || _cache.Count <= 0)
            return;

        foreach (var client in _cache.Values)
            client.CancelPendingRequests();
    }

    public void Dispose()
    {
        if (_cache is null || _cache.Count <= 0)
            return;

        foreach (var client in _cache.Values)
            client.Dispose();
    }

    ResilienceHandler BuildResilienceHandler(HareDuCredentials credentials)
    {
        var retry = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>
            {
                BackoffType = DelayBackoffType.Constant,
                MaxRetryAttempts = 3,
                MaxDelay = TimeSpan.FromMilliseconds(50)
            })
            .Build();

        return new ResilienceHandler(retry)
        {
            InnerHandler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(1),
                Credentials = new NetworkCredential(credentials.Username, credentials.Password)
            }
        };
    }
}