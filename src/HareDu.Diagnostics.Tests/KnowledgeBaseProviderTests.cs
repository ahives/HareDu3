namespace HareDu.Diagnostics.Tests;

using System;
using Core.Extensions;
using Diagnostics.Probes;
using KnowledgeBase;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;

[TestFixture]
public class KnowledgeBaseProviderTests
{
    ServiceProvider _services;

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDu()
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
            Assert.IsTrue(found);
            Assert.IsNotNull(article);
            Assert.AreEqual(typeof(TestProbe).GetIdentifier(), article.Id);
            Assert.AreEqual(reason, article.Reason);
            Assert.AreEqual(ProbeResultStatus.Healthy, article.Status);
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