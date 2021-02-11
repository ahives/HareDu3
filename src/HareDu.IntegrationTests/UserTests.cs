namespace HareDu.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using MicrosoftIntegration;
    using NUnit.Framework;

    [TestFixture]
    public class UserTests
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
        public async Task Verify_can_get_all_users()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .GetAll()
                .ScreenDump();
        }
        
        [Test]
        public async Task Verify_can_get_all_users_without_permissions()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .GetAllWithoutPermissions()
                .ScreenDump();
        }
        
        [Test]
        public async Task Verify_can_create()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Create(x =>
                {
                    x.Username("testuser3");
                    x.Password("testuserpwd3");
                    x.PasswordHash("gkgfjjhfjh");
                    x.WithTags(t =>
                    {
                        t.Administrator();
                    });
                });
            
            Console.WriteLine(result.ToJsonString());
        }


        [Test]
        public async Task Verify_can_delete()
        {
            var result = await _services.GetService<IBrokerObjectFactory>()
                .Object<User>()
                .Delete(x => x.User(""));
        }
    }
}