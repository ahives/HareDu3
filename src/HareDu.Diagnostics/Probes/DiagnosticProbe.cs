namespace HareDu.Diagnostics.Probes;

using System;

public interface DiagnosticProbe :
    IObservable<ProbeContext>
{
    /// <summary>
    /// Miscellaneous information pertinent to describing the diagnostic probe.
    /// </summary>
    ProbeMetadata Metadata { get; }
        
    /// <summary>
    /// The type of RabbitMQ component the diagnostic probe can execute against.
    /// </summary>
    ComponentType ComponentType { get; }
        
    /// <summary>
    /// The category used to classify what type of data the diagnostic probe can execute against.
    /// </summary>
    ProbeCategory Category { get; }
    
    /// <summary>
    /// 
    /// </summary>
    // bool HasExecuted { get; set; }
    
    /// <summary>
    /// Executes a set of rules against the snapshot and returns a corresponding result.
    /// </summary>
    /// <param name="snapshot"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    ProbeResult Execute<T>(T snapshot);
}