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