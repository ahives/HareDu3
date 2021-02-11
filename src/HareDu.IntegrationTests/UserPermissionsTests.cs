namespace HareDu.IntegrationTests
{
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
                .AddHareDu()
                .BuildServiceProvider();
        }

        [Test]
        public async Task Should_be_able_to_get_all_user_permissions()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .GetAll()
                .ScreenDump();
        }

        [Test]
        public async Task Verify_can_delete_user_permissions()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Delete(x =>
                {
                    x.User("");
                    x.Targeting(t => t.VirtualHost("HareDu5"));
                });
        }

        [Test]
        public async Task Verify_can_create_user_permissions()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<UserPermissions>()
                .Create(x =>
                {
                    x.User("");
                    x.Configure(c =>
                    {
                        c.UsingConfigurePattern("");
                        c.UsingReadPattern("");
                        c.UsingWritePattern("");
                    });
                    x.Targeting(t => t.VirtualHost("HareDu5"));
                });
        }
    }
}