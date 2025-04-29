namespace HareDu.HTTP;

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Core.Configuration;
using Core.Extensions;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Retry;

/// <summary>
/// Represents a client for interacting with the HareDu API.
/// </summary>
public class HareDuClient :
    IHareDuClient
{
    private readonly HareDuConfig _config;
    IDictionary<string, HttpClient> _credentials;
    
    public HareDuClient(HareDuConfig config)
    {
        _config = config;
        _credentials = new Dictionary<string, HttpClient>();
    }

    public HttpClient CreateClient(HareDuCredentials credentials)
    {
        if (credentials is null)
            throw new HareDuBrokerApiInitException(nameof(credentials));

        string key = $"{credentials.Username}:{credentials.Password}".GetIdentifier();

        if (_credentials.TryGetValue(key, out var cachedClient))
            return cachedClient;

        var handler = BuildResilienceHandler(credentials);

        var client = new HttpClient(new HareDuRateLimiter(_config, handler));

        client.BaseAddress = new Uri($"{_config.Broker.Url}/");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", "HareDu");

        if (_config.Broker.Timeout != TimeSpan.Zero)
            client.Timeout = _config.Broker.Timeout;

        _credentials.Add(key, client);

        return client;
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