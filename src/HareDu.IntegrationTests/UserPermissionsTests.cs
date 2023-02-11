namespace HareDu.IntegrationTests;

using System.Threading.Tasks;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using MicrosoftIntegration;
using NUnit.Framework;

[TestFixture]
public class UserPermissionsTests
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
    public async Task Should_be_able_to_get_all_user_permissions()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .Object<UserPermissions>()
            .GetAll()
            .ScreenDump();
    }

    [Test]
    public async Task Verify_can_delete_user_permissions()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .Object<UserPermissions>()
            .Delete("", "HareDu5");
    }

    [Test]
    public async Task Verify_can_create_user_permissions()
    {
        var result = await _services.GetService<IBrokerApiFactory>()
            .Object<UserPermissions>()
            .Create("", "HareDu5", x =>
            {
                x.UsingConfigurePattern("");
                x.UsingReadPattern("");
                x.UsingWritePattern("");
            });
    }
}