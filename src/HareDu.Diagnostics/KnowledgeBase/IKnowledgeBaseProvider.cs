namespace HareDu.Diagnostics.KnowledgeBase;

using System.Collections.Generic;
using Model;
using Probes;

/// <summary>
/// Defines the contract for a provider that manages and retrieves knowledge base articles
/// related to diagnostic probes.
/// </summary>
public interface IKnowledgeBaseProvider
{
    /// <summary>
    /// Attempts to retrieve a knowledge base article corresponding to the given identifier and probe result status.
    /// </summary>
    /// <param name="identifier">The unique identifier for the knowledge base entry to be retrieved.</param>
    /// <param name="status">The status of the diagnostic probe result to filter the knowledge base article.</param>
    /// <param name="article">Outputs the retrieved knowledge base article if found; otherwise, <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if the knowledge base article is successfully retrieved; otherwise, <c>false</c>.
    /// </returns>
    bool TryGet(string identifier, ProbeResultStatus status, out KnowledgeBaseArticle article);

    /// <summary>
    /// Attempts to retrieve all knowledge base articles corresponding to the given identifier.
    /// </summary>
    /// <param name="identifier">The unique identifier for the knowledge base entries to be retrieved.</param>
    /// <param name="articles">Outputs a read-only list of knowledge base articles if found; otherwise, an empty list.</param>
    /// <returns>
    /// <c>true</c> if one or more knowledge base articles are successfully retrieved; otherwise, <c>false</c>.
    /// </returns>
    bool TryGet(string identifier, out IReadOnlyList<KnowledgeBaseArticle> articles);

    /// <summary>
    /// Adds a knowledge base article associated with the specified diagnostic probe type, probe result status, reason, and remediation steps.
    /// </summary>
    /// <param name="status">The status of the diagnostic probe result that the knowledge base article is associated with.</param>
    /// <param name="reason">The reason describing why the diagnostic probe resulted in the given status.</param>
    /// <param name="remediation">The recommended remediation steps for addressing the diagnostic probe result.</param>
    /// <typeparam name="T">The type of the diagnostic probe associated with the knowledge base article.</typeparam>
    void Add<T>(ProbeResultStatus status, string reason, string remediation)
        where T : DiagnosticProbe;
}