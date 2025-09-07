namespace HareDu.Internal;

using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Model;

class AuthenticationImpl :
    BaseHareDuImpl,
    Authentication
{
    public AuthenticationImpl(HttpClient client, [FromKeyedServices("broker")] IHareDuDeserializer deserializer)
        : base(client, deserializer)
    {
    }

    public async Task<Result<AuthenticationDetails>> GetDetails(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetRequest<AuthenticationDetails>("api/auth", RequestType.Authentication, cancellationToken).ConfigureAwait(false);
    }
}