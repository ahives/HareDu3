namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents detailed information about the RabbitMQ server including its version, users, virtual hosts, permissions, policies, parameters, queues, exchanges, bindings, and topic permissions.
/// </summary>
public record ServerInfo
{
    /// <summary>
    /// Gets the version of the RabbitMQ server.
    /// </summary>
    /// <remarks>
    /// Represents the `rabbit_version` field in the server information.
    /// </remarks>
    [JsonPropertyName("rabbit_version")]
    public string RabbitVersion { get; init; }

    /// <summary>
    /// Gets the version of the RabbitMQ software.
    /// </summary>
    /// <remarks>
    /// Corresponds to the `rabbitmq_version` field in the server information.
    /// </remarks>
    [JsonPropertyName("rabbitmq_version")]
    public string RabbitMqVersion { get; init; }

    /// <summary>
    /// Gets the name of the product to which the RabbitMQ server belongs.
    /// </summary>
    /// <remarks>
    /// Corresponds to the `product_name` field in the server information.
    /// </remarks>
    [JsonPropertyName("product_name")]
    public string ProductName { get; init; }

    /// <summary>
    /// Gets the version of the product in use.
    /// </summary>
    /// <remarks>
    /// Represents the `product_version` field in the server information.
    /// </remarks>
    [JsonPropertyName("product_version")]
    public string ProductVersion { get; init; }

    /// <summary>
    /// Gets the RabbitMQ definition format version.
    /// </summary>
    /// <remarks>
    /// Represents the `rabbitmq_definition_format` field in the server information.
    /// </remarks>
    [JsonPropertyName("rabbitmq_definition_format")]
    public string RabbitMqDefinitionFormat { get; init; }

    /// <summary>
    /// Gets the original name of the RabbitMQ cluster.
    /// </summary>
    /// <remarks>
    /// Represents the `original_cluster_name` field in the server information.
    /// </remarks>
    [JsonPropertyName("original_cluster_name")]
    public string OriginalClusterName { get; init; }

    /// <summary>
    /// Provides a detailed explanation or description associated with the server information.
    /// </summary>
    /// <remarks>
    /// Represents the `explanation` field in the server's detailed information response.
    /// </remarks>
    [JsonPropertyName("explanation")]
    public string Explanation { get; init; }

    /// <summary>
    /// Gets the collection of users in the RabbitMQ server.
    /// </summary>
    /// <remarks>
    /// Represents the `users` field in the server information.
    /// </remarks>
    [JsonPropertyName("users")]
    public IList<UserInfo> Users { get; init; }

    /// <summary>
    /// Gets the list of all virtual hosts configured on the RabbitMQ server.
    /// </summary>
    /// <remarks>
    /// Represents the `vhosts` field in the server information, which contains details about each virtual host.
    /// </remarks>
    [JsonPropertyName("vhosts")]
    public IList<VirtualHostOverviewInfo> VirtualHosts { get; init; }

    /// <summary>
    /// Gets the collection of user permissions configured within the RabbitMQ server.
    /// </summary>
    /// <remarks>
    /// Represents the `permissions` field in the server definitions, which provides
    /// details regarding user-specific permissions for virtual hosts and resource access.
    /// </remarks>
    [JsonPropertyName("permissions")]
    public IList<UserPermissionsInfo> Permissions { get; init; }

    /// <summary>
    /// Gets the list of policies configured on the RabbitMQ server.
    /// </summary>
    /// <remarks>
    /// Represents the `policies` field in the server information, containing details about each policy.
    /// </remarks>
    [JsonPropertyName("policies")]
    public IList<PolicyInfo> Policies { get; init; }

    /// <summary>
    /// Gets the list of scoped parameters within the RabbitMQ server.
    /// </summary>
    /// <remarks>
    /// Represents the configurations applied to specific components in virtual hosts, defined as scoped parameters.
    /// </remarks>
    [JsonPropertyName("parameters")]
    public IList<ScopedParameterInfo> Parameters { get; init; }

    /// <summary>
    /// Gets the collection of global parameters configured in the RabbitMQ system.
    /// </summary>
    /// <remarks>
    /// Represents the `global_parameters` field retrieved from the RabbitMQ server information.
    /// Each global parameter contains a name and associated value.
    /// </remarks>
    [JsonPropertyName("global_parameters")]
    public IList<GlobalParameterInfo> GlobalParameters { get; init; }

    /// <summary>
    /// Gets the collection of queues present in the RabbitMQ server.
    /// </summary>
    /// <remarks>
    /// Provides details on the available queues, including configurations for each queue.
    /// </remarks>
    [JsonPropertyName("queues")]
    public IList<QueueInfo> Queues { get; init; }

    /// <summary>
    /// Gets the collection of exchange information for RabbitMQ.
    /// </summary>
    /// <remarks>
    /// Represents the `exchanges` field in the server information.
    /// </remarks>
    [JsonPropertyName("exchanges")]
    public IList<ExchangeInfo> Exchanges { get; init; }

    /// <summary>
    /// Gets the list of bindings on the RabbitMQ server.
    /// </summary>
    /// <remarks>
    /// Represents the `bindings` field in the server information.
    /// A binding is a link between a queue and an exchange that defines rules
    /// for routing messages between them.
    /// </remarks>
    [JsonPropertyName("bindings")]
    public IList<BindingInfo> Bindings { get; init; }

    /// <summary>
    /// Gets the list of topic permissions configured within the RabbitMQ server.
    /// </summary>
    /// <remarks>
    /// Each entry represents a set of permissions assigned to a user for topic-based operations,
    /// including the ability to read from or write to specific exchanges within a particular virtual host.
    /// </remarks>
    [JsonPropertyName("topic_permissions")]
    public IList<TopicPermissionsInfo> TopicPermissions { get; init; }
}