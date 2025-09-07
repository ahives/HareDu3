namespace HareDu.Shovel.Extensions;

using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Extensions;
using Core.Security;
using Model;

public static class ShovelExtensions
{
    /// <summary>
    /// Creates a dynamic shovel in the specified virtual host on the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The broker factory instance used to communicate with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="name">The name of the shovel to be created.</param>
    /// <param name="vhost">The virtual host where the shovel will be created.</param>
    /// <param name="configurator">An optional configurator to define specific shovel settings.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="HareDu.Core.Result"/> indicating the outcome of the operation.</returns>
    /// <exception cref="System.ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="System.OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDu.Core.Security.HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> CreateShovel(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string name,
        [NotNull] string vhost,
        [AllowNull] Action<ShovelConfigurator> configurator = null,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>(credentials)
            .Create(name, vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a specific dynamic shovel in the specified virtual host from the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The broker factory instance used to communicate with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="name">The name of the shovel to be deleted.</param>
    /// <param name="vhost">The virtual host where the shovel to be deleted is located.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="HareDu.Core.Result"/> indicating the outcome of the operation.</returns>
    /// <exception cref="System.ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="System.OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDu.Core.Security.HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Result> DeleteShovel(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string name,
        [NotNull] string vhost,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>(credentials)
            .Delete(name, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes all dynamic shovels for the specified virtual host from the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The broker factory instance used to communicate with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="vhost">The virtual host on which the shovels to be deleted are located.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation and contains the results of type <see cref="System.Collections.Generic.IReadOnlyList{T}"/> indicating the outcome of each shovel deletion.</returns>
    /// <exception cref="System.ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="System.OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDu.Core.Security.HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<IReadOnlyList<Result>> DeleteAllShovels(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string vhost,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        var result = await factory
            .API<Shovel>(credentials)
            .GetAll(vhost, cancellationToken)
            .ConfigureAwait(false);

        if (result.HasFaulted)
            return [];

        var shovels = result.Select(x => x.Data)
            .Where(x => x.VirtualHost == vhost)
            .ToList();

        var results = new List<Result>();

        foreach (var shovel in shovels)
        {
            var deleteResult = await factory
                .API<Shovel>(credentials)
                .Delete(shovel.Name, vhost, cancellationToken)
                .ConfigureAwait(false);

            results.Add(deleteResult);
        }

        return results;
    }

    /// <summary>
    /// Retrieves a list of all dynamic shovels in the specified virtual host on the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The broker factory instance used to communicate with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="vhost">The virtual host from which all shovels will be retrieved.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation and contains a <see cref="HareDu.Core.Results{T}"/> object with details of all dynamic shovels.</returns>
    /// <exception cref="System.ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="System.OperationCanceledException">Throws if the operation is canceled via the cancellation token.</exception>
    /// <exception cref="HareDu.Core.Security.HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Results<ShovelInfo>> GetAllShovels(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] string vhost,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>(credentials)
            .GetAll(vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves all shovels configured on the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">The broker factory instance used to communicate with the RabbitMQ broker.</param>
    /// <param name="credentials">The credential provider used to authenticate with the RabbitMQ broker.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation and contains the result of type <see cref="HareDu.Core.Results{T}"/> with information about all configured shovels.</returns>
    /// <exception cref="System.ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="System.OperationCanceledException">Throws if the thread has a cancellation request.</exception>
    /// <exception cref="HareDu.Core.Security.HareDuSecurityException">Throws if the user credentials are not valid.</exception>
    [return: NotNull]
    public static async Task<Results<ShovelInfo>> GetAllShovels(
        [NotNull] this IHareDuFactory factory,
        [NotNull] Action<HareDuCredentialProvider> credentials,
        [NotNull] CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Shovel>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }
}