namespace HareDu.Model;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Represents the client properties associated with a connection in the system.
/// </summary>
public record ConnectionClientProperties
{
    /// <summary>
    /// Gets the name of the assembly associated with the connection client.
    /// </summary>
    [JsonPropertyName("assembly")]
    public string Assembly { get; init; }

    /// <summary>
    /// Gets the version of the assembly associated with the connection client.
    /// </summary>
    [JsonPropertyName("assembly_version")]
    public string AssemblyVersion { get; init; }

    /// <summary>
    /// Gets the capabilities available for the connection client.
    /// </summary>
    [JsonPropertyName("capabilities")]
    public ConnectionCapabilities Capabilities { get; init; }

    /// <summary>
    /// Gets the API version of the client used in the connection.
    /// </summary>
    [JsonPropertyName("client_api")]
    public string ClientApi { get; init; }

    /// <summary>
    /// Gets the date and time when the client connected.
    /// </summary>
    [JsonPropertyName("connected")]
    public DateTimeOffset Connected { get; init; }

    /// <summary>
    /// Gets the name of the connection associated with the client.
    /// </summary>
    [JsonPropertyName("connection_name")]
    public string ConnectionName { get; init; }

    /// <summary>
    /// Gets the copyright information associated with the connection client.
    /// </summary>
    [JsonPropertyName("copyright")]
    public string Copyright { get; init; }

    /// <summary>
    /// Gets the hostname associated with the connection client.
    /// </summary>
    [JsonPropertyName("hostname")]
    public string Host { get; init; }

    /// <summary>
    /// Gets the client-provided information associated with the connection.
    /// </summary>
    [JsonPropertyName("information")]
    public string Information { get; init; }

    /// <summary>
    /// Gets the platform information associated with the connection client.
    /// </summary>
    [JsonPropertyName("platform")]
    public string Platform { get; init; }

    /// <summary>
    /// Gets the identifier of the process associated with the connection client.
    /// </summary>
    [JsonPropertyName("process_id")]
    public string ProcessId { get; init;}

    /// <summary>
    /// Gets the name of the process associated with the connection client.
    /// </summary>
    [JsonPropertyName("process_name")]
    public string ProcessName { get; init; }

    /// <summary>
    /// Gets the name of the product associated with the connection client.
    /// </summary>
    [JsonPropertyName("product")]
    public string Product { get; init; }

    /// <summary>
    /// Gets the version of the connection client.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; init; }
}