namespace HareDu.HTTP;

using System;
using System.Net.Http;
using Core.Security;

/// <summary>
/// Provides functionalities to create and manage HTTP clients configured to communicate
/// with RabbitMQ brokers using the provided credentials.
/// </summary>
public interface IHareDuClient
{
    /// <summary>
    /// Creates and returns an instance of <see cref="HttpClient"/> configured with the broker credentials provided through the specified action.
    /// </summary>
    /// <param name="provider">An action specifying the <see cref="HareDuCredentialProvider"/> to configure the credentials for authentication with the broker.</param>
    /// <returns>An instance of <see cref="HttpClient"/> configured to communicate with the broker.</returns>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    HttpClient GetClient(Action<HareDuCredentialProvider> provider);

    /// <summary>
    /// Cancels all pending requests for all <see cref="HttpClient"/> instances managed by the current client.
    /// </summary>
    void CancelPendingRequests();
}