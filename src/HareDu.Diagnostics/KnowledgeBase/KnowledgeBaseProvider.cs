namespace HareDu.Diagnostics.KnowledgeBase;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Core.Configuration;
using Model;
using Probes;
using Serialization;

/// <summary>
/// Represents a provider for accessing knowledge base articles related to diagnostic probes.
/// </summary>
public class KnowledgeBaseProvider :
    IKnowledgeBaseProvider
{
    readonly HareDuConfig _config;
    readonly List<KnowledgeBaseArticle> _articles;
    bool _loaded;

    public KnowledgeBaseProvider(HareDuConfig config)
    {
        _config = config;
        _articles = new List<KnowledgeBaseArticle>();
    }

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
                articles = [new MissingKnowledgeBaseArticle{Id = identifier, Status = ProbeResultStatus.NA}];
                return false;
            }
        }

        articles = [new MissingKnowledgeBaseArticle{Id = identifier, Status = ProbeResultStatus.NA}];
        return false;
    }

    public void Add<T>(ProbeResultStatus status, string reason, string remediation)
        where T : DiagnosticProbe
    {
        _articles.Add(new KnowledgeBaseArticle{Id = typeof(T).FullName, Status = status, Reason = reason, Remediation = remediation});
    }

    public void Load()
    {
        if (_loaded)
            return;

        string path = Path.Combine(Directory.GetCurrentDirectory(), _config.KB.Path, _config.KB.File);

        if (!File.Exists(path))
            throw new HareDuFileNotFoundException($"The file '{path}' does not exist.");

        string data = File.ReadAllText(path);
        var deserializer = new DiagnosticDeserializer();
        var articles = JsonSerializer.Deserialize<List<KnowledgeBaseArticle>>(data, deserializer.Options);

        _articles.AddRange(articles);
        _loaded = true;
    }
}