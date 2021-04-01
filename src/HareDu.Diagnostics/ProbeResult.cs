namespace HareDu.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using KnowledgeBase;

    public record ProbeResult
    {
        public ProbeResult()
        {
            Timestamp = DateTimeOffset.UtcNow;
        }

        public string ParentComponentId { get; init; }
        
        public string ComponentId { get; init; }
        
        public ComponentType ComponentType { get; init; }
        
        /// <summary>
        /// Probe identifier
        /// </summary>
        public string Id { get; init; }
        
        /// <summary>
        /// Probe human readable name
        /// </summary>
        public string Name { get; init; }
        
        public ProbeResultStatus Status { get; init; }
        
        public KnowledgeBaseArticle KB { get; init; }
        
        public IReadOnlyList<ProbeData> Data { get; init; }
        
        public DateTimeOffset Timestamp { get; init; }
    }
}