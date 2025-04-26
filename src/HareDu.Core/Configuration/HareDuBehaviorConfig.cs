namespace HareDu.Core.Configuration;

/// <summary>
/// Represents the configuration for API behavior settings in the HareDu library.
/// </summary>
public record HareDuBehaviorConfig
{
    /// <summary>
    /// Gets the maximum number of allowed parallel requests to the RabbitMQ broker.
    /// </summary>
    /// <remarks>
    /// This property defines the upper limit on the number of concurrent requests that can be executed
    /// against the RabbitMQ broker. It is used in configuring rate limiting to prevent exceeding
    /// the capacity or causing strain on the broker's resources.
    /// </remarks>
    public int MaxConcurrentRequests { get; init; }

    /// <summary>
    /// Defines the interval, in milliseconds, for replenishing tokens used in rate limiting operations.
    /// </summary>
    /// <remarks>
    /// This property determines the time duration after which additional tokens are added to the token bucket
    /// when using the token bucket rate-limiting algorithm.
    /// It directly influences the rate at which requests are allowed.
    /// </remarks>
    public int RequestReplenishmentInterval { get; init; }

    /// <summary>
    /// Gets the number of requests replenished at each replenishment interval for the rate limiter.
    /// </summary>
    /// <remarks>
    /// This property specifies how many requests are added to the token bucket during each replenishment period.
    /// It directly controls the rate at which requests are allowed when the limiter is in use.
    /// </remarks>
    public int RequestsPerReplenishment { get; init; }
}