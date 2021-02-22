namespace HareDu.Internal
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    class ChannelImpl :
        BaseBrokerObject,
        Channel
    {
        public ChannelImpl(HttpClient client) :
            base(client)
        {
        }

        public async Task<ResultList<ChannelInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/channels";
            
            return await GetAllRequest<ChannelInfo>(url, cancellationToken).ConfigureAwait(false);
        }
    }
}