namespace HareDu.IntegrationTests;

using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;

[TestFixture]
public class RateLimitTests
{
    ServiceProvider _services;
    string _vhost = "QueueTestVirtulHost";
    string _node = "rabbit@192a30bbb161";

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddSingleton<ITestClient, TestClient>()
            .AddHareDu(x =>
            {
                x.Broker(b =>
                {
                    b.ConnectTo("http://localhost:15672");
                    b.WithBehavior(behavior =>
                    {
                        behavior.LimitRequests(5, 5);
                    });
                });
                x.Diagnostics(d =>
                {
                    d.Probes(p =>
                    {
                        p.SetConsumerUtilizationThreshold(1);
                        p.SetFileDescriptorUsageThresholdCoefficient(1);
                        p.SetHighConnectionClosureRateThreshold(1);
                        p.SetFileDescriptorUsageThresholdCoefficient(1);
                        p.SetHighConnectionClosureRateThreshold(1);
                        p.SetMessageRedeliveryThresholdCoefficient(1);
                        p.SetHighConnectionCreationRateThreshold(1);
                        p.SetQueueHighFlowThreshold(1);
                        p.SetQueueLowFlowThreshold(1);
                        p.SetRuntimeProcessUsageThresholdCoefficient(1);
                        p.SetSocketUsageThresholdCoefficient(1);
                    });
                });
            })
            .BuildServiceProvider();
    }

    [Test]
    public async Task Test()
    {
        var oneHundredUrls = Enumerable.Range(0, 100).Select(i => $"https://example.com?iteration={i:0#}");
        var t = _services.GetService<ITestClient>();

        var floodOneThroughFortyNineTask = Parallel.ForEachAsync(
            source: oneHundredUrls.Take(0..49),
            body: (url, cancellationToken) => t.Get(url, cancellationToken));

        var floodFiftyThroughOneHundredTask = Parallel.ForEachAsync(
            source: oneHundredUrls.Take(^50..),
            body: (url, cancellationToken) => t.Get(url, cancellationToken));

        await Task.WhenAll(
            floodOneThroughFortyNineTask,
            floodFiftyThroughOneHundredTask);
    }

    class TestClient :
        ITestClient
    {
        private readonly IHttpClientFactory _factory;

        public TestClient(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async ValueTask Get(string url, CancellationToken cancellationToken)
        {
            using var response =
                await _factory.CreateClient("broker").GetAsync(url, cancellationToken);

            Console.WriteLine(
                $"URL: {url}, HTTP status code: {response.StatusCode} ({(int) response.StatusCode})");
        }
    }

    interface ITestClient
    {
        ValueTask Get(string url, CancellationToken cancellationToken);
    }
}
