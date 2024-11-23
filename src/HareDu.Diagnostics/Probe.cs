namespace HareDu.Diagnostics;

using System.Collections.Generic;
using KnowledgeBase;

public static class Probe
{
    public static ProbeResult Healthy(string parentComponentId, string componentId, ProbeMetadata probeMetadata,
        ComponentType componentType, IReadOnlyList<ProbeData> probeData, KnowledgeBaseArticle article) =>
        new()
        {
            Status = ProbeResultStatus.Healthy,
            ParentComponentId = parentComponentId,
            ComponentId = componentId,
            Id = probeMetadata.Id,
            Name = probeMetadata.Name,
            ComponentType = componentType,
            Data = probeData,
            KB = article
        };

    public static ProbeResult Unhealthy(string parentComponentId, string componentId, ProbeMetadata probeMetadata,
        ComponentType componentType, IReadOnlyList<ProbeData> probeData, KnowledgeBaseArticle article) =>
        new()
        {
            Status = ProbeResultStatus.Unhealthy,
            ParentComponentId = parentComponentId,
            ComponentId = componentId,
            Id = probeMetadata.Id,
            Name = probeMetadata.Name,
            ComponentType = componentType,
            Data = probeData,
            KB = article
        };

    public static ProbeResult Warning(string parentComponentId, string componentId, ProbeMetadata probeMetadata,
        ComponentType componentType, IReadOnlyList<ProbeData> probeData, KnowledgeBaseArticle article) =>
        new()
        {
            Status = ProbeResultStatus.Warning,
            ParentComponentId = parentComponentId,
            ComponentId = componentId,
            Id = probeMetadata.Id,
            Name = probeMetadata.Name,
            ComponentType = componentType,
            Data = probeData,
            KB = article
        };

    public static ProbeResult Inconclusive(string parentComponentId, string componentId, ProbeMetadata probeMetadata,
        ComponentType componentType, IReadOnlyList<ProbeData> probeData, KnowledgeBaseArticle article) =>
        new()
        {
            Status = ProbeResultStatus.Inconclusive,
            ParentComponentId = parentComponentId,
            ComponentId = componentId,
            Id = probeMetadata.Id,
            Name = probeMetadata.Name,
            ComponentType = componentType,
            Data = probeData,
            KB = article
        };

    public static ProbeResult NotAvailable(string parentComponentId, string componentId, ProbeMetadata probeMetadata,
        ComponentType componentType, IReadOnlyList<ProbeData> probeData, KnowledgeBaseArticle article) =>
        new()
        {
            Status = ProbeResultStatus.NA,
            ParentComponentId = parentComponentId,
            ComponentId = componentId,
            Id = probeMetadata.Id,
            Name = probeMetadata.Name,
            ComponentType = componentType,
            Data = probeData,
            KB = article
        };
}