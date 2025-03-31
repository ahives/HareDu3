namespace HareDu.Extensions;

using System;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Diagnostics;
using Core;
using Model;

public static class BindingExtensions
{
    /// <summary>
    /// Returns all bindings on the current RabbitMQ node.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a binding.</exception>
    public static async Task<Results<BindingInfo>> GetAllBindings(this IBrokerFactory factory, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Binding>()
            .GetAll(cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Creates the specified binding between source exchange and destination exchange on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">The virtual host where the binding is defined.</param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a binding.</exception>
    public static async Task<Result> CreateBinding(
        this IBrokerFactory factory,
        string vhost,
        Action<BindingConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Binding>()
            .Create(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }

    /// <summary>
    /// Deletes the specified binding between exchanges on the specified RabbitMQ virtual host.
    /// </summary>
    /// <param name="factory">The API that implements the underlying functionality.</param>
    /// <param name="vhost">The virtual host where the binding is defined.</param>
    /// <param name="configurator"></param>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">Throws if IBrokerFactory is null.</exception>
    /// <exception cref="HareDuBrokerApiInitException">Throws if HareDu could not find the implementation associated with a binding.</exception>
    public static async Task<Result> DeleteBinding(
        this IBrokerFactory factory,
        string vhost,
        Action<DeleteBindingConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(factory);

        return await factory
            .API<Binding>()
            .Delete(vhost, configurator, cancellationToken)
            .ConfigureAwait(false);
    }
}

public static class ResultExtensions
{
    public static Result<TValue> Maybe<T, TValue>(this Result<T> source, Func<T, Result<TValue>> projection)
    {
        Guard.IsNotNull(source);

        return !source.HasFaulted && source.HasData
            ? projection(source.Data) ?? ResultHelper.Missing<TValue>()
            : ResultHelper.Missing<TValue>();
    }
}