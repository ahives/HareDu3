namespace HareDu.Diagnostics.KnowledgeBase
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Extensions;
    using Probes;

    public abstract class BaseKnowledgeBaseProvider :
        IKnowledgeBaseProvider
    {
        protected readonly List<KnowledgeBaseArticle> _articles;

        protected BaseKnowledgeBaseProvider()
        {
            _articles = new List<KnowledgeBaseArticle>();

            Load();
        }

        protected abstract void Load();

        public bool TryGet(string identifier, ProbeResultStatus status, out KnowledgeBaseArticle article)
        {
            if (_articles.Exists(x => x.Id == identifier))
            {
                try
                {
                    article = _articles.Single(x => x.Id == identifier && x.Status == status);
                    return true;
                }
                catch
                {
                    article = new MissingKnowledgeBaseArticle{Id = identifier, Status = status};
                    return false;
                }
            }

            article = new MissingKnowledgeBaseArticle{Id = identifier, Status = status};
            return false;
        }

        public bool TryGet(string identifier, out IReadOnlyList<KnowledgeBaseArticle> articles)
        {
            if (_articles.Exists(x => x.Id == identifier))
            {
                try
                {
                    articles = _articles.Where(x => x.Id == identifier).ToList();
                    return true;
                }
                catch
                {
                    articles = new KnowledgeBaseArticle[] {new MissingKnowledgeBaseArticle{Id = identifier, Status = ProbeResultStatus.NA}};
                    return false;
                }
            }

            articles = new KnowledgeBaseArticle[] {new MissingKnowledgeBaseArticle{Id = identifier, Status = ProbeResultStatus.NA}};
            return false;
        }

        public void Add<T>(ProbeResultStatus status, string reason, string remediation)
            where T : DiagnosticProbe
        {
            _articles.Add(new KnowledgeBaseArticle{Id = typeof(T).GetIdentifier(), Status = status, Reason = reason, Remediation = remediation});
        }
    }
}