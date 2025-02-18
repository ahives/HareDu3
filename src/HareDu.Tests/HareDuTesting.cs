﻿namespace HareDu.Tests;

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using NUnit.Framework;

public class HareDuTesting
{
    protected ServiceCollection GetContainerBuilder(string file, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var services = new ServiceCollection();

        services.AddSingleton<IBrokerFactory>(x =>
        {
            string data = File.ReadAllText($"{TestContext.CurrentContext.TestDirectory}/{file}");
                    
            return new BrokerFactory(GetClient(data, statusCode));
        });
            
        return services;
    }

    protected ServiceCollection GetContainerBuilder(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var services = new ServiceCollection();

        services.AddSingleton<IBrokerFactory>(x => new BrokerFactory(GetClient(string.Empty, statusCode)));

        return services;
    }

    protected HttpClient GetClient(string data, HttpStatusCode statusCode = HttpStatusCode.OK)
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

        var uri = new Uri("http://localhost:15672/");
        var client = new HttpClient(mock.Object){BaseAddress = uri};
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        return client;
    }
}