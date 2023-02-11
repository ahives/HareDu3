using BenchmarkDotNet.Running;
using HareDu.Perf.Benchmarks;

var run = BenchmarkRunner.Run<GetAllChannelBenchmarks>();

// var run = BenchmarkRunner.Run(typeof(Program).Assembly);

// BenchmarkRunner
// class Program
// {
//     static void Main(string[] args)
//     {
//         var run = BenchmarkRunner.Run(typeof(Program).Assembly);
//         // var run = BenchmarkRunner.Run<GetAllChannelBenchmarks>();
//     }
// }