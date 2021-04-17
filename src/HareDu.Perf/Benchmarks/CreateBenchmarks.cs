namespace HareDu.Perf.Benchmarks
{
    using System.Threading.Tasks;
    using BenchmarkDotNet.Attributes;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;

    [Config(typeof(BenchmarkConfig))]
    public class CreateBenchmarks :
        HareDuPerformanceTesting
    {
        readonly ServiceProvider _services;

        public CreateBenchmarks()
        {
            _services = GetContainerBuilder().BuildServiceProvider();
        }

        [Benchmark]
        public async Task QueueCreateBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Queue>()
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
            var result = await _services.GetService<IBrokerObjectFactory>()
                .CreateQueue("TestQueue31", "HareDu", "Node1", x =>
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
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Exchange>()
                .Create("fake_exchange", "HareDu", x =>
                {
                    x.IsDurable();
                    x.IsForInternalUse();
                    x.HasRoutingType(ExchangeRoutingType.Direct);
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
            var result = await _services.GetService<IBrokerObjectFactory>()
                .CreateExchange("fake_exchange", "HareDu", x =>
                {
                    x.IsDurable();
                    x.IsForInternalUse();
                    x.HasRoutingType(ExchangeRoutingType.Direct);
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
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<GlobalParameter>()
                .Create("fake_param",x =>
                {
                    x.Value(arg =>
                    {
                        arg.Add("arg1", 1);
                        arg.Add("arg2", 2);
                        arg.Add("arg3", 3);
                    });
                });
        }

        [Benchmark]
        public async Task GlobalParameterCreateExtensionBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .CreateGlobalParameter("fake_param",x =>
                {
                    x.Value(arg =>
                    {
                        arg.Add("arg1", 1);
                        arg.Add("arg2", 2);
                        arg.Add("arg3", 3);
                    });
                });
        }

        [Benchmark]
        public async Task ScopedParameterCreateBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<ScopedParameter>()
                .Create<long>("fake_parameter", 89, "fake_component", "HareDu");
        }

        [Benchmark]
        public async Task ScopedParameterCreateExtensionBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .CreateScopeParameter<long>("fake_parameter", 89, "fake_component", "HareDu");
        }

        [Benchmark]
        public async Task TopicPermissionsCreateBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<TopicPermissions>()
                .Create("user1", "E4", "HareDu", x =>
                {
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });
        }

        [Benchmark]
        public async Task TopicPermissionsCreateExtensionBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .CreateTopicPermission("user1", "E4", "HareDu", x =>
                {
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });
        }

        [Benchmark]
        public async Task UserPermissionsCreateBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Create("user", "HareDu", x =>
                {
                    x.UsingConfigurePattern(".*");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });
        }

        [Benchmark]
        public async Task UserPermissionsCreateExtensionBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .CreateUserPermissions("user", "HareDu", x =>
                {
                    x.UsingConfigurePattern(".*");
                    x.UsingReadPattern(".*");
                    x.UsingWritePattern(".*");
                });
        }

        [Benchmark]
        public async Task UserCreateBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create("user", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
        }

        [Benchmark]
        public async Task UserCreateExtensionBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .CreateUser("user", "testuserpwd3", "gkgfjjhfjh".ComputePasswordHash(), x =>
                {
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
        }

        [Benchmark]
        public async Task VirtualHostLimitsDefineBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHostLimits>()
                .Define("HareDu", x =>
                {
                    x.SetMaxQueueLimit(1);
                    x.SetMaxConnectionLimit(5);
                });
        }

        [Benchmark]
        public async Task VirtualHostLimitsDefineExtensionBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .DefineVirtualHostLimits("HareDu", x =>
                {
                    x.SetMaxQueueLimit(1);
                    x.SetMaxConnectionLimit(5);
                });
        }

        [Benchmark]
        public async Task VirtualHostCreateBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<VirtualHost>()
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
            var result = await _services.GetService<IBrokerObjectFactory>()
                .CreateVirtualHost("HareDu",x =>
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
        public async Task BindingCreateBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<Binding>()
                .Create("exchange", "queue", BindingType.Exchange, "HareDu");
        }

        [Benchmark]
        public async Task BindingCreateExtensionBenchmark()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .CreateExchangeBinding("exchange", "queue", "HareDu");
        }
    }
}