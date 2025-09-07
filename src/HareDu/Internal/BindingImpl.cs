namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Model;

class BindingImpl :
    BaseHareDuImpl,
    Binding
{
    public BindingImpl(HttpClient client, [FromKeyedServices("broker")] IHareDuDeserializer deserializer)
        : base(client, deserializer)
    {
    }

    public async Task<Results<BindingInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetAllRequest<BindingInfo>("api/bindings", RequestType.Binding, cancellationToken).ConfigureAwait(false);
    }
}