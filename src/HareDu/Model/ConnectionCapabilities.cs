namespace HareDu.Model
{
    using System.Text.Json.Serialization;

    public record ConnectionCapabilities
    {
        [JsonPropertyName("authentication_failure_close")]
        public bool AuthenticationFailureNotificationEnabled { get; init; }

        [JsonPropertyName("basic.nack")]
        public bool NegativeAcknowledgmentNotificationsEnabled { get; init; }

        [JsonPropertyName("connection.blocked")]
        public bool ConnectionBlockedNotificationsEnabled { get; init; }

        [JsonPropertyName("consumer_cancel_notify")]
        public bool ConsumerCancellationNotificationsEnabled { get; init; }

        [JsonPropertyName("exchange_exchange_bindings")]
        public bool ExchangeBindingEnabled { get; init; }

        [JsonPropertyName("publisher_confirms")]
        public bool PublisherConfirmsEnabled { get; init; }
    }
}