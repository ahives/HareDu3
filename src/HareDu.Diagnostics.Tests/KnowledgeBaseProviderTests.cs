namespace HareDu.Diagnostics.Tests;

using System;
using Core.Extensions;
using DependencyInjection;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using Model;
using NUnit.Framework;

[TestFixture]
public class KnowledgeBaseProviderTests
{
    ServiceProvider _services;

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDuDiagnostics()
            .BuildServiceProvider();
    }

    [Test]
    public void Test()
    {
        string reason = "This is a test";
        _services.GetService<IKnowledgeBaseProvider>()
            .Add<TestProbe>(ProbeResultStatus.Healthy, reason, null);

        bool found = _services.GetService<IKnowledgeBaseProvider>()
            .TryGet(typeof(TestProbe).GetIdentifier(), ProbeResultStatus.Healthy, out var article);
            
        Assert.Multiple(() =>
        {
            Assert.That(found, Is.True);
            Assert.That(article, Is.Not.Null);
            Assert.That(article.Id, Is.EqualTo(typeof(TestProbe).GetIdentifier()));
            Assert.That(article.Reason, Is.EqualTo(reason));
            Assert.That(article.Status, Is.EqualTo(ProbeResultStatus.Healthy));
        });
    }

    class TestProbe :
        DiagnosticProbe
    {
        public IDisposable Subscribe(IObserver<ProbeContext> observer) => throw new NotImplementedException();

        public ProbeMetadata Metadata { get; }
        public ComponentType ComponentType { get; }
        public ProbeCategory Category { get; }
        public ProbeResult Execute<T>(T snapshot) => throw new NotImplementedException();
    }
}