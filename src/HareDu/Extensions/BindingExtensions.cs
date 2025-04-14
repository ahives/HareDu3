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
    /// Fetches all bindings available in the RabbitMQ broker.
    /// </summary>
    /// <param name="factory">
    /// The broker factory instance used to access the RabbitMQ API.
    /// </param>
    /// <param name="cancellationToken">
    /// A token used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the results of the bindings retrieved from the broker as <see cref="Results{BindingInfo}"/>.
    /// </returns>
    public static async Task<Results<BindingInfo>> GetAllBindings(this IBrokerFactory factory,
        CancellationToken cancellationToken = default)
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