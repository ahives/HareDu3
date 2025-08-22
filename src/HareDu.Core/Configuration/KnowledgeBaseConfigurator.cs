namespace HareDu.Core.Configuration;

/// <summary>
/// Provides an interface for configuring knowledge base settings, including file names and paths.
/// </summary>
public interface KnowledgeBaseConfigurator
{
    /// <summary>
    /// Configures the knowledge base file settings.
    /// </summary>
    /// <param name="name">The name of the configuration file.</param>
    /// <param name="path">The file path where the configuration file is located.</param>
    void File(string name, string path);
}