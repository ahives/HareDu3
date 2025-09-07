namespace HareDu.Perf.Benchmarks;

using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Core;
using Extensions;
using Microsoft.Extensions.DependencyInjection;

[Config(typeof(BenchmarkConfig))]
public class GetAllBindingsBenchmarks :
    HareDuPerformanceTesting
{
    readonly IHareDuFactory _service;

    public GetAllBindingsBenchmarks()
    {
        var services = GetContainerBuilder("Benchmarks/TestData/BindingInfo.json")
            .BuildServiceProvider();
            
        _service = services.GetService<IHareDuFactory>();
    }

    [Benchmark]
    public async Task GetAllBindingsBenchmark()
    {
        var result = await _service
            .API<Binding>(x => x.UsingCredentials("guest", "guest"))
            .GetAll();
    }

    [Benchmark]
    public async Task GetAllBindingsExtensionBenchmark()
    {
        var result = await _service
            .GetAllBindings(x => x.UsingCredentials("guest", "guest"));
    }
}