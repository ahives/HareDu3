namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Core.Configuration;
using Model;

public static class ScopedParameterExtensions
{
    /// <summary>
    /// Retrieves all scoped parameters configured on the RabbitMQ server for the given virtual hosts and components.
    /// </summary>
    /// <param name="factory">The API factory that provides access to the scoped parameter functionality.</param>
    /// <param name="credentials">The credential configuration for authenticating with the RabbitMQ server.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation, containing the results with a list of scoped parameter information.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Results<ScopedParameterInfo>> GetAllScopedParameters(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<ScopedParameter>(credentials)
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates a scoped parameter on the specified component and virtual host on the RabbitMQ server.
    /// </summary>
    /// <typeparam name="T">The type of the parameter value.</typeparam>
    /// <param name="factory">The API factory that provides access to the scoped parameter functionality.</param>
    /// <param name="credentials">The credential configuration for authenticating with the RabbitMQ server.</param>
    /// <param name="parameter">The name of the scoped parameter to be created.</param>
    /// <param name="value">The value to assign to the scoped parameter.</param>
    /// <param name="component">The component of the RabbitMQ server where the scoped parameter will be applied.</param>
    /// <param name="vhost">The virtual host within which the scoped parameter will be created.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the scoped parameter creation request.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> CreateScopeParameter<T>(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string parameter, T value, string component, string vhost,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<ScopedParameter>(credentials)
            .Create(parameter, value, component, vhost, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified scoped parameter from the RabbitMQ server.
    /// </summary>
    /// <param name="factory">The API factory that facilitates access to scoped parameter functionalities.</param>
    /// <param name="credentials">The action that provides the RabbitMQ credentials.</param>
    /// <param name="parameter">The name of the scoped parameter to be deleted.</param>
    /// <param name="component">The component associated with the scoped parameter.</param>
    /// <param name="vhost">The virtual host where the parameter exists.</param>
    /// <param name="cancellationToken">Token used to cancel the ongoing operation, if needed.</param>
    /// <returns>A task representing the asynchronous operation, containing the result of the scoped parameter deletion.</returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a policy.</exception>
    public static async Task<Result> DeleteScopedParameter(this IBrokerFactory factory,
        Action<HareDuCredentialProvider> credentials, string parameter, string component, string vhost, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);
        Guard.IsNotNull(credentials);

        return await factory
            .API<ScopedParameter>(credentials)
            .Delete(parameter, component, vhost, cancellationToken)
            .ConfigureAwait(false);
    }
}