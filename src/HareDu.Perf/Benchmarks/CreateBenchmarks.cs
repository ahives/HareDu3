namespace HareDu.Perf.Benchmarks;

using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Core.Security;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;

[Config(typeof(BenchmarkConfig))]
public class CreateBenchmarks :
    HareDuPerformanceTesting
{
    readonly IBrokerFactory _service;

    public CreateBenchmarks()
    {
        var services = GetContainerBuilder().BuildServiceProvider();
            
        _service = services.GetService<IBrokerFactory>();
    }

    [Benchmark]
    public async Task QueueCreateBenchmark()
    {
        var services = GetContainerBuilder()
            .BuildServiceProvider();
        var provider = services.GetService<IHareDuCredentialBuilder>();
        var result = await services.GetService<IBrokerFactory>()
            .API<Queue>(x => x.UsingCredentials("guest", "guest"))
            .Create("TestQueue31", "HareDu", "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetPerQueuedMessageExpiration(2000);
                    arg.SetAlternateExchange("exchange2");
                    arg.SetDeadLetterExchange("exchange-dead");
                    arg.SetDeadLetterExchangeRoutingKey(".*");
                });
            });
    }

    [Benchmark]
    public async Task QueueCreateExtensionBenchmark()
    {
        var result = await _service
            .CreateQueue(x => x.UsingCredentials("guest", "guest"), "TestQueue31", "HareDu", "Node1", x =>
            {
                x.IsDurable();
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.SetQueueExpiration(1000);
                    arg.SetPerQueuedMessageExpiration(2000);
                    arg.SetAlternateExchange("exchange2");
                    arg.SetDeadLetterExchange("exchange-dead");
                    arg.SetDeadLetterExchangeRoutingKey(".*");
                });
            });
    }

    [Benchmark]
    public async Task ExchangeCreateBenchmark()
    {
        var result = await _service
            .API<Exchange>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_exchange", "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.WithRoutingType(RoutingType.Direct);
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
    }

    [Benchmark]
    public async Task ExchangeCreateExtensionBenchmark()
    {
        var result = await _service
            .CreateExchange(x => x.UsingCredentials("guest", "guest"), "fake_exchange", "HareDu", x =>
            {
                x.IsDurable();
                x.IsForInternalUse();
                x.WithRoutingType(RoutingType.Direct);
                x.AutoDeleteWhenNotInUse();
                x.HasArguments(arg =>
                {
                    arg.Add("fake_arg", "8238b");
                });
            });
    }

    [Benchmark]
    public async Task GlobalParameterCreateBenchmark()
    {
        var result = await _service
            .API<GlobalParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create("fake_param",x =>
            {
                x.Add("arg1", 1);
                x.Add("arg2", 2);
                x.Add("arg3", 3);
            });
    }

    [Benchmark]
    public async Task GlobalParameterCreateExtensionBenchmark()
    {
        var result = await _service
            .CreateGlobalParameter(x => x.UsingCredentials("guest", "guest"), "fake_param",x =>
            {
                x.Add("arg1", 1);
                x.Add("arg2", 2);
                x.Add("arg3", 3);
            });
    }

    [Benchmark]
    public async Task ScopedParameterCreateBenchmark()
    {
        var result = await _service
            .API<ScopedParameter>(x => x.UsingCredentials("guest", "guest"))
            .Create<long>("fake_parameter", 89, "fake_component", "HareDu");
    }

    [Benchmark]
    public async Task ScopedParameterCreateExtensionBenchmark()
    {
        var result = await _service
            .CreateScopeParameter<long>(x => x.UsingCredentials("guest", "guest"), "fake_parameter", 89, "fake_component", "HareDu");
    }

    [Benchmark]
    public async Task TopicPermissionsCreateBenchmark()
    {
        var result = await _service
            .API<TopicPermissions>(x => x.UsingCredentials("guest", "guest"))
            .Create("user1", "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });
    }

    [Benchmark]
    public async Task TopicPermissionsCreateExtensionBenchmark()
    {
        var result = await _service
            .CreateTopicPermission(x => x.UsingCredentials("guest", "guest"), "user1", "HareDu", x =>
            {
                x.Exchange("E4");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });
    }

    [Benchmark]
    public async Task UserPermissionsCreateBenchmark()
    {
        var result = await _service
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .ApplyPermissions("user", "HareDu", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });
    }

    [Benchmark]
    public async Task UserPermissionsCreateExtensionBenchmark()
    {
        var result = await _service
            .ApplyVirtualHostUserPermissions(x => x.UsingCredentials("guest", "guest"), "user", "HareDu", x =>
            {
                x.UsingConfigurePattern(".*");
                x.UsingReadPattern(".*");
                x.UsingWritePattern(".*");
            });
    }

    [Benchmark]
    public async Task UserCreateBenchmark()
    {
        var result = await _service
            .API<User>(x => x.UsingCredentials("guest", "guest"))
            .Create("user", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
                });
            });
    }

    [Benchmark]
    public async Task UserCreateExtensionBenchmark()
    {
        var result = await _service
            .CreateUser(x => x.UsingCredentials("guest", "guest"), "user", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
            {
                x.WithTags(t =>
                {
                    t.AddTag(UserAccessTag.Administrator);
                });
            });
    }

    [Benchmark]
    public async Task VirtualHostLimitsDefineBenchmark()
    {
        var result = await _service
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .DefineLimit("HareDu", x =>
            {
                x.SetMaxQueueLimit(1);
                x.SetMaxConnectionLimit(5);
            });
    }

    [Benchmark]
    public async Task VirtualHostLimitsDefineExtensionBenchmark()
    {
        var result = await _service
            .DefineVirtualHostLimit(x => x.UsingCredentials("guest", "guest"), "HareDu", x =>
            {
                x.SetMaxQueueLimit(1);
                x.SetMaxConnectionLimit(5);
            });
    }

    [Benchmark]
    public async Task VirtualHostCreateBenchmark()
    {
        var result = await _service
            .API<VirtualHost>(x => x.UsingCredentials("guest", "guest"))
            .Create("HareDu",x =>
            {
                x.Description("This is a vhost");
                x.Tags(t =>
                {
                    t.Add("tag1");
                    t.Add("tag2");
                    t.Add("tag3");
                });
                x.WithTracingEnabled();
            });
    }

    [Benchmark]
    public async Task VirtualHostCreateExtensionBenchmark()
    {
        var result = await _service
            .CreateVirtualHost(x => x.UsingCredentials("guest", "guest"), "HareDu",x =>
            {
                x.Description("This is a vhost");
                x.Tags(t =>
                {
                    t.Add("tag1");
                    t.Add("tag2");
                    t.Add("tag3");
                });
                x.WithTracingEnabled();
            });
    }

    // [Benchmark]
    // public async Task BindingCreateBenchmark()
    // {
    //     var result = await _service
    //         .API<Binding>()
    //         .Create("HareDu", x =>
    //         {
    //             x.Source("exchange");
    //             x.Destination("queue");
    //             x.BindingKey("*.");
    //             x.BindingType(BindingType.Exchange);
    //         });
    // }
    //
    // [Benchmark]
    // public async Task BindingCreateExtensionBenchmark()
    // {
    //     var result = await _service
    //         .CreateBinding("HareDu", x =>
    //         {
    //             x.Source("exchange");
    //             x.Destination("queue");
    //             x.BindingType(BindingType.Exchange);
    //         });
    // }
}