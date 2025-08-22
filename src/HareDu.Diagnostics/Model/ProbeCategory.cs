namespace HareDu.Diagnostics.Model;

/// <summary>
/// Represents categories used to organize diagnostic probes based on the type of system behavior
/// or performance aspect they evaluate. Each category indicates the functional focus
/// of the corresponding diagnostic probes.
/// </summary>
public enum ProbeCategory
{
    /// <summary>
    /// Represents a category of diagnostic probes that evaluate system performance in terms of
    /// data processing and operational efficiency (e.g., throughput or capacity analysis).
    /// </summary>
    Throughput,

    /// <summary>
    /// Represents a category of diagnostic probes that focus on assessing the system's network connectivity,
    /// including analyzing network partitions, communication issues, and other factors affecting inter-component connectivity.
    /// </summary>
    Connectivity,

    /// <summary>
    /// Represents a category of diagnostic probes that analyze system memory usage, including
    /// resource utilization, memory allocation, and potential memory constraints.
    /// </summary>
    Memory,

    /// <summary>
    /// Represents a category of diagnostic probes that assess the system's ability to handle faults,
    /// ensuring stability and continuity in the presence of errors, failures, or disruptions.
    /// </summary>
    FaultTolerance,

    /// <summary>
    /// Represents a category of diagnostic probes that assess system performance related to
    /// resource utilization and overall operational effectiveness.
    /// </summary>
    Efficiency
}