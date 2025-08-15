namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record ExchangeInfo
{
    /// <summary>
    /// Gets the name of the exchange.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// Gets the name of the virtual host associated with the exchange.
    /// </summary>
    [JsonPropertyName("vhost")]
    public string VirtualHost { get; init; }

    /// <summary>
    /// Gets the type of routing used by the exchange.
    /// </summary>
    [JsonPropertyName("type")]
    public RoutingType RoutingType { get; init; }

    /// <summary>
    /// Indicates whether the exchange survives broker restarts.
    /// </summary>
    [JsonPropertyName("durable")]
    public bool Durable { get; init; }

    /// <summary>
    /// Indicates whether the exchange is automatically deleted when it is no longer in use.
    /// </summary>
    [JsonPropertyName("auto_delete")]
    public bool AutoDelete { get; init; }

    /// <summary>
    /// Indicates whether the exchange is internal. Internal exchanges can only be used by other exchanges
    /// and cannot be published directly by clients.
    /// </summary>
    [JsonPropertyName("internal")]
    public bool Internal { get; init; }

    /// <summary>
    /// Gets the custom arguments associated with the exchange.
    /// These arguments may define additional behaviors or parameters for the exchange.
    /// </summary>
    [JsonPropertyName("arguments")]
    public IDictionary<string, object> Arguments { get; init; }
}