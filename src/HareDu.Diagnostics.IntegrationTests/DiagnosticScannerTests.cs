namespace HareDu.Diagnostics.IntegrationTests;

using System;
using System.Threading.Tasks;
using DependencyInjection;
using Formatting;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Snapshotting;
using Snapshotting.DependencyInjection;
using Snapshotting.Model;

[TestFixture]
public class DiagnosticScannerTests
{
    ServiceProvider _services;

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDuSnapshotting()
            .AddHareDuDiagnostics()
            .BuildServiceProvider();
    }

    [Test]
    public void Test()
    {
        Console.WriteLine(Guid.CreateVersion7(DateTimeOffset.UtcNow));
    }

    [Test]
    public async Task Test1()
    {
        var scanner = _services.GetService<IScanner>();
        var lens = _services.GetService<ISnapshotFactory>()
            .Lens<BrokerConnectivitySnapshot>();
            
        var result = await lens.TakeSnapshot(x => x.UsingCredentials("guest", "guest"));;
            
        var report = scanner.Scan(result.Snapshot);

        var formatter = _services.GetService<IDiagnosticReportFormatter>();
            
        string formattedReport = formatter.Format(report);
             
        Console.WriteLine(formattedReport);
    }

    [Test]
    public async Task Test3()
    {
        var lens = _services.GetService<ISnapshotFactory>()
            .Lens<BrokerConnectivitySnapshot>();
            
        var result = await lens.TakeSnapshot(x => x.UsingCredentials("guest", "guest"));
            
        var scanner = _services.GetService<IScanner>();

        var report = scanner.Scan(result.Snapshot);

        var formatter = _services.GetService<IDiagnosticReportFormatter>();

        string formattedReport = formatter.Format(report);
            
        Console.WriteLine(formattedReport);
            
//            for (int i = 0; i < report.Results.Count; i++)
//            {
//                Console.WriteLine("Diagnostic => Channel: {0}, Status: {1}", report.Results[i].ComponentIdentifier, report.Results[i].Status);
//                
//                if (report.Results[i].Status == DiagnosticStatus.Red)
//                {
//                    Console.WriteLine(report.Results[i].KnowledgeBaseArticle.Reason);
//                    Console.WriteLine(report.Results[i].KnowledgeBaseArticle.Remediation);
//                }
//            }
    }
        
    // [Test]
    // public async Task Test4()
    // {
    //     var provider = new YamlFileConfigProvider();
    //     provider.TryGet($"{Directory.GetCurrentDirectory()}/haredu.yaml", out HareDuConfig config);
    //
    //     var factory = new SnapshotFactory(new BrokerObjectFactory(config));
    //
    //     var lens = factory.Lens<BrokerConnectivitySnapshot>();
    //     var result = lens.TakeSnapshot();
    //     
    //     IScanner scanner = new Scanner(
    //         new ScannerFactory(config, new KnowledgeBaseProvider()));
    //
    //     var report = scanner.Scan(result.Snapshot);
    //
    //     var formatter = new DiagnosticReportTextFormatter();
    //
    //     string formattedReport = formatter.Format(report);
    //     
    //     Console.WriteLine(formattedReport);
    // }
    //
    // [Test]
    // public async Task Test5()
    // {
    //     var provider = new YamlFileConfigProvider();
    //     provider.TryGet($"{Directory.GetCurrentDirectory()}/haredu.yaml", out HareDuConfig config);
    //
    //     var factory = new SnapshotFactory(config);
    //     var lens = factory.Lens<BrokerConnectivitySnapshot>();
    //     var result = lens.TakeSnapshot();
    //
    //     var scannerFactory = new ScannerFactory(config, new KnowledgeBaseProvider());
    //     IScanner scanner = new Scanner(scannerFactory);
    //
    //     var report = scanner.Scan(result.Snapshot);
    //
    //     var formatter = new DiagnosticReportTextFormatter();
    //
    //     string formattedReport = formatter.Format(report);
    //     
    //     Console.WriteLine(formattedReport);
    // }
        
    // [Test]
    // public async Task Test6()
    // {
    //     var provider = new HareDuConfigProvider();
    //
    //     var config1 = provider.Configure(x =>
    //     {
    //         x.Broker(y =>
    //         {
    //             y.ConnectTo("http://localhost:15672");
    //             y.UsingCredentials("guest", "guest");
    //         });
    //
    //         x.Diagnostics(y =>
    //         {
    //             y.Probes(z =>
    //             {
    //                 z.SetMessageRedeliveryThresholdCoefficient(0.60M);
    //                 z.SetSocketUsageThresholdCoefficient(0.60M);
    //                 z.SetConsumerUtilizationThreshold(0.65M);
    //                 z.SetQueueHighFlowThreshold(90);
    //                 z.SetQueueLowFlowThreshold(10);
    //                 z.SetRuntimeProcessUsageThresholdCoefficient(0.65M);
    //                 z.SetFileDescriptorUsageThresholdCoefficient(0.65M);
    //                 z.SetHighConnectionClosureRateThreshold(90);
    //                 z.SetHighConnectionCreationRateThreshold(60);
    //             });
    //         });
    //     });
    //     
    //     var factory1 = new SnapshotFactory(config1);
    //     var lens = factory1.Lens<BrokerConnectivitySnapshot>();
    //     var result = await lens.TakeSnapshot();
    //     var factory2 = new ScannerFactory(config1, new KnowledgeBaseProvider());
    //     IScanner scanner = new Scanner(factory2);
    //
    //     var report = scanner.Scan(result.Snapshot);
    //
    //     var formatter = new DiagnosticReportTextFormatter();
    //
    //     string formattedReport = formatter.Format(report);
    //         
    //     Console.WriteLine(formattedReport);
    // }
}