namespace HareDu;

using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Binding :
    BrokerAPI
{
    /// <summary>
    /// Returns all bindings on the current RabbitMQ node.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation running on the current thread.</param>
    /// <returns>Asynchronous task of <see cref="Result{T}"/></returns>
    Task<Results<BindingInfo>> GetAll(CancellationToken cancellationToken = default);
}