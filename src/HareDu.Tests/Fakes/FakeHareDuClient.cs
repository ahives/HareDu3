namespace HareDu.Tests;

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Core.Security;
using HTTP;
using Moq;
using Moq.Protected;

public class FakeHareDuClient(string data, HttpStatusCode statusCode = HttpStatusCode.OK) :
    IHareDuClient
{
    public HttpClient GetClient(Action<HareDuCredentialProvider> provider)
    {
        var client = new HttpClient(GetHttpMessageHandler());

        client.BaseAddress = new Uri("http://localhost:15672/");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("User-Agent", "HareDu");

        return client;
    }

    public void CancelPendingRequests()
    {
        throw new NotImplementedException();
    }

    HttpMessageHandler GetHttpMessageHandler()
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