namespace HareDu.Diagnostics.Model;

using System;
using System.Collections.Generic;
using KnowledgeBase;

/// <summary>
/// Represents the result of executing a diagnostic probe in the system.
/// </summary>
public record ProbeResult
{
    /// <summary>
    /// Identifier of the parent component associated with the probe result.
    /// </summary>
    public string ParentComponentId { get; init; }

    /// <summary>
    /// Identifier of the current component associated with the probe result.
    /// </summary>
    public string ComponentId { get; init; }

    /// <summary>
    /// Type of component being evaluated by the probe result.
    /// </summary>
    public ComponentType ComponentType { get; init; }

    /// <summary>
    /// Unique identifier representing the specific diagnostic probe result.
    /// </summary>
    public string Id { get; init; }

    /// <summary>
    /// Name of the probe associated with the diagnostic result.
    /// </summary>
    public string Name { get; init; }

    /// <summary>
    /// Represents the evaluation result of a diagnostic probe, indicating the health status of a specific component or system.
    /// </summary>
    public ProbeResultStatus Status { get; init; }

    /// <summary>
    /// Represents the knowledge base article associated with the probe result,
    /// which provides additional context, status, reason, and remediation related to the result.
    /// </summary>
    public KnowledgeBaseArticle KB { get; init; }

    /// <summary>
    /// Collection of data points associated with the execution of a diagnostic probe.
    /// </summary>
    public IReadOnlyList<ProbeData> Data { get; init; }

    /// <summary>
    /// The date and time when the probe result was created or recorded.
    /// </summary>
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;
}