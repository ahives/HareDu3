namespace HareDu.Diagnostics.Probes;

using System;
using Model;

/// <summary>
/// Defines the contract for a diagnostic probe that can analyze various snapshots of provided data
/// and return evaluation results to determine the state of the system under observation.
/// </summary>
public interface DiagnosticProbe :
    IObservable<ProbeContext>
{
    /// <summary>
    /// Provides essential metadata about the diagnostic probe, including its unique identifier, name, and a description
    /// explaining the probe’s purpose or functionality.
    /// </summary>
    ProbeMetadata Metadata { get; }

    /// <summary>
    /// Represents the type of RabbitMQ component (e.g., Queue, Connection, Channel)
    /// that the diagnostic probe is associated with or monitors.
    /// </summary>
    ComponentType ComponentType { get; }
        
    /// <summary>
    /// Represents the classification of the diagnostic probe, defining the type of data
    /// the probe evaluates during its execution.
    /// </summary>
    ProbeCategory Category { get; }

    /// <summary>
    /// 
    /// </summary>
    // bool HasExecuted { get; set; }

    /// <summary>
    /// Executes the diagnostic operation using the provided snapshot and returns the result of the probe.
    /// </summary>
    /// <typeparam name="T">The type of the snapshot used for the diagnostic operation.</typeparam>
    /// <param name="snapshot">The data snapshot used for performing the probe.</param>
    /// <returns>The result of executing the probe as a <see cref="ProbeResult"/>.</returns>
    ProbeResult Execute<T>(T snapshot);
}