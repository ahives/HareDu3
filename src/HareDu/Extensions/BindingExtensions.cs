namespace HareDu.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    public static class BindingExtensions
    {
        public static async Task<ResultList<BindingInfo>> GetAllBindings(this IBrokerObjectFactory factory, CancellationToken cancellationToken = default)
        {
            if (factory.IsNull())
                throw new ArgumentNullException(nameof(factory));

            return await factory.Object<Binding>()
                .GetAll(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}