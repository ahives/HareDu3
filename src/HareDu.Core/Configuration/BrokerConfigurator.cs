namespace HareDu.Core.Configuration;

using System;

public interface BrokerConfigurator
{
    /// <summary>
    /// Specify the RabbitMQ server url to connect to.
    /// </summary>
    /// <param name="url"></param>
    void ConnectTo(string url);

    /// <summary>
    /// Specify the maximum time before the HTTP request to the RabbitMQ server will fail.
    /// </summary>
    /// <param name="timeout"></param>
    void TimeoutAfter(TimeSpan timeout);

    /// <summary>
    /// Configures the number of parallel requests allowed, along with replenishment settings for the request limiter.
    /// </summary>
    /// <param name="allowedParallelRequests">The maximum number of parallel requests allowed at a given time.</param>
    /// <param name="requestsPerReplenishment">The number of requests added back to the allowable pool per replenishment period.</param>
    /// <param name="replenishPeriod">The period, in seconds, after which the requests are replenished.</param>
    void LimitParallelRequests(int allowedParallelRequests = 100, int requestsPerReplenishment = 100, int replenishPeriod = 1);

    /// <summary>
    /// Specify the user credentials to connect to the RabbitMQ server.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    void UsingCredentials(string username, string password);
}