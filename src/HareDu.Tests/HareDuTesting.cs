namespace HareDu.Tests;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using NUnit.Framework;

public class HareDuTesting
{
    protected ServiceCollection GetContainerBuilder(string file, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var services = new ServiceCollection();

        string data = File.ReadAllText($"{TestContext.CurrentContext.TestDirectory}/{file}");

        services.AddHttpClient<BrokerFactory>("broker", client =>
            {
                client.BaseAddress = new Uri("http://localhost:15672/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .ConfigurePrimaryHttpMessageHandler(() => GetHttpMessageHandler(data, statusCode));

        services.AddSingleton<IBrokerFactory, BrokerFactory>();
        services.AddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();

        return services;
    }

    protected ServiceCollection GetContainerBuilder(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var services = new ServiceCollection();

        services.AddHttpClient("broker", client =>
            {
                client.BaseAddress = new Uri("http://localhost:15672/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .ConfigurePrimaryHttpMessageHandler(() => GetHttpMessageHandler(string.Empty, statusCode));

        services.AddSingleton<IBrokerFactory, BrokerFactory>();
        services.AddSingleton<IHareDuCredentialBuilder, HareDuCredentialBuilder>();

        return services;
    }

    protected HttpMessageHandler GetHttpMessageHandler(string data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var mock = new Mock<HttpMessageHandler>();

        mock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(
                new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = new StringContent(data)
                })
            .Verifiable();

        return mock.Object;
    }
}