namespace HareDu.Perf
{
    using BenchmarkDotNet.Running;
    using Benchmarks;

    class Program
    {
        static void Main(string[] args)
        {
            var run = BenchmarkRunner.Run(typeof(Program).Assembly);
            // var run = BenchmarkRunner.Run<GetAllChannelBenchmarks>();
        }
    }
}