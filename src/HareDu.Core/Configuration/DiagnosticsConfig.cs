namespace HareDu.Core.Configuration;

public record DiagnosticsConfig
{
    public ProbesConfig Probes { get; init; }
}