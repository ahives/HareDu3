namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;

class UserPermissionsImpl :
    BaseBrokerObject,
    UserPermissions
{
    public UserPermissionsImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<UserPermissionsInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<UserPermissionsInfo>("api/permissions", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string username, string vhost,
        Action<UserPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new UserPermissionsConfiguratorImpl();
        configurator?.Invoke(impl);

        var request = impl.Request.Value;

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username and/or password is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        string url = $"api/permissions/{vhost.ToSanitizedName()}/{username}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Request = request.ToJsonString(), Errors = errors}};

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string username, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username and/or password is missing."});

        if (string.IsNullOrWhiteSpace(vhost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        string url = $"api/permissions/{vhost.ToSanitizedName()}/{username}";

        if (errors.Count > 0)
            return new FaultedResult{DebugInfo = new (){URL = url, Errors = errors}};

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }


    class UserPermissionsConfiguratorImpl :
        UserPermissionsConfigurator
    {
        string _configurePattern;
        string _readPattern;
        string _writePattern;

        public Lazy<UserPermissionsRequest> Request { get; }

        public UserPermissionsConfiguratorImpl()
        {
            Request = new Lazy<UserPermissionsRequest>(
                () => new ()
                {
                    Configure = _configurePattern,
                    Write = _writePattern,
                    Read = _readPattern
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void UsingConfigurePattern(string pattern) => _configurePattern = pattern;

        public void UsingWritePattern(string pattern) => _writePattern = pattern;

        public void UsingReadPattern(string pattern) => _readPattern = pattern;
    }
}