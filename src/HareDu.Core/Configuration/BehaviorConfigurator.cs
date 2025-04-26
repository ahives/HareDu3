namespace HareDu.Core.Configuration;

/// <summary>
/// Provides an interface for configuring behavior settings for handling requests,
/// such as the number of concurrent requests allowed and the settings for request replenishment.
/// </summary>
public interface BehaviorConfigurator
{
    /// <summary>
    /// Configures the number of parallel requests allowed, along with replenishment settings for the request limiter.
    /// </summary>
    /// <param name="maxConcurrentRequests">The maximum number of parallel requests allowed at a given time.</param>
    /// <param name="requestsPerReplenishment">The number of requests added back to the allowable pool per replenishment period.</param>
    /// <param name="replenishInterval">The period, in seconds, after which the requests are replenished.</param>
    void LimitRequests(int maxConcurrentRequests = 100, int requestsPerReplenishment = 100, int replenishInterval = 1);
}