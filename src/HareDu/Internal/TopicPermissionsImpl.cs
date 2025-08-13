namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Model;
using Serialization;

class TopicPermissionsImpl :
    BaseBrokerImpl,
    TopicPermissions
{
    public TopicPermissionsImpl(HttpClient client)
        : base(client, Deserializer.Options)
    {
    }

    public async Task<Results<TopicPermissionsInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<TopicPermissionsInfo>("api/topic-permissions", RequestType.TopicPermissions, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(
        string username,
        string vhost,
        Action<TopicPermissionsConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic(Debug.Info("api/topic-permissions/{vhost}/{username}",
                Errors.Create(e => { e.Add("No topic permissions was defined."); })));

        var impl = new TopicPermissionsConfiguratorImpl();
        configurator(impl);

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = impl.Validate();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The username and/or password is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        var request = impl.Request.Value;

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/topic-permissions/{vhost}/{username}", errors, request: request.ToJsonString(Deserializer.Options)))
            : await PutRequest($"api/topic-permissions/{sanitizedVHost}/{username}", request,
                RequestType.TopicPermissions, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string username, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The username and/or password is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/topic-permissions/{vhost}/{username}", errors))
            : await DeleteRequest($"api/topic-permissions/{sanitizedVHost}/{username}", RequestType.TopicPermissions,
                cancellationToken).ConfigureAwait(false);
    }


    class TopicPermissionsConfiguratorImpl :
        TopicPermissionsConfigurator
    {
        string _writePattern;
        string _readPattern;
        bool _usingWritePatternCalled;
        bool _usingReadPatternCalled;
        string _exchange;

        List<Error> InternalErrors { get; } = new();

        public Lazy<TopicPermissionsRequest> Request { get; }

        public TopicPermissionsConfiguratorImpl()
        {
            InternalErrors = new List<Error>();
            Request = new Lazy<TopicPermissionsRequest>(
                () => new ()
                {
                    Exchange = _exchange,
                    Read = _readPattern,
                    Write = _writePattern
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Exchange(string exchange) => _exchange = exchange;

        public void UsingWritePattern(string pattern)
        {
            _usingWritePatternCalled = true;
            _writePattern = pattern;

            InternalErrors.AddIfTrue(pattern, string.IsNullOrWhiteSpace, Errors.Create("The write pattern is missing."));
        }

        public void UsingReadPattern(string pattern)
        {
            _usingReadPatternCalled = true;
            _readPattern = pattern;

            InternalErrors.AddIfTrue(pattern, string.IsNullOrWhiteSpace, Errors.Create("The read pattern is missing."));
        }

        public List<Error> Validate()
        {
            if (!_usingWritePatternCalled)
                InternalErrors.Add(Errors.Create("The write pattern is missing."));

            if (!_usingReadPatternCalled)
                InternalErrors.Add(Errors.Create("The read pattern is missing."));

            InternalErrors.AddIfTrue(_exchange, string.IsNullOrWhiteSpace, Errors.Create("Then name of the exchange is missing."));

            return InternalErrors;
        }
    }
}