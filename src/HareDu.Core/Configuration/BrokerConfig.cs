namespace HareDu.Core.Configuration;

using System;

/// <summary>
/// Represents the configuration settings for a broker connection.
/// </summary>
public record BrokerConfig
{
    /// <summary>
    /// Gets the URL of the RabbitMQ broker instance that is used for establishing a connection.
    /// </summary>
    /// <remarks>
    /// This property defines the base address for communication with the RabbitMQ broker.
    /// It specifies the protocol, hostname, port, and optional path used in API requests.
    /// </remarks>
    public string Url { get; init; }

    /// <summary>
    /// Gets the timeout duration used for configuring the maximum interval to wait for a broker operation to complete.
    /// </summary>
    /// <remarks>
    /// This property is used to define the time span after which a broker request or operation will time out if not completed.
    /// A value of TimeSpan.Zero typically indicates no timeout constraint.
    /// </remarks>
    public TimeSpan Timeout { get; init; }

    /// <summary>
    /// Gets the configuration settings that define the behavior of API request handling.
    /// </summary>
    /// <remarks>
    /// This property specifies various operational parameters related to API request behaviors,
    /// such as maximum concurrent requests, replenishment periods, and other request management controls.
    /// </remarks>
    public HareDuBehaviorConfig Behavior { get; init; }
}