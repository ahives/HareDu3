namespace HareDu.Diagnostics.KnowledgeBase
{
    public record KnowledgeBaseArticle
    {
        public string Id { get; init; }
        
        public ProbeResultStatus Status { get; init; }
        
        public string Reason { get; init; }
        
        public string Remediation { get; init; }
    }
}