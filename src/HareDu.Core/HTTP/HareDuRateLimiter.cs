namespace HareDu.Core.HTTP;

using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.RateLimiting;
using System.Threading.Tasks;
using Configuration;

public class HareDuRateLimiter :
    DelegatingHandler,
    IAsyncDisposable
{
    RateLimiter _limiter;
    
    public HareDuRateLimiter(HareDuConfig config)
    {
        InnerHandler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(config.Broker.Credentials.Username, config.Broker.Credentials.Password)
        };

        var options = new TokenBucketRateLimiterOptions
        {
            TokenLimit = config.Broker.MaxAllowedParallelRequests,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
            QueueLimit = 5,
            ReplenishmentPeriod = TimeSpan.FromMilliseconds(config.Broker.RequestReplenishmentPeriod),
            TokensPerPeriod = config.Broker.RequestsPerReplenishment,
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

    protected virtual void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
            _limiter.Dispose();
    }
}