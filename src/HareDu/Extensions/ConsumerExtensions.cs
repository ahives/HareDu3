namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Security;
using Model;

public static class ConsumerExtensions
{
    /// <summary>
    /// Retrieves information about all consumers on the RabbitMQ node.
    /// </summary>
    /// <param name="factory">The factory instance used to access the RabbitMQ broker functionalities.</param>
    /// <param name="credentials">The credentials provider action to authenticate access to RabbitMQ resources.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of consumer information from the RabbitMQ node.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<ConsumerInfo>> GetAllConsumers(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Consumer>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves information about all consumers within a specific virtual host on the RabbitMQ node.
    /// </summary>
    /// <param name="factory">The factory instance used to access RabbitMQ broker functionalities.</param>
    /// <param name="credentials">The credential provider action to authenticate access to RabbitMQ resources.</param>
    /// <param name="vhost">The name of the virtual host whose consumer information is to be retrieved.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of consumer information specific to the provided virtual host.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    public static async Task<Results<ConsumerInfo>> GetConsumersByVirtualHost(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Consumer>(credentials)
            .GetByVirtualHost(vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}