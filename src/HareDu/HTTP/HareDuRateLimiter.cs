namespace HareDu.HTTP;

using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Core.Configuration;
using Microsoft.Extensions.Http.Resilience;

/// <summary>
/// Represents a rate-limiting delegating handler used to control the rate of outgoing HTTP requests from the client.
/// This implementation utilizes the Token Bucket algorithm to enforce rate limits based on configuration parameters.
/// </summary>
public sealed class HareDuRateLimiter :
    DelegatingHandler,
    IAsyncDisposable
{
    readonly RateLimiter _limiter;

    public HareDuRateLimiter(HareDuConfig config, ResilienceHandler handler)
    {
        InnerHandler = handler;

        var options = new TokenBucketRateLimiterOptions
        {
            TokenLimit = config.Broker.Behavior.MaxConcurrentRequests,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = (int) Math.Ceiling(config.Broker.Behavior.MaxConcurrentRequests * 0.2),
            ReplenishmentPeriod = TimeSpan.FromMilliseconds(config.Broker.Behavior.RequestReplenishmentInterval),
            TokensPerPeriod = config.Broker.Behavior.RequestsPerReplenishment,
            AutoReplenishment = true
        };

        _limiter = new TokenBucketRateLimiter(options);
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        using RateLimitLease lease = await _limiter.AcquireAsync(permitCount: 1, cancellationToken);

        if (lease.IsAcquired)
            return await base.SendAsync(request, cancellationToken);

        var response = new HttpResponseMessage(HttpStatusCode.TooManyRequests);

        if (lease.TryGetMetadata(MetadataName.RetryAfter, out TimeSpan retryAfter))
            response.Headers.Add("Retry-After", ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo));

        return response;
    }

    public async ValueTask DisposeAsync()
    {
        await _limiter.DisposeAsync().ConfigureAwait(false);
    
        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
            _limiter.Dispose();
    }
}