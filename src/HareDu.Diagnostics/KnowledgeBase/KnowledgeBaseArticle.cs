namespace HareDu.Diagnostics.KnowledgeBase;

using System.Text.Json.Serialization;
using Model;

/// <summary>
/// Represents an article within a knowledge base, containing information on a specific diagnostic result.
/// </summary>
public record KnowledgeBaseArticle
{
    /// <summary>
    /// Represents the unique identifier associated with the knowledge base article,
    /// providing a reference to the specific diagnostic information relevant to the probe.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// Represents the diagnostic outcome associated with a knowledge base article, indicating
    /// the health or state classification based on the probe's execution.
    /// </summary>
    [JsonPropertyName("status")]
    public ProbeResultStatus Status { get; init; }

    /// <summary>
    /// Describes why the diagnostic probe returned its result, offering context or explanation
    /// related to the probe's status evaluation.
    /// </summary>
    [JsonPropertyName("reason")]
    public string Reason { get; init; }

    /// <summary>
    /// Provides a description of the recommended remedial action or steps for resolving
    /// the issue addressed by the knowledge base article.
    /// </summary>
    [JsonPropertyName("remediation")]
    public string Remediation { get; init; }
}