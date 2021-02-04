namespace HareDu.Diagnostics.KnowledgeBase
{
    public record MissingKnowledgeBaseArticle :
        KnowledgeBaseArticle
    {
        public MissingKnowledgeBaseArticle()
        {
            Reason = "No KB article Available";
            Remediation = "NA";
        }
    }
}