namespace HareDu.Core.Configuration;

using System;

/// <summary>
/// Provides methods to configure RabbitMQ broker connection settings, including server URL, credentials, timeout, and request limitations.
/// </summary>
public interface BrokerConfigurator
{
    /// <summary>
    /// Specifies the RabbitMQ server URL to connect to.
    /// </summary>
    /// <param name="url">The fully qualified URL of the RabbitMQ server.</param>
    void ConnectTo(string url);

    /// <summary>
    /// Specify the maximum time allowed before the HTTP request to the RabbitMQ server will timeout.
    /// </summary>
    /// <param name="timeout">The duration to wait before the request times out.</param>
    void TimeoutAfter(TimeSpan timeout);

    /// <summary>
    /// Configures specific behavior characteristics when interacting with RabbitMQ, such as request handling and limitations.
    /// </summary>
    /// <param name="configurator">The configurator instance for defining behavior-related settings.</param>
    void WithBehavior(Action<BehaviorConfigurator> configurator);
}