namespace HareDu.Perf.Benchmarks;

using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Extensions;
using Microsoft.Extensions.DependencyInjection;

[Config(typeof(BenchmarkConfig))]
public class GetAllChannelBenchmarks :
    HareDuPerformanceTesting
{
    readonly IBrokerApiFactory _service;

    public GetAllChannelBenchmarks()
    {
        var services = GetContainerBuilder("Benchmarks/TestData/ChannelInfo.json")
            .BuildServiceProvider();
            
        _service = services.GetService<IBrokerApiFactory>();
    }

    [Benchmark]
    public async Task GetAllChannelsBenchmark()
    {
        var result = await _service
            .Object<Channel>()
            .GetAll();
    }

    [Benchmark]
    public async Task GetAllChannelsExtensionBenchmark()
    {
        var result = await _service
            .GetAllChannels();
    }
}