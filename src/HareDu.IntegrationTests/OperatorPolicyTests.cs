namespace HareDu.IntegrationTests;

using System;
using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;
using Serialization;

[TestFixture]
public class OperatorPolicyTests
{
    ServiceProvider _services;

    [OneTimeSetUp]
    public void Init()
    {
        _services = new ServiceCollection()
            .AddHareDu(x =>
            {
                x.Broker(b =>
                {
                    b.ConnectTo("http://localhost:15672");
                    b.UsingCredentials("guest", "guest");
                });
            })
            .BuildServiceProvider();
    }

    [Test]
    public async Task Should_be_able_to_get_all_policies()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Verify_can_create_operator_policy()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
            .Create("test7", "TestHareDu", x =>
            {
                x.Pattern(".*");
                x.Priority(0);
                x.ApplyTo(OperatorPolicyAppliedTo.Queues);
                x.Definition(arg =>
                {
                    arg.SetMaxInMemoryBytes(9803129);
                    arg.SetMaxInMemoryLength(283);
                    arg.SetDeliveryLimit(5);
                    arg.SetExpiry(5000);
                    arg.SetMessageMaxSize(189173219);
                });
            });
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }

    [Test]
    public async Task Test()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .API<OperatorPolicy>()
            .Delete("test6", "TestHareDu");
            
        Console.WriteLine(result.ToJsonString(Deserializer.Options));
    }
}