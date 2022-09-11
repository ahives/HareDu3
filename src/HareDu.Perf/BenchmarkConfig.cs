namespace HareDu.Perf;

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

public class BenchmarkConfig :
    ManualConfig
{
    const int Iteration = 25;

    public BenchmarkConfig()
    {
        Add(MemoryDiagnoser.Default);
        Add(HtmlExporter.Default);
        Add(new Job
        {
            Environment = { Runtime = CoreRuntime.Core50 },
            Run =
            {
                IterationCount = Iteration,
                RunStrategy = RunStrategy.Throughput,
                WarmupCount = 1,
                LaunchCount = 1,
                UnrollFactor = 1,
                InvocationCount = Iteration
            }
        });
    }
}