namespace HareDu.Perf.Benchmarks;

using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Extensions;
using Microsoft.Extensions.DependencyInjection;

[Config(typeof(BenchmarkConfig))]
public class GetAllBindingsBenchmarks :
    HareDuPerformanceTesting
{
    readonly IBrokerObjectFactory _service;

    public GetAllBindingsBenchmarks()
    {
        var services = GetContainerBuilder("Benchmarks/TestData/BindingInfo.json")
            .BuildServiceProvider();
            
        _service = services.GetService<IBrokerObjectFactory>();
    }

    [Benchmark]
    public async Task GetAllBindingsBenchmark()
    {
        var result = await _service
            .Object<Binding>()
            .GetAll();
    }

    [Benchmark]
    public async Task GetAllBindingsExtensionBenchmark()
    {
        var result = await _service
            .GetAllBindings();
    }
}