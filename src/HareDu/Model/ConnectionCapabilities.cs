namespace HareDu.Model;

using System.Text.Json.Serialization;

/// <summary>
/// Represents the set of connection capabilities that are supported or enabled
/// for a specific connection in the system.
/// </summary>
public record ConnectionCapabilities
{
    /// <summary>
    /// Indicates whether the notification for authentication failure is enabled.
    /// When set to true, the system is configured to notify and possibly close the connection
    /// upon an authentication failure.
    /// </summary>
    [JsonPropertyName("authentication_failure_close")]
    public bool AuthenticationFailureNotificationEnabled { get; init; }

    /// <summary>
    /// Determines whether the notification for negative acknowledgments is enabled.
    /// When enabled, the system is set to notify about message delivery that was negatively acknowledged.
    /// </summary>
    [JsonPropertyName("basic.nack")]
    public bool NegativeAcknowledgmentNotificationsEnabled { get; init; }

    /// <summary>
    /// Specifies whether notifications for blocked connections are enabled.
    /// When set to true, the system will send notifications if a connection becomes blocked.
    /// </summary>
    [JsonPropertyName("connection.blocked")]
    public bool ConnectionBlockedNotificationsEnabled { get; init; }

    /// <summary>
    /// Specifies whether consumer cancellation notifications are enabled.
    /// When set to true, notifications are sent to consumers when their subscription is canceled by the server.
    /// </summary>
    [JsonPropertyName("consumer_cancel_notify")]
    public bool ConsumerCancellationNotificationsEnabled { get; init; }

    /// <summary>
    /// Specifies whether exchange-to-exchange bindings are enabled.
    /// When set to true, it allows binding one exchange to another, facilitating direct forwarding of messages from one exchange to another.
    /// </summary>
    [JsonPropertyName("exchange_exchange_bindings")]
    public bool ExchangeBindingEnabled { get; init; }

    /// <summary>
    /// Indicates whether publisher confirms are enabled.
    /// When set to true, the system is configured to support publisher acknowledgments for message delivery confirmation.
    /// </summary>
    [JsonPropertyName("publisher_confirms")]
    public bool PublisherConfirmsEnabled { get; init; }
}