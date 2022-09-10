namespace HareDu.Model;

using System;
using System.Text.Json.Serialization;

public record ConnectionClientProperties
{
    [JsonPropertyName("assembly")]
    public string Assembly { get; init; }

    [JsonPropertyName("assembly_version")]
    public string AssemblyVersion { get; init; }

    [JsonPropertyName("capabilities")]
    public ConnectionCapabilities Capabilities { get; init; }

    [JsonPropertyName("client_api")]
    public string ClientApi { get; init; }

    [JsonPropertyName("connected")]
    public DateTimeOffset Connected { get; init; }

    [JsonPropertyName("connection_name")]
    public string ConnectionName { get; init; }

    [JsonPropertyName("copyright")]
    public string Copyright { get; init; }

    [JsonPropertyName("hostname")]
    public string Host { get; init; }

    [JsonPropertyName("information")]
    public string Information { get; init; }

    [JsonPropertyName("platform")]
    public string Platform { get; init; }

    [JsonPropertyName("process_id")]
    public string ProcessId { get; init;}

    [JsonPropertyName("process_name")]
    public string ProcessName { get; init; }

    [JsonPropertyName("product")]
    public string Product { get; init; }

    [JsonPropertyName("version")]
    public string Version { get; init; }
}