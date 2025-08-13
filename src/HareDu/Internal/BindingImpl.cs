namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;
using Serialization;

class BindingImpl :
    BaseBrokerImpl,
    Binding
{
    public BindingImpl(HttpClient client)
        : base(client, Deserializer.Options)
    {
    }

    public async Task<Results<BindingInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetAllRequest<BindingInfo>("api/bindings", RequestType.Binding, cancellationToken).ConfigureAwait(false);
    }
}