namespace HareDu.Perf.Benchmarks
{
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;

    [Config(typeof(BenchmarkConfig))]
    public class GetAllBindingsBenchmarks :
        HareDuPerformanceTesting
    {
        readonly ServiceProvider _services;

        public GetAllBindingsBenchmarks()
        {
            _services = GetContainerBuilder("Benchmarks/TestData/BindingInfo.json").BuildServiceProvider();
        }

        [Benchmark]
        public async Task GetAllBindingsBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .GetAll();
        }

        [Benchmark]
        public async Task GetAllBindingsExtensionBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .GetAllBindings();
        }
    }
}