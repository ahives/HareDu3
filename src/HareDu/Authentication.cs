namespace HareDu;

using System.Threading;
using System.Threading.Tasks;
using Core;
using Model;

public interface Authentication :
    BrokerAPI
{
    Task<Result<AuthenticationDetails>> GetDetails(CancellationToken cancellationToken = default);
}