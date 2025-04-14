namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class ScopedParameterExtensions
{
    /// <summary>
    /// Retrieves all scoped parameters available on the current RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the scoped parameter functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation, containing the results of scoped parameter information.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Results<ScopedParameterInfo>> GetAllScopedParameters(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<ScopedParameter>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a scoped parameter on the RabbitMQ server with the specified details.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the scoped parameter functionality.</param>
    /// <param name="parameter">The name of the scoped parameter to create.</param>
    /// <param name="value">The value of the scoped parameter.</param>
    /// <param name="component">The component to associate with the scoped parameter, such as a queue or exchange.</param>
    /// <param name="vhost">The virtual host where the scoped parameter will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <typeparam name="T">The type of the value assigned to the scoped parameter.</typeparam>
    /// <returns>A task representing the asynchronous operation, containing the result of the scoped parameter creation.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if the API implementation for the user could not be accessed.</exception>
    public static async Task<Result> CreateScopeParameter<T>(this IBrokerFactory factory,
        string parameter, T value, string component, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<ScopedParameter>()
            .Create(parameter, value, component, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes a specified scoped parameter from the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the scoped parameter functionality.</param>
    /// <param name="parameter">The name of the scoped parameter to be deleted.</param>
    /// <param name="component">The component for which the scoped parameter is defined.</param>
    /// <param name="vhost">The virtual host where the scoped parameter is defined.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the delete action.</returns>
    /// <exception cref="ArgumentNullException">Throws if the provided IBrokerFactory is null.</exception>
    public static async Task<Result> DeleteScopedParameter(this IBrokerFactory factory,
        string parameter, string component, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<ScopedParameter>()
            .Delete(parameter, component, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}