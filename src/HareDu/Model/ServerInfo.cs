namespace HareDu.Model;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public record ServerInfo
{
    [JsonPropertyName("rabbit_version")]
    public string RabbitMqVersion { get; init; }
        
    [JsonPropertyName("users")]
    public IList<UserInfo> Users { get; init; }
        
    [JsonPropertyName("vhosts")]
    public IList<VirtualHostInfo> VirtualHosts { get; init; }
        
    [JsonPropertyName("permissions")]
    public IList<UserPermissionsInfo> Permissions { get; init; }
        
    [JsonPropertyName("policies")]
    public IList<PolicyInfo> Policies { get; init; }
        
    [JsonPropertyName("parameters")]
    public IList<ScopedParameterInfo> Parameters { get; init; }
        
    [JsonPropertyName("global_parameters")]
    public IList<GlobalParameterInfo> GlobalParameters { get; init; }
        
    [JsonPropertyName("queues")]
    public IList<QueueInfo> Queues { get; init; }
        
    [JsonPropertyName("exchanges")]
    public IList<ExchangeInfo> Exchanges { get; init; }
        
    [JsonPropertyName("bindings")]
    public IList<BindingInfo> Bindings { get; init; }
        
    [JsonPropertyName("topic_permissions")]
    public IList<TopicPermissionsInfo> TopicPermissions { get; init; }
}