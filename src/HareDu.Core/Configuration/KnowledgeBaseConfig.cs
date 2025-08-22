namespace HareDu.Core.Configuration;

/// <summary>
/// Represents the configuration settings for the knowledge base.
/// </summary>
public record KnowledgeBaseConfig
{
    /// <summary>
    /// Represents the file name used within the knowledge base configuration.
    /// </summary>
    public string File { get; init; }

    /// <summary>
    /// Represents the relative path to the directory containing knowledge base articles.
    /// </summary>
    public string Path { get; init; }
}