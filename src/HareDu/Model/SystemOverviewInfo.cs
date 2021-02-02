namespace HareDu.Model
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public record SystemOverviewInfo
    {
        [JsonPropertyName("management_version")]
        public string ManagementVersion { get; init; }

        [JsonPropertyName("rates_mode")]
        public string RatesMode { get; init; }

        [JsonPropertyName("exchange_types")]
        public IList<ExchangeType> ExchangeTypes { get; init; }

        [JsonPropertyName("rabbitmq_version")]
        public string RabbitMqVersion { get; init; }

        [JsonPropertyName("cluster_name")]
        public string ClusterName { get; init; }

        [JsonPropertyName("erlang_version")]
        public string ErlangVersion { get; init; }

        [JsonPropertyName("erlang_full_version")]
        public string ErlangFullVersion { get; init; }

        [JsonPropertyName("message_stats")]
        public MessageStats MessageStats { get; init; }
        
        [JsonPropertyName("churn_rates")]
        public ChurnRates ChurnRates { get; init; }

        [JsonPropertyName("queue_totals")]
        public QueueStats QueueStats { get; init; }

        [JsonPropertyName("object_totals")]
        public ClusterObjectTotals ObjectTotals { get; init; }

        [JsonPropertyName("statistics_db_event_queue")]
        public long StatsDatabaseEventQueue { get; init; }

        [JsonPropertyName("node")]
        public string Node { get; init; }

        [JsonPropertyName("listeners")]
        public IList<Listener> Listeners { get; init; }

        [JsonPropertyName("contexts")]
        public IList<NodeContext> Contexts { get; init; }
    }
}