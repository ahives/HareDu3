namespace HareDu.Core.Configuration;

public record HareDuConfig
{
    public BrokerConfig Broker { get; init; }
        
    public DiagnosticsConfig Diagnostics { get; init; }
}