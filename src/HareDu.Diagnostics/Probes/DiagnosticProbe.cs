namespace HareDu.Diagnostics.Probes
{
    using System;

    public interface DiagnosticProbe :
        IObservable<ProbeContext>,
        IObservable<ProbeConfigurationContext>
    {
        string Id { get; }
        
        string Name { get; }
        
        string Description { get; }
        
        ComponentType ComponentType { get; }
        
        ProbeCategory Category { get; }
        
        ProbeResult Execute<T>(T snapshot);
    }
}