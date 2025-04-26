namespace HareDu.HTTP;

using System.Net.Http;
using Core.Configuration;

/// <summary>
/// Provides functionalities to create and manage HTTP clients configured to communicate
/// with RabbitMQ brokers using the provided credentials.
/// </summary>
public interface IHareDuClient
{
    /// <summary>
    /// Creates and returns an instance of <see cref="HttpClient"/> configured with the provided broker credentials.
    /// </summary>
    /// <param name="credentials">The <see cref="HareDuCredentials"/> which contain the username and password for authentication with the broker.</param>
    /// <returns>An instance of <see cref="HttpClient"/> configured to communicate with the broker.</returns>
    HttpClient CreateClient(HareDuCredentials credentials);
}